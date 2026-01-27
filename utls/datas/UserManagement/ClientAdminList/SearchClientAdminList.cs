using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.UserManagement.ClientAdminList
{
    public class SearchClientAdminListData
    {
        public static Dictionary<string, string> SearchClientAdminListSuccess = new Dictionary<string, string>
    {
        { "Name", "Lavanya" },
        { "Email", "chinnu@gmail.com" },
        { "Mobile", "+91 78409 47857" },
        { "Username", "Anshi" },
        { "Client Id", "CL0110" },
        { "Client Name", "riotachittoor" },
        };
        public static Dictionary<string, string> SearchClientAdminListFailure = new Dictionary<string, string>
    {
        { "Name", "FFDDDCVBFD" },
        { "Email", "FCNSGVC@.com" },
        { "Mobile", "0000000000" },
        { "Username", "GHNVDQQWE" },
        { "Client Id", "XXX000" },
        { "Client Name", "LLOOKIMJN" },
        };

        public static Dictionary<string, string> ResetClientAdminList = new Dictionary<string, string>
      {
          { "reset", "A12345e"+new Random().Next(1, 9) },
          
      };
    }
}