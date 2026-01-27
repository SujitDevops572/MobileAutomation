using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.UserManagement.ClientAdminList
{
    public class AddClientAdminData
    {

        public static Dictionary<string, string> AddClientAdminSuccess = new Dictionary<string, string>
    {
        { "Name", "Automation"},
        { "Username", "auto" + new Random().Next(10000, 100000)},
        { "Email", "auto1"+ new Random().Next(10000, 100000)+"@gmail.com"},
        { "Mobile", "+91 88409" + new Random().Next(10000, 100000)},
        { "Password", "12345678" },
        { "Client", "HR&CE" },
        {"upload",@"D:\projects\VMS_automations\VMS-WebUI-AT\utls\Files\kindpng_863344.png" },
        };
        

    }
}
