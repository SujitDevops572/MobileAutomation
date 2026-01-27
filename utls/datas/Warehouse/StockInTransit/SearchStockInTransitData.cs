using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.StockInTransit
{
    public class SearchStockInTransitData
    {
        public static Dictionary<string, string> searchStockInTransitSuccess = new Dictionary<string, string>
    {
            { "Transit Id","704" },
            { "Batch Id","6" },
            { "Warehouse Id","0000003" },
            { "Product Id","64" },
    };

        public static Dictionary<string, string> searchStockInTransitFailure = new Dictionary<string, string>
    {
            { "Transit Id","xxxxxxx" },
            { "Batch Id","xxxxx" },
            { "Warehouse Id","xxxxxxxx" },
            { "Product Id","xcvcvcv" },
    };
    }
}
