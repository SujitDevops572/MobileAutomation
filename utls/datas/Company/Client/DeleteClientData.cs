using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Client
{
    public class DeleteClientData
    {
        public static Dictionary<string, string> DeleteClientSuccess = new Dictionary<string, string>
    {
        { "name", "Cadbursy" }
        };
        public static Dictionary<string, string> DeleteClientFailure = new Dictionary<string, string>
    {
        { "clientId", "CL0001" }
        };
    }
}
