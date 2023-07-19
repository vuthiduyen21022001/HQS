using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace HQSOFT.eBiz.Inventory.Blazor.Pages.Inventory.LotClasses
{
    public partial class LotSerClassesDetail
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<LotSerClassDto> LotSerClassList { get; set; }


        private bool CanCreateLotSerClass { get; set; }
        private bool CanEditLotSerClass { get; set; }
        private bool CanDeleteLotSerClass { get; set; }
        private Validations NewLotSerClassValidations { get; set; } = new();
        private LotSerClassUpdateDto EditingLotSerClass { get; set; }
        private Validations EditingLotSerClassValidations { get; set; } = new();
        private Guid EditingLotSerClassId { get; set; }
        private readonly IUiMessageService _uiMessageService;

        private LotSerSegmentCreateDto NewLotSerSegment { get; set; }
        private string FocusedColumn { get; set; }
        private Validations NewLotSerSegmentValidations { get; set; } = new();
        List<LotSerSegmentDto> listSegment { get; set; }
        private IReadOnlyList<object> selectedSegment { get; set; }
        IGrid Grid { get; set; }

        [Parameter]
        public string Id { get; set; }
        public LotSerClassesDetail(IUiMessageService uiMessageService)
        {
            EditingLotSerClass = new LotSerClassUpdateDto();
            EditingLotSerClass.ConcurrencyStamp = string.Empty;
            _uiMessageService = uiMessageService;
        }


        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();

            EditingLotSerClassId = Guid.Parse(Id);
            if (EditingLotSerClassId != Guid.Empty)
            {
                var serClassID = await LotSerClassesAppService.GetAsync(EditingLotSerClassId);
                EditingLotSerClass = ObjectMapper.Map<LotSerClassDto, LotSerClassUpdateDto>(serClassID);
            }
            listSegment = await LotSerSegmentsAppService.GetListAllAttriDetail(EditingLotSerClassId);
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:LotSerClasses"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {

            Toolbar.AddButton(L["Back"], () =>
            {
                NavigationManager.NavigateTo($"/Inventory/LotSerClasses");
                return Task.CompletedTask;
            },
            IconName.Undo,
            Color.Secondary);

            Toolbar.AddButton(L["New"], async () =>
            {
                await SaveLotSerClassAsync(true);
            }, IconName.Add,
            requiredPolicyName: InventoryPermissions.LotSerClasses.Create);

            Toolbar.AddButton(L["Save"], async () =>
            {
                await SaveLotSerClassAsync(false);
            },
           IconName.Save,
           Color.Primary,
           requiredPolicyName: InventoryPermissions.LotSerClasses.Edit);

            Toolbar.AddButton(L["Delete"], async () =>
            {
                var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                if (confirmed)
                {
                    await DeleteLotSerClassAsync(EditingLotSerClassId);
                }
            },
            IconName.Delete,
            Color.Danger,
            requiredPolicyName: InventoryPermissions.LotSerClasses.Delete);


            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateLotSerClass = await AuthorizationService
                .IsGrantedAsync(InventoryPermissions.LotSerClasses.Create);
            CanEditLotSerClass = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerClasses.Edit);
            CanDeleteLotSerClass = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerClasses.Delete);
        }
        private async Task CreateLotSerClassAsync()
        {
            try
            {
                if (await EditingLotSerClassValidations.ValidateAll() == false)
                {
                    return;
                }
                LotSerClassCreateDto lotSerClassCreateDto = ObjectMapper.Map<LotSerClassUpdateDto, LotSerClassCreateDto>(EditingLotSerClass);
                var serClass = await LotSerClassesAppService.CreateAsync(lotSerClassCreateDto);

                foreach (var item in listSegment)
                {
                    var mapitem = ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentCreateDto>(item);
                    mapitem.LotSerClassId = serClass.Id;
                    await LotSerSegmentsAppService.CreateAsync(mapitem);
                }


                EditingLotSerClassId = serClass.Id;
                EditingLotSerClass = ObjectMapper.Map<LotSerClassDto, LotSerClassUpdateDto>(serClass);
                NavigationManager.NavigateTo($"/Inventory/LotSerClasses/Detail/{EditingLotSerClassId}");

            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }

        }


        private async Task UpdateLotSerClassAsync()
        {
            try
            {
                if (await EditingLotSerClassValidations.ValidateAll() == false)
                {
                    return;
                }

                foreach (var item in listSegment)
                {
                    if (item.Id == Guid.Empty)
                    {
                        var mapitem = ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentCreateDto>(item);
                        mapitem.LotSerClassId = EditingLotSerClassId;
                        await LotSerSegmentsAppService.CreateAsync(mapitem);
                    }
                    else
                    {
                        var mapitem = ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentUpdateDto>(item);
                        mapitem.LotSerClassId = EditingLotSerClassId;
                        await LotSerSegmentsAppService.UpdateAsync(item.Id, mapitem);
                    }
                }
                await LotSerClassesAppService.UpdateAsync(EditingLotSerClassId, EditingLotSerClass);
                var serClass = await LotSerClassesAppService.GetAsync(EditingLotSerClassId);
                EditingLotSerClass = ObjectMapper.Map<LotSerClassDto, LotSerClassUpdateDto>(serClass);

            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        private async Task SaveLotSerClassAsync(bool isNewNext)
        {
            try
            {
                if (EditingLotSerClassId == Guid.Empty)
                {
                    await CreateLotSerClassAsync();
                }
                else
                {
                    await UpdateLotSerClassAsync();
                }
                if (isNewNext)
                {

                    NavigationManager.NavigateTo($"/Inventory/LotSerClasses/Detail/{Guid.Empty}", true);

                }

            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        private async Task DeleteLotSerClassAsync(Guid Id)
        {
            await LotSerClassesAppService.DeleteAsync(Id);
            NavigationManager.NavigateTo("/Inventory/LotSerClasses");
        }

        async Task Grid_EditModelSaving(GridEditModelSavingEventArgs e)
        {
            var edittableDetail = (LotSerSegmentDto)e.EditModel;
            var createtableDetail = ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentCreateDto>(edittableDetail);
            if (e.IsNew)
            {
                createtableDetail.LotSerClassId = EditingLotSerClassId;
                await LotSerSegmentsAppService.CreateAsync(createtableDetail);
            }
            else
            {
                var updatetableDetail = ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentUpdateDto>(edittableDetail);
                await LotSerSegmentsAppService.UpdateAsync(edittableDetail.Id, updatetableDetail);
            }
            await UpdateDataAsync();

        }
        async Task Grid_DataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            var removetableDetail = (LotSerSegmentDto)e.DataItem;
            if (EditingLotSerClassId == Guid.Empty)
            {
                listSegment.Remove(removetableDetail);
            }
            else
            {
                listSegment.Remove(removetableDetail);
                await LotSerSegmentsAppService.DeleteAsync(removetableDetail.Id);
                await UpdateDataAsync();
            }

        }
        void Grid_CustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {
            if (e.IsNew)
            {
                var newEmployee = (LotSerSegmentDto)e.EditModel;
                newEmployee = new LotSerSegmentDto();
            }
        }
        async Task UpdateDataAsync()
        {
            listSegment = await LotSerSegmentsAppService.GetListAllAttriDetail(EditingLotSerClassId);
        }
        public Task UpdateListLotSegmentAsync(LotSerSegmentDto dataItem, LotSerSegmentDto newDataItem)
        {

            var index = listSegment.FindIndex(r => r.SegmentID == dataItem.SegmentID);
            if (index != -1)
            {
                listSegment[index] = newDataItem;
            }
            return Task.CompletedTask;
        }


        private async Task Grid_OnRowDoubleClick(GridRowClickEventArgs e)
        {
            FocusedColumn = e.Column.Name;
            await e.Grid.StartEditRowAsync(e.VisibleIndex);
        }

    }
}
