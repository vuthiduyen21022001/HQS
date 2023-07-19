using HQSOFT.eBiz.Inventory.ReasonCodes;
using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodeExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? ReasonCodeID { get; set; }
        public string? Descr { get; set; }
        public ReasonCodeType? Usage { get; set; }
        public string? SubMask { get; set; }
        public int? AccountIDMin { get; set; }
        public int? AccountIDMax { get; set; }
        public int? SubIDMin { get; set; }
        public int? SubIDMax { get; set; }
        public int? SalesAcctIDMin { get; set; }
        public int? SalesAcctIDMax { get; set; }
        public int? SalesSubIDMin { get; set; }
        public int? SalesSubIDMax { get; set; }

        public ReasonCodeExcelDownloadDto()
        {

        }
    }
}