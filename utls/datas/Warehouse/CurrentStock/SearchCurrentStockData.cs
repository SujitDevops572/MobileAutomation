using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.CurrentStock
{
    public class SearchCurrentStockeData
    {
        public static Dictionary<string, string> searchCurrentStockSuccess = new Dictionary<string, string>
    {
            { "Stock Id","690" },
            { "Batch Id","12" },
            { "Warehouse Id","05" },
            { "Warehouse Name","dev" },
            { "Product Id","048" },
            { "Product Name","coin" },
    };

        public static Dictionary<string, string> searchCurrentStockFailure = new Dictionary<string, string>
    {
            { "Stock Id","xxxxxxx" },
            { "Batch Id","xxxxx" },
            { "Warehouse Id","xxxxxxxx" },
            { "Warehouse Name","xxxxrty" },
            { "Product Id","xcvcvcv" },
            { "Product Name","xxvxcvcv" },
    };
    }
}
