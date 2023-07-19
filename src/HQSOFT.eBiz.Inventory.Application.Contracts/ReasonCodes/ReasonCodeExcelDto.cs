using HQSOFT.eBiz.Inventory.ReasonCodes;
using System;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodeExcelDto
    {
        public string ReasonCodeID { get; set; }
        public string Descr { get; set; }
        public ReasonCodeType Usage { get; set; }
        public string? SubMask { get; set; }
        public int AccountID { get; set; }
        public int   SubID { get; set; }
        public int SalesAcctID { get; set; }
        public int  SalesSubID { get; set; }
    }
}