using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    public class AddReturnRequestData
    {
        public static Dictionary<string, string> addReturnRequestSuccess = new Dictionary<string, string>
    {
        { "machineIds", "2VE0000048" },
        };
        public static Dictionary<string, string> addReturnRequestFailure = new Dictionary<string, string>
    {
        { "machineIds", "2TE0000005" },
        };
    }
}
