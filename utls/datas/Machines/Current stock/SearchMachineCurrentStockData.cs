using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    public class SearchMachineCurrentStockData
    {
        public static Dictionary<string, string> searchCurrentStockSuccess = new Dictionary<string, string>
    {
            { "Machine Id","54" },
            { "Product Id","27" },
            { "Batch Id","464545" },
            { "Stock Id","MST0000493" },
    };

        public static Dictionary<string, string> searchCurrentStockFailure = new Dictionary<string, string>
    {
            { "Machine Id","xxxxxxx" },
            { "Product Id","xxxxx" },
            { "Batch Id","xxxxxxxx" },
            { "Stock Id","xxxxrty" },
    };
    }
}
