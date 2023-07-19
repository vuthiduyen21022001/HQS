using Volo.Abp.Reflection;

namespace HQSOFT.eBiz.Inventory.Permissions;

public class InventoryPermissions
{
    public const string GroupName = "Inventory";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InventoryPermissions));
    }

    public static class ReasonCodes
    {
        public const string Default = GroupName + ".ReasonCodes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class LotSerClasses
    {
        public const string Default = GroupName + ".LotSerClasses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class LotSerSegments
    {
        public const string Default = GroupName + ".LotSerSegments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}