using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.ScrappedStock
{
    public class SearchScrappedStockeData
    {
        public static Dictionary<string, string> searchScrappedStockSuccess = new Dictionary<string, string>
    {
            { "Stock Id","05" },
            { "Batch Id","8" },
            { "Warehouse Id","2" },
            { "Product Id","1" },
    };

        public static Dictionary<string, string> searchScrappedStockFailure = new Dictionary<string, string>
    {
            { "Stock Id","69777777777770" },
            { "Batch Id","77777777777" },
            { "Warehouse Id","77777777777" },
            { "Product Id","77777777777" },
    };
    }
}
