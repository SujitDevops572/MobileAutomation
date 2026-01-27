using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.PurchaseOrder.ScrappedPurchaseOrder
{
    public class SearchScrappedPurchaseOrderData
    {

        public static Dictionary<string, string> searchScrappedPurchaseOrderSuccess = new Dictionary<string, string>
    {
        { "Id", "PO000200" },
        { "Machine Id", "2VE0000219" },
         { "Client Id", "CL0010" },

    };

        public static Dictionary<string, string> searchScrappedPurchaseOrderFailure = new Dictionary<string, string>
    {
        { "Id", "XXXXXBBBB" },
        { "Machine Id", "JJJJJJKKFC" },
         { "Client Id", "LBBJGJGFC" },

    };
    }
}
