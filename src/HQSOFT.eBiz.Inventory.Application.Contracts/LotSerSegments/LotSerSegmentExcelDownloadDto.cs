using HQSOFT.eBiz.Inventory.LotSerSegments;
using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public int? SegmentIDMin { get; set; }
        public int? SegmentIDMax { get; set; }
        public Typeee? AsgmentType { get; set; }
        public string? Value { get; set; }
        public Guid? LotSerClassId { get; set; }

        public LotSerSegmentExcelDownloadDto()
        {

        }
    }
}