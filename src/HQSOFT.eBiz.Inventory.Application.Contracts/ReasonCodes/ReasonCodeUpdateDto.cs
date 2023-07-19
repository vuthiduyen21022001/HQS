using HQSOFT.eBiz.Inventory.ReasonCodes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodeUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string ReasonCodeID { get; set; }
        [Required]
        public string Descr { get; set; }
        public ReasonCodeType Usage { get; set; }
        //[StringLength(ReasonCodeConsts.SubMaskMaxLength, MinimumLength = ReasonCodeConsts.SubMaskMinLength)]
        [StringLength(6, ErrorMessage = "Độ dài tên phải là 6 kí tự.")]
        public string? SubMask { get; set; }
        public int AccountID { get; set; }
        public int SubID { get; set; }
        public int SalesAcctID { get; set; }
        public int SalesSubID { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}