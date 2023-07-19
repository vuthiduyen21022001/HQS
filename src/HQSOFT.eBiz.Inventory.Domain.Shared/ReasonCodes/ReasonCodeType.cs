using System;
using System.Collections.Generic;
using System.Text;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public enum ReasonCodeType
    {
        Transfer = 1,
        CreditWriteOff = 2,
        BalanceWriteOff = 3,
        Issue = 4,
        Receipt = 5,
        Adjustment = 6,
        Sales = 7,
        VendorReturn = 8,
        AssemblyDisassembly = 9

    }
}
