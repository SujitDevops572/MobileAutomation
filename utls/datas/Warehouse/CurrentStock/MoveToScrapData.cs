using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.CurrentStock
{
    public class MoveToScrapData
    {
        public static Dictionary<string, string> MoveToScrapSuccess = new Dictionary<string, string>
        {
            { "Stock Id", "001" },
        };

        public static Dictionary<string, string> MoveToScrapFailure = new Dictionary<string, string>
        {
            { "Stock Id", "203" },
        };
    }
}
