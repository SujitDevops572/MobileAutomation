using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Client
{
    public class EditClientData
    {
        public static Dictionary<string, string> EditClientSuccess = new Dictionary<string, string>
    {
        { "name", "Nestle3"+ DateTime.Now.ToString("yyyy-MM-dd_ss.ff") },
        { "clientId", "CL0102" }
        };

        public static Dictionary<string, string> EditClientFailure = new Dictionary<string, string>
    {
        { "name", "Nestle" },
        { "clientId", "CL0102" }
        };
    }
}
