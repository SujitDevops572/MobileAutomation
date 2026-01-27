using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.PurchaseOrder.CurrentPurchaseOrder
{
    public class SearchCurrentPurchaseOrderData
    {

        public static Dictionary<string, string> searchCurrentPurchaseOrderSuccess = new Dictionary<string, string>
    {
        { "Id", "PO000190" },
        { "Machine Id", "2VE0000218" },
         { "Client Id", "CL0010" },

    };
    
     public static Dictionary<string, string> searchCurrentPurchaseOrderFailure = new Dictionary<string, string>
    {
        { "Id", "XXXXXBBBB" },
        { "Machine Id", "JJJJJJKKFC" },
         { "Client Id", "LBBJGJGFC" },

    };
    }
    }
