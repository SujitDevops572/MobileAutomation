using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.UserManagement.ClientUserList
{
    public class EditClientUserListData
    {
        public static Dictionary<string, string> EditClientUserList = new Dictionary<string, string>
    {
        { "Name", "Edited" +(char)new Random().Next('A', 'Z' + 1)},
        { "Email", "edited_123"+new Random().Next(1, 9) +"@gmail.com" },
        { "Mobile", "91 1934 18"+new Random().Next(1, 9) +" 037" },
            {"qty", "20" },
            {"ehours", "02" },
            {"emins", "20" },
            { "search" , "Automation user"},
             { "search edit" , "Edited"},
        };

    }
}
