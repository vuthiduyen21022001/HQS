using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClassDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ClassID { get; set; }
        public string Description { get; set; }
        public TrackingMethod TrackingMethod { get; set; }
        public bool TrackExpriationDate { get; set; }
        public bool RequiredforDropShip { get; set; }
        public AssignMethod AssignMethod { get; set; }
        public IssueMethod IssueMethod { get; set; }
        public bool ShareAutoIncremenitalValueBetwenAllClassItems { get; set; }
        public int AutoIncremetalValue { get; set; }
        public bool AutoGenerateNextNumber { get; set; }
        public int MaxAutoNumbers { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}