using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.ReturnedStock
{
    public class SearchReturnedStockeData
    {
        public static Dictionary<string, string> searchReturnedStockSuccess = new Dictionary<string, string>
    {
            { "Stock Id","204" },
            { "Batch Id","81" },
            { "Warehouse Id","24" },
            { "Product Id","154" },
            { "Product Name","Dark" },
            { "Machine Id","30" },
    };

        public static Dictionary<string, string> searchReturnedStockFailure = new Dictionary<string, string>
    {
            { "Stock Id","69777777777770" },
            { "Batch Id","77777777777" },
            { "Warehouse Id","77777777777" },
            { "Product Id","77777777777" },
            { "Product Name","xxxxxcxzc" },
            { "Machine Id","77777777777" },
    };
    }
}
