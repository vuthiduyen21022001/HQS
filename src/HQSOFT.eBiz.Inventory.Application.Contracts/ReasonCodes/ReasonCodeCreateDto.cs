using HQSOFT.eBiz.Inventory.ReasonCodes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodeCreateDto
    {
        [Required]
        public string ReasonCodeID { get; set; }
        [Required]
        public string Descr { get; set; }
        public ReasonCodeType Usage { get; set; } = ((ReasonCodeType[])Enum.GetValues(typeof(ReasonCodeType)))[0];
        public string? SubMask { get; set; }
        public int AccountID { get; set; }
        public int SubID { get; set; }
        public int SalesAcctID { get; set; }
        public int SalesSubID { get; set; }
    }
}