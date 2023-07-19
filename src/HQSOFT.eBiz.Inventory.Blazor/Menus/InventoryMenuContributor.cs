using HQSOFT.eBiz.Inventory.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using HQSOFT.eBiz.Inventory.Localization;
using Volo.Abp.UI.Navigation;

namespace HQSOFT.eBiz.Inventory.Blazor.Menus;

public class InventoryMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemReasonCodes(context, moduleMenu);

        AddMenuItemLotSerClasses(context, moduleMenu);

        AddMenuItemLotSerSegments(context, moduleMenu);
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<InventoryResource>();

        context.Menu.AddItem(new ApplicationMenuItem(InventoryMenus.Prefix, displayName: "Sample Page", "/Inventory", icon: "fa fa-globe"));

        await Task.CompletedTask;
    }
    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            InventoryMenus.Prefix,
            context.GetLocalizer<InventoryResource>()["Menu:Inventory"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemReasonCodes(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.InventoryMenus.ReasonCodes,
                context.GetLocalizer<InventoryResource>()["Menu:ReasonCodes"],
                "/Inventory/ReasonCodes",
                icon: "fa fa-file-alt",
                requiredPermissionName: InventoryPermissions.ReasonCodes.Default
            )
        );
    }

    private static void AddMenuItemLotSerClasses(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.InventoryMenus.LotSerClasses,
                context.GetLocalizer<InventoryResource>()["Menu:LotSerClasses"],
                "/Inventory/LotSerClasses",
                icon: "fa fa-file-alt",
                requiredPermissionName: InventoryPermissions.LotSerClasses.Default
            )
        );
    }

    private static void AddMenuItemLotSerSegments(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.InventoryMenus.LotSerSegments,
                context.GetLocalizer<InventoryResource>()["Menu:LotSerSegments"],
                "/Inventory/LotSerSegments",
                icon: "fa fa-file-alt",
                requiredPermissionName: InventoryPermissions.LotSerSegments.Default
            )
        );
    }
}