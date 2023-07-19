using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClassCreateDto
    {
        [Required]
        public string ClassID { get; set; }
        [Required]
        public string Description { get; set; }
        public TrackingMethod TrackingMethod { get; set; } = ((TrackingMethod[])Enum.GetValues(typeof(TrackingMethod)))[0];
        public bool TrackExpriationDate { get; set; } = false;
        public bool RequiredforDropShip { get; set; } = false;
        public AssignMethod AssignMethod { get; set; } = ((AssignMethod[])Enum.GetValues(typeof(AssignMethod)))[0];
        public IssueMethod IssueMethod { get; set; } = ((IssueMethod[])Enum.GetValues(typeof(IssueMethod)))[0];
        public bool ShareAutoIncremenitalValueBetwenAllClassItems { get; set; } = false;
        public int AutoIncremetalValue { get; set; }
        public bool AutoGenerateNextNumber { get; set; } = false;
        public int MaxAutoNumbers { get; set; }
    }
}