using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using DevExpress.Blazor;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using HQSOFT.eBiz.Inventory.Permissions;
using HQSOFT.eBiz.Inventory.Shared;
using HQSOFT.eBiz.Inventory.LotSerSegments;
using Volo.Abp.AspNetCore.Components.Messages;
using static HQSOFT.eBiz.Inventory.Permissions.InventoryPermissions;
using Microsoft.AspNetCore.Components;
using Volo.Abp.ObjectMapping;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using HQSOFT.eBiz.Inventory;

namespace HQSOFT.eBiz.Inventory.Blazor.Pages.Inventory.LClass

{
    public partial class SerClassDetail
    {

        //==================================Initialize Section===================================

        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int MaxCount { get; } = 1000;

        private bool CanCreateLotSerClass { get; set; }
        private bool CanEditLotSerClass { get; set; }
        private bool CanDeleteLotSerClass { get; set; }
        private bool CanCreateLotSerSegment { get; set; }
        private bool CanEditLotSerSegment { get; set; }
        private bool CanDeleteLotSerSegment { get; set; }



        private LotSerClassUpdateDto EditingLotSerClass { get; set; }//Current editting LotSerClass 
        private Guid EditingLotSerClassId { get; set; }//Current edditing LotSerClass Id
        private LotSerSegmentDto EditingSegment { get; set; } = new LotSerSegmentDto();  //Editing row on grid
        private Guid EditingSegmentId { get; set; } = Guid.Empty; //Editing Segment Id on grid
        private List<LotSerSegmentDto> Segment { get; set; } = new List<LotSerSegmentDto>(); //Data source used to bind to grid
        private IReadOnlyList<object> selectedSegments { get; set; } = new List<LotSerSegmentDto>(); //Selected rows on grid


        private Validations ClassValidations { get; set; } = new();



        private readonly IUiMessageService _uiMessageService;
        private string FocusedColumn { get; set; }
        private bool IsDataEntryChanged = false;
        private IGrid SegmentGrid { get; set; } //Segment grid control name
        private Validations SegmentValidations { get; set; } = new();
        private EditContext _gridSegmentEditContext;

     

        [Parameter]
        public string Id { get; set; }

        //==================================Initialize Section===================================


        public SerClassDetail(IUiMessageService uiMessageService)
        {
            EditingLotSerClass = new LotSerClassUpdateDto();
            EditingLotSerClass.ConcurrencyStamp = string.Empty;
            _uiMessageService = uiMessageService;
        }

        EditContext GridSegmentEditContext
        {
            get { return SegmentGrid.IsEditing() ? _gridSegmentEditContext : null; }
            set { _gridSegmentEditContext = value; }
        }

        protected override async Task OnInitializedAsync()
        {
            if (IsDataEntryChanged)
            {
                var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                //if (confirmed)
                //{
                //    await JSRuntime.InvokeVoidAsync("preventRefresh");
                //}

            }
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            EditingLotSerClassId = Guid.Parse(Id);
            await LoadDataAsync(EditingLotSerClassId);

           
        }
        private async Task SetPermissionsAsync()
        {
            CanCreateLotSerClass = await AuthorizationService
                .IsGrantedAsync(InventoryPermissions.LotSerClasses.Create);
            CanEditLotSerClass = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerClasses.Edit);
            CanDeleteLotSerClass = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerClasses.Delete);
            CanCreateLotSerSegment = await AuthorizationService
             .IsGrantedAsync(InventoryPermissions.LotSerSegments.Create);
            CanEditLotSerSegment = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerSegments.Edit);
            CanDeleteLotSerSegment = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerSegments.Delete);

        }
        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:LotSerClasses"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {

            Toolbar.AddButton(L["Back"], async() =>
            {
                bool checkSaving = await SavingConfirmAsync();
                if (!checkSaving)
                    NavigationManager.NavigateTo($"/Inventory/LotSerClasses");
                
            },
            IconName.Undo,
            Color.Secondary);

       
            Toolbar.AddButton(L["Save"], async () =>
            {
                await SaveClassessAsync(false);
            },
              IconName.Save,
              Color.Primary,
               requiredPolicyName: InventoryPermissions.LotSerClasses.Edit);

            Toolbar.AddButton(L["New"], async () =>
            {
                await SaveClassessAsync(true);
            }, IconName.Add,
            Color.Primary,
            requiredPolicyName: InventoryPermissions.LotSerSegments.Create);


            Toolbar.AddButton(L["Delete"], DeleteClass,
                IconName.Delete,
                Color.Danger,
                requiredPolicyName: InventoryPermissions.LotSerClasses.Delete);

            return ValueTask.CompletedTask;
        }

      


        //======================CRUD & Load Main Data Source Section=============================
        private async Task LoadDataAsync(Guid classId)
        {
            if (classId != Guid.Empty)
            {
                var clas = await LotSerClassesAppService.GetAsync(classId);
                EditingLotSerClass = ObjectMapper.Map<LotSerClassDto, LotSerClassUpdateDto>(clas);
                await GetSegmentAsync();
            }
        }
        private async Task GetSegmentAsync()
        {
            var result = await LotSerSegmentsAppService.GetListAsync(new GetLotSerSegmentsInput
            {
                LotSerClassId = EditingLotSerClassId,
                MaxResultCount = MaxCount,



            });
            Segment = ObjectMapper.Map<List<LotSerSegmentDto>, List<LotSerSegmentDto>>((List<LotSerSegmentDto>)result.Items);
        }

        private async Task NewClass()
        {
            EditingLotSerClass = new LotSerClassUpdateDto
            {
             
               
                TrackingMethod = TrackingMethod.L,
                AssignMethod = AssignMethod.U,
                IssueMethod =IssueMethod.L,
                ConcurrencyStamp = string.Empty,

            };
            EditingLotSerClassId = Guid.Empty;
            Segment = new List<LotSerSegmentDto>();
            IsDataEntryChanged = false;
            NavigationManager.NavigateTo($"/Inventory/LotSerClasses/Detail/{Guid.Empty}");
        }
        private async Task SaveClassessAsync(bool IsNewNext)
        {
            try
            {
                await Task.CompletedTask;
                //if (await ClassValidations.ValidateAll() == false)
                //{
                //    return;
                //}

                if (EditingLotSerClassId == Guid.Empty)
                {
                    var clas = await LotSerClassesAppService.CreateAsync(EditingLotSerClass);
                    EditingLotSerClassId = clas.Id;
                }
                else
                {
                    await LotSerClassesAppService.UpdateAsync(EditingLotSerClassId, EditingLotSerClass);
                    var clas = await LotSerClassesAppService.GetAsync(EditingLotSerClassId);
                    EditingLotSerClass = ObjectMapper.Map<LotSerClassDto, LotSerClassUpdateDto>(clas);
                }

                await SaveSegmentAsync();

                if (IsNewNext)
                    await NewClass();
                else
                    NavigationManager.NavigateTo($"/Inventory/LotSerClasses/Detail/{EditingLotSerClassId}");
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }

        }
        private async Task SaveSegmentAsync()
        {
            try
            {
                await Task.CompletedTask;
                foreach (var ser in Segment)
                {
                    if (ser.LotSerClassId == Guid.Empty)
                        ser.LotSerClassId = EditingLotSerClassId;

                    if (ser.ConcurrencyStamp == string.Empty)
                        await LotSerSegmentsAppService.CreateAsync(ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentUpdateDto>(ser));
                    else
                        await LotSerSegmentsAppService.UpdateAsync(ser.Id, ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentUpdateDto>(ser));
                }
                await GetSegmentAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task DeleteClass()
        {
            var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
            if (confirmed)
            {
                await Task.CompletedTask;
                await DeleteSegment();
                await DeleteClassAsync(EditingLotSerClassId);
            }
        }
        private async Task DeleteClassAsync(Guid Id)
        {
            await LotSerClassesAppService.DeleteAsync(Id);
            NavigationManager.NavigateTo("/Inventory/LotSerClasses");
        }
        private async Task DeleteSegment()
        {
            await Task.CompletedTask;
            foreach (var ser in Segment)
            {
                if (ser.ConcurrencyStamp != string.Empty)
                    await LotSerSegmentsAppService.DeleteAsync(ser.Id);
            }
        }
        private async Task DeleteSegment(Guid Id)
        {
            await Task.CompletedTask;
            await LotSerSegmentsAppService.DeleteAsync(Id);
            await GetSegmentAsync();
        }


        //=====================================Validations=======================================
 
        private async Task<bool> SavingConfirmAsync()
        {
            if (IsDataEntryChanged)
            {
                var confirmed = await _uiMessageService.Confirm(L["SavingConfirmationMessage"]);
                if (confirmed)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
   



        //============================Controls triggers/events===================================

        private async Task SegmentGrid_OnFocusedRowChanged(GridFocusedRowChangedEventArgs e)
        {
            if (SegmentGrid.IsEditing() && _gridSegmentEditContext.IsModified())
            {
                await SegmentGrid.SaveChangesAsync();
                IsDataEntryChanged = true;
            }

        }
        private async Task SegmentGrid_OnRowDoubleClick(GridRowClickEventArgs e)
        {
            FocusedColumn = e.Column.Name;
            await e.Grid.StartEditRowAsync(e.VisibleIndex);
            EditingSegment = (LotSerSegmentDto)e.Grid.GetFocusedDataItem();
            EditingSegmentId = EditingSegment.Id;
        }
        private void SegmentGrid_OnCustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {

            if (e.IsNew)
            {
                var newRow = (LotSerSegmentDto)e.EditModel;
                newRow.Id = Guid.Empty;
                newRow.LotSerClassId = EditingLotSerClassId;
                newRow.ConcurrencyStamp = string.Empty;

            }
        }
        private void SegmentGrid_EditModelSaving(GridEditModelSavingEventArgs e)
        {
      
            LotSerSegmentDto editModel = (LotSerSegmentDto)e.EditModel;
            if (e.EditModel != null && e.IsNew)
                Segment.Add(e.EditModel as LotSerSegmentDto);
        }

        private async Task SegmentGrid_DataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            if (e.DataItem != null)
                await DeleteSegment((e.DataItem as LotSerSegmentDto).Id);
        }

        private async Task BtnAdd_SegmentGrid_OnClick()
        {


            await SegmentGrid.SaveChangesAsync();
            SegmentGrid.ClearSelection();
            await SegmentGrid.StartEditNewRowAsync();
        }

        private async Task BtnDelete_SegmentGrid_OnClick()
        {
            if (selectedSegments != null)
            {
                foreach (LotSerSegmentDto row in selectedSegments)
                    await DeleteSegment(row.Id);
                selectedSegments = null;
            }
        }
        //===========================chữ hoa========


        // Define EditingLotSerClass property

        // Event handler to convert ClassID to uppercase when the value changes
        private void OnClassIDInput(ChangeEventArgs e)
        {
            string inputValue = e.Value?.ToString() ?? string.Empty;
            EditingLotSerClass.ClassID = inputValue.ToUpper();
        }



    }

}
