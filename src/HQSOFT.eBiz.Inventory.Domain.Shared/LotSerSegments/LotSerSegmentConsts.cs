namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public static class LotSerSegmentConsts
    {
        private const string DefaultSorting = "{0}SegmentID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "LotSerSegment." : string.Empty);
        }

    }
}