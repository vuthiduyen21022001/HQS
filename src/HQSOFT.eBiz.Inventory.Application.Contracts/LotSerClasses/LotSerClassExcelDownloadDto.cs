using HQSOFT.eBiz.Inventory.LotSerClasses;
using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClassExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? ClassID { get; set; }
        public string? Description { get; set; }
        public TrackingMethod? TrackingMethod { get; set; }
        public bool? TrackExpriationDate { get; set; }
        public bool? RequiredforDropShip { get; set; }
        public AssignMethod? AssignMethod { get; set; }
        public IssueMethod? IssueMethod { get; set; }
        public bool? ShareAutoIncremenitalValueBetwenAllClassItems { get; set; }
        public int? AutoIncremetalValueMin { get; set; }
        public int? AutoIncremetalValueMax { get; set; }
        public bool? AutoGenerateNextNumber { get; set; }
        public int? MaxAutoNumbersMin { get; set; }
        public int? MaxAutoNumbersMax { get; set; }

        public LotSerClassExcelDownloadDto()
        {

        }
    }
}