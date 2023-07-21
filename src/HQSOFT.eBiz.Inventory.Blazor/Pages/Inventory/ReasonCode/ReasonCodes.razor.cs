using Blazorise;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using HQSOFT.eBiz.Inventory.Permissions;
using HQSOFT.eBiz.Inventory.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace HQSOFT.eBiz.Inventory.Blazor.Pages.Inventory.ReasonCode
{
    public partial class ReasonCodes
    {
        [Parameter]
        public string Id { get; set; }

        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<ReasonCodeDto> ReasonCodeList { get; set; }
        private bool CanCreateReasonCode { get; set; }
        private bool CanEditReasonCode { get; set; }
        private bool CanDeleteReasonCode { get; set; }
        private readonly IUiMessageService _uiMessageService;
        private ReasonCodeUpdateDto EditingReasonCode { get; set; }
        private Validations EditingReasonCodeValidations { get; set; } = new();
        private Guid EditingReasonCodeId { get; set; }
        private Modal CreateReasonCodeModal { get; set; } = new();
        private Modal EditReasonCodeModal { get; set; } = new();



        public ReasonCodes(IUiMessageService uiMessageService)
        {
            EditingReasonCode = new ReasonCodeUpdateDto();
            EditingReasonCode.ConcurrencyStamp = string.Empty;
            _uiMessageService = uiMessageService;
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            EditingReasonCodeId = Guid.Parse(Id);
            if (EditingReasonCodeId != Guid.Empty)
            {
                var reasonCode = await ReasoncodesAppService.GetAsync(EditingReasonCodeId);
                EditingReasonCode = ObjectMapper.Map<ReasonCodeDto, ReasonCodeUpdateDto>(reasonCode);
            }
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

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:ReasonCodes"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["Back"], () =>
            {
                NavigationManager.NavigateTo($"Inventory/ReasonCodes");
                return Task.CompletedTask;
            },
           IconName.Undo,
           Color.Secondary);

            Toolbar.AddButton(L["Save"], async () =>
            {
                await SaveReasonCodeAsync(false);
            },
            IconName.Save,
            Color.Primary,
            requiredPolicyName: InventoryPermissions.ReasonCodes.Edit);

            Toolbar.AddButton(L["New"], async () =>
            {
                await SaveReasonCodeAsync(true);
            }, IconName.Add, requiredPolicyName: InventoryPermissions.ReasonCodes.Create);

            Toolbar.AddButton(L["Delete"], async () =>
            {
                var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                if (confirmed)
                {
                    await DeleteReasonCodeAsync(EditingReasonCodeId);
                }
            },
            IconName.Delete,
            Color.Danger,
            requiredPolicyName: InventoryPermissions.ReasonCodes.Delete);

            return ValueTask.CompletedTask;
        }

        private async Task CreateReasonCodeAsync()
        {
            try
            {
                if (await EditingReasonCodeValidations.ValidateAll() == false)
                {
                    return;
                }

                var reasoncode = await ReasoncodesAppService.CreateAsync(EditingReasonCode);
                EditingReasonCodeId = reasoncode.Id;
                EditingReasonCode = ObjectMapper.Map<ReasonCodeDto, ReasonCodeUpdateDto>(reasoncode);
                NavigationManager.NavigateTo($"Inventory/ReasonCodes/{EditingReasonCodeId}");
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task UpdateReasonCodeAsync()
        {
            try
            {
                if (await EditingReasonCodeValidations.ValidateAll() == false)
                {
                    return;
                }
                await ReasoncodesAppService.UpdateAsync(EditingReasonCodeId, EditingReasonCode);
                var reasoncode = await ReasoncodesAppService.GetAsync(EditingReasonCodeId);
                EditingReasonCode = ObjectMapper.Map<ReasonCodeDto, ReasonCodeUpdateDto>(reasoncode);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task SaveReasonCodeAsync(bool isNewNext)
        {
            try
            {
                if (EditingReasonCodeId == Guid.Empty)
                {
                    await CreateReasonCodeAsync();
                }
                else
                {
                    await UpdateReasonCodeAsync();
                }
                if (isNewNext)
                {
                
                    NavigationManager.NavigateTo($"Inventory/ReasonCodes/{Guid.Empty}", true);

                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task DeleteReasonCodeAsync(Guid Id)
        {
            await ReasoncodesAppService.DeleteAsync(Id);
            NavigationManager.NavigateTo("Inventory/ReasonCodes");
        }


        //========chữ hoa====

        private void OnClassIDInput(ChangeEventArgs e)
        {
            string inputValue = e.Value?.ToString() ?? string.Empty;
            EditingReasonCode.ReasonCodeID = inputValue.ToUpper();
        }
    }
}
