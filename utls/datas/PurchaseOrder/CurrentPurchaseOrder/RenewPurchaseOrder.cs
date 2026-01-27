using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.PurchaseOrder.CurrentPurchaseOrder
{
    public class RenewCurrentPurchaseOrder
    {
        public static Dictionary<string, string> RenewCurrentPurchaseOrderBill = new Dictionary<string, string>
    {
        { "date", "5" },
        { "month", "aug" },
         { "year", "2025" },
    };
        public static Dictionary<string, string> RenewCurrentPurchaseOrderExpiry = new Dictionary<string, string>
    {
         { "date", "10" },
        { "month", "sept" },
         { "year", "2026" },

    };

    }
}
