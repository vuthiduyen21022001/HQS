using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.AspNetCore.Components.Messages;
using DevExpress.Blazor;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Data.Linq;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using HQSOFT.eBiz.Inventory.Permissions;
using HQSOFT.eBiz.Inventory.Shared;
using Microsoft.AspNetCore.Components;

namespace HQSOFT.eBiz.Inventory.Blazor.Pages.Inventory.ReasonCode
{
    public partial class ReasonCodeListView
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<ReasonCodeDto> ReasonCodeList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateReasonCode { get; set; }
        private bool CanEditReasonCode { get; set; }
        private bool CanDeleteReasonCode { get; set; }
        private GetReasonCodesInput Filter { get; set; }
        private DataGridEntityActionsColumn<ReasonCodeDto> EntityActionsColumn { get; set; } = new();

        private List<ReasonCodeDto> selectedReasonCode;

        private readonly IUiMessageService _uiMessageService;
        private IGrid ReasonCodeGrid { get; set; }
        private ReasonCodeDto CurrentReasonCode { get; set; }


        public ReasonCodeListView(IUiMessageService uiMessageService)
        {
            Filter = new GetReasonCodesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            ReasonCodeList = new List<ReasonCodeDto>();
            selectedReasonCode = new List<ReasonCodeDto>();
            _uiMessageService = uiMessageService;

        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetReasonCodesAsync();

        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:ReasonCodes"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () => { await DownloadAsExcelAsync(); }, IconName.Download);

            Toolbar.AddButton(L["New"], () =>
            {
                NavigationManager.NavigateTo($"Inventory/ReasonCodes/{Guid.Empty}");
                return Task.CompletedTask;
            }, IconName.Add, requiredPolicyName: InventoryPermissions.ReasonCodes.Create);

            Toolbar.AddButton(L["Delete"], async () =>
            {
                if (selectedReasonCode.Count > 0)
                {
                    var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                    if (confirmed)
                    {
                        foreach (ReasonCodeDto selectedorderType in selectedReasonCode)
                        {
                            await ReasonCodesAppService.DeleteAsync(selectedorderType.Id);
                        }
                        await GetReasonCodesAsync();
                    }

                }
            }, IconName.Delete,
            Color.Danger,
            requiredPolicyName: InventoryPermissions.ReasonCodes.Delete);
            return ValueTask.CompletedTask;
        }

        protected void GoToEditPage(ReasonCodeDto context)
        {
            NavigationManager.NavigateTo($"Inventory/ReasonCodes/{context.Id}");
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateReasonCode = await AuthorizationService
                .IsGrantedAsync(InventoryPermissions.ReasonCodes.Create);
            CanEditReasonCode = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.ReasonCodes.Edit);
            CanDeleteReasonCode = await AuthorizationService
                            .IsGrantedAsync(InventoryPermissions.ReasonCodes.Delete);
        }

        private async Task GetReasonCodesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;
            var result = await ReasonCodesAppService.GetListAsync(Filter);
            ReasonCodeList = result.Items;
            TotalCount = (int)result.TotalCount;
            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetReasonCodesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task DownloadAsExcelAsync()
        {
            var token = (await ReasonCodesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("OM") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/o-m/order-types/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ReasonCodeDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetReasonCodesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnPageIndexChanged(int newPageIndex)
        {
            //CurrentSorting = OrderTypeListGrid.Data. .Columns
            //    .Where(c => c.SortDirection != SortDirection.Default)
            //    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            //    .JoinAsString(",");
            CurrentPage = newPageIndex;
            await GetReasonCodesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task DeleteOrderTypeAsync(ReasonCodeDto input)
        {
            await ReasonCodesAppService.DeleteAsync(input.Id);
            await GetReasonCodesAsync();
        }


    }
}
