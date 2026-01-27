using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.W.Transactions.RefillRequest
{
    public class SearchRefillRequestData
    {
        public static Dictionary<string, string> searchRefillRequestSuccess = new Dictionary<string, string>
    {
            { "Request Id","REQ0000781" },
            { "Machine Id","2VE0000091" },
            { "Warehouse Id","JPH0000003" },
            { "Created By","vendomatic1" },
            { "Status","Completed" },
    };

        public static Dictionary<string, string> searchRefillRequestFailure = new Dictionary<string, string>
    {
            { "Request Id","xxxxxxx" },
            { "Machine Id","xxxxx" },
            { "Warehouse Id","xxxxxxxx" },
            { "Created By","xxxxrty" },
            { "Status","xxxx" },
    };
    }
}
