namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public static class ReasonCodeConsts
    {
        private const string DefaultSorting = "{0}ReasonCodeID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ReasonCode." : string.Empty);
        }

    }
}