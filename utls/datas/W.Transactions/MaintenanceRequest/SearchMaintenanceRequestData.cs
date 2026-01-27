using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.W.Transactions.MaintenanceRequest
{
    public class SearchMaintenanceRequestData
    {
        public static Dictionary<string, string> searchMaintenanceRequestSuccess = new Dictionary<string, string>
    {
            { "Request Id","MREQ0000104" },
            { "Machine Id","2VE0000092" },
            { "Warehouse Id","JPH0000003" },
            { "Created By","Auto Generated" },
            { "Status","Closed" },
          
    };

        public static Dictionary<string, string> searchMaintenanceRequestFailure = new Dictionary<string, string>
    {
            { "Request Id","xxxxxxx" },
            { "Machine Id","xxxxx" },
            { "Warehouse Id","xxxxxxxx" },
            { "Created By","xxxxrty" },
            { "Status","xxxxxxxx" },
    };
    }
}
