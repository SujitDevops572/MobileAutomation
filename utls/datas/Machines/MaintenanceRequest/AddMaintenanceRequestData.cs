using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Machines.MaintenanceRequest
{
    public class AddMaintenanceRequestData
    {
        public static Dictionary<string, string> addMaintenacnRequestSuccess = new Dictionary<string, string>
    {
        { "machineIds", "2VE0000185" },
        { "issues", "testing" },
        };

        public static Dictionary<string, string> addMaintenacnRequestFailure = new Dictionary<string, string>
    {
        { "machineIds", "2TE0000003" },
        { "issues", "testing" },
        };

    }
}
