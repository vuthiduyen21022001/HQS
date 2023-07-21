using AutoMapper.Internal.Mappers;
using Blazorise.DataGrid;
using Blazorise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;
using Volo.Abp.Http.Client;
using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.Shared;
using HQSOFT.eBiz.Inventory.Permissions;

namespace HQSOFT.eBiz.Inventory.Blazor.Pages.Inventory.LotSegment
{
    public partial class LotSerSegment
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<LotSerSegmentDto> SegmentListt { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateLotSerSegment { get; set; }
        private bool CanEditLotSerSegment { get; set; }
        private bool CanDeleteLotSerSegment { get; set; }
       
        private Validations EditingLotSerSegmentValidations { get; set; } = new();
        private LotSerSegmentUpdateDto EditingLotSerSegment { get; set; }
        private Guid EditingLotSerSegmentId { get; set; }
        private Modal CreateLotSerSegmentModal { get; set; } = new();
        private Modal EditLotSerSegmentModal { get; set; } = new();
        private GetLotSerSegmentsInput Filter { get; set; }
        private DataGridEntityActionsColumn<LotSerSegmentWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "lotSerSegment-create-tab";
        protected string SelectedEditTab = "lotSerSegment-edit-tab";
        private IReadOnlyList<LookupDto<Guid>> LotSerClassesCollection { get; set; } = new List<LookupDto<Guid>>();

        public LotSerSegment()
        {
            EditingLotSerSegment = new LotSerSegmentUpdateDto();
            EditingLotSerSegment = new LotSerSegmentUpdateDto();
            Filter = new GetLotSerSegmentsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            SegmentListt = new List<LotSerSegmentDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetLotSerClassCollectionLookupAsync();


        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:LotSerSegments"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () => { await DownloadAsExcelAsync(); }, IconName.Download);

            Toolbar.AddButton(L["EditingLotSerSegment"], async () =>
            {
                await OpenCreateLotSerSegmentModalAsync();
            }, IconName.Add, requiredPolicyName: InventoryPermissions.LotSerSegments.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateLotSerSegment = await AuthorizationService
                .IsGrantedAsync(InventoryPermissions.LotSerSegments.Create);
            CanEditLotSerSegment = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerSegments.Edit);
            CanDeleteLotSerSegment = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.LotSerSegments.Delete);
        }

        private async Task GetLotSerSegmentsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await LotSerSegmentsAppService.GetListAsync(Filter);
            SegmentListt = result.Items;
            TotalCount = (int)result.TotalCount;
        }
  

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetLotSerSegmentsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task DownloadAsExcelAsync()
        {
            var token = (await LotSerSegmentsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Inventory") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/inventory/lot-ser-segments/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<LotSerSegmentDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetLotSerSegmentsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateLotSerSegmentModalAsync()
        {
            EditingLotSerSegment = new LotSerSegmentUpdateDto
            {


            };
            await EditingLotSerSegmentValidations.ClearAll();
            await CreateLotSerSegmentModal.Show();
        }

        private async Task CloseCreateLotSerSegmentModalAsync()
        {
            EditingLotSerSegment = new LotSerSegmentUpdateDto
            {


            };
            await CreateLotSerSegmentModal.Hide();
        }

        private async Task OpenEditLotSerSegmentModalAsync(LotSerSegmentWithNavigationPropertiesDto input)
        {
            var lotSerSegment = await LotSerSegmentsAppService.GetWithNavigationPropertiesAsync(input.LotSerSegment.Id);

            EditingLotSerSegmentId = lotSerSegment.LotSerSegment.Id;
            EditingLotSerSegment = ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentUpdateDto>(lotSerSegment.LotSerSegment);
            await EditingLotSerSegmentValidations.ClearAll();
            await EditLotSerSegmentModal.Show();
        }

        private async Task DeleteLotSerSegmentAsync(LotSerSegmentWithNavigationPropertiesDto input)
        {
            await LotSerSegmentsAppService.DeleteAsync(input.LotSerSegment.Id);
            await GetLotSerSegmentsAsync();
        }

        private async Task CreateLotSerSegmentAsync()
        {
            try
            {
                if (await EditingLotSerSegmentValidations.ValidateAll() == false)
                {
                    return;
                }

                await LotSerSegmentsAppService.CreateAsync(EditingLotSerSegment);
                await GetLotSerSegmentsAsync();
                await CloseCreateLotSerSegmentModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditLotSerSegmentModalAsync()
        {
            await EditLotSerSegmentModal.Hide();
        }

        private async Task UpdateLotSerSegmentAsync()
        {
            try
            {
                if (await EditingLotSerSegmentValidations.ValidateAll() == false)
                {
                    return;
                }

                await LotSerSegmentsAppService.UpdateAsync(EditingLotSerSegmentId, EditingLotSerSegment);
                await GetLotSerSegmentsAsync();
                await EditLotSerSegmentModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }


        private async Task GetLotSerClassCollectionLookupAsync(string? newValue = null)
        {
            LotSerClassesCollection = (await LotSerSegmentsAppService.GetLotSerClassLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

    }
}
