using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.UserManagement.ClientUserList
{
    public class SearchClientUserListData
    {
        public static Dictionary<string, string> SearchClientUserListSuccess = new Dictionary<string, string>
    {
        { "Name", "Lavanya" },
        { "Email", "devtest@gmail.com" },
        { "Mobile", "91 1234 125 033" },
        { "Employee Id", "1191" },
        { "Client Id", "CL0110" },
        { "Client Name", "riotachittoor" },
        };
        public static Dictionary<string, string> SearchClientUserListFailure = new Dictionary<string, string>
    {
        { "Name", "XXXXXXXX94" },
        { "Email", "cBVftyf@ymail.com" },
        { "Mobile", "678VSFSG327" },
        { "Employee Id", "HHHH22" },
        { "Client Id", "KJHBvg21" },
        { "Client Name", "YYYYJFBKJBS" },
        };

        public static Dictionary<string, string> EditAccessId_ClientUserListFailure = new Dictionary<string, string>
    {
        
        { "Access id", "35"+ DateTime.Now.ToString("ssff")  },
        };
    }

}
