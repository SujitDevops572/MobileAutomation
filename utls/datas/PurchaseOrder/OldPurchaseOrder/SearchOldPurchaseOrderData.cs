using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.PurchaseOrder.OldPurchaseOrder
{
    public class SearchOldPurchaseOrderData
    {
        public static Dictionary<string, string> searchOldPurchaseOrderSuccess = new Dictionary<string, string>
    {
        { "Id", "PO000204" },
        { "Machine Id", "2TE0000202" },
         { "Client Id", "CL0010" },

    };

        public static Dictionary<string, string> searchOldPurchaseOrderFailure = new Dictionary<string, string>
    {
        { "Id", "XXXXXBBBB" },
        { "Machine Id", "JJJJJJKKFC" },
         { "Client Id", "LBBJGJGFC" },

    };

    }
}
