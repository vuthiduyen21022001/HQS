using HQSOFT.eBiz.Inventory.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HQSOFT.eBiz.Inventory.Permissions;

public class InventoryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InventoryPermissions.GroupName, L("Permission:Inventory"));

        var reasonCodePermission = myGroup.AddPermission(InventoryPermissions.ReasonCodes.Default, L("Permission:ReasonCodes"));
        reasonCodePermission.AddChild(InventoryPermissions.ReasonCodes.Create, L("Permission:Create"));
        reasonCodePermission.AddChild(InventoryPermissions.ReasonCodes.Edit, L("Permission:Edit"));
        reasonCodePermission.AddChild(InventoryPermissions.ReasonCodes.Delete, L("Permission:Delete"));

        var lotSerClassPermission = myGroup.AddPermission(InventoryPermissions.LotSerClasses.Default, L("Permission:LotSerClasses"));
        lotSerClassPermission.AddChild(InventoryPermissions.LotSerClasses.Create, L("Permission:Create"));
        lotSerClassPermission.AddChild(InventoryPermissions.LotSerClasses.Edit, L("Permission:Edit"));
        lotSerClassPermission.AddChild(InventoryPermissions.LotSerClasses.Delete, L("Permission:Delete"));

        var lotSerSegmentPermission = myGroup.AddPermission(InventoryPermissions.LotSerSegments.Default, L("Permission:LotSerSegments"));
        lotSerSegmentPermission.AddChild(InventoryPermissions.LotSerSegments.Create, L("Permission:Create"));
        lotSerSegmentPermission.AddChild(InventoryPermissions.LotSerSegments.Edit, L("Permission:Edit"));
        lotSerSegmentPermission.AddChild(InventoryPermissions.LotSerSegments.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InventoryResource>(name);
    }
}