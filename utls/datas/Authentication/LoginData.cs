using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls
{
    public class LoginData
    {
        public static Dictionary<string, string> CompanyAdminSuccess = new Dictionary<string, string>
    {
        { "Username", "vendomatic1" },
        { "Password", "VENDOMATIc123" }
    };
        public static Dictionary<string, string> CompanyAdminFailure = new Dictionary<string, string>
    {
        { "Username", "vendomatic12" },
        { "Password", "VENDOMATIc123" }
    };
        public static Dictionary<string, string> CompanyUserSuccess = new Dictionary<string, string>
    {
        { "Username", "Basha@gmail.com" },
        { "Password", "12345678" }
    };
        public static Dictionary<string, string> CompanyUserFailure = new Dictionary<string, string>
    {
        { "Username", "Basha@gmail.com" },
        { "Password", "123457899" }
    };
        public static Dictionary<string, string> ClientAdminSuccess = new Dictionary<string, string>
    {
        { "Username", "sowmya111" },
        { "Password", "sowmya@123" }
    };
        public static Dictionary<string, string> ClientAdminFailure = new Dictionary<string, string>
    {
        { "Username", "sowmya555" },
        { "Password", "sowmya@123" }
    };
        public static Dictionary<string, string> addClientAdminSuccess = new Dictionary<string, string>
    {
        { "name", "Babu" },
        { "username", "Babu VJ" },
        { "email", "babu@riota.in" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
        { "password", "babu@123" }
    };
    }


}
