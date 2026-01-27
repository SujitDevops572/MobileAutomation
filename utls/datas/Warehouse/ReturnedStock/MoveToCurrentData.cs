using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.ReturnedStock
{
    public class MoveToCurrentStockData
    {
        public static Dictionary<string, string> MergeCurrentStockSuccess = new Dictionary<string, string>
        {
            { "Stock Id", "207" },
        };

        public static Dictionary<string, string> MergeCurrentStockFailure = new Dictionary<string, string>
        {
            { "Stock Id", "000695" },
        };
    }
}
