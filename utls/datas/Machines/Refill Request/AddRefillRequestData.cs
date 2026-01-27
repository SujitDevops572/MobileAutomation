using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    public class AddRefillRequestData
    {
        
        public static Dictionary<string, string> addRequestSuccess = new Dictionary<string, string>
    {
        { "machineIds", "1VE0000212" },
            { "SearchOption","Machine Id"}
        };
        public static Dictionary<string, string> addRequestBtnDisable = new Dictionary<string, string>
    {
        { "machineIds", "NVE0000035" },
        };
    }
}
