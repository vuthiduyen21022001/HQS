namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public static class LotSerClassConsts
    {
        private const string DefaultSorting = "{0}ClassID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "LotSerClass." : string.Empty);
        }

    }
}