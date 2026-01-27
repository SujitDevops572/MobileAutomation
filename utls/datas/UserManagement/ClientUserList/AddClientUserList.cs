using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.UserManagement.ClientUserList
{
    public class AddClientUserListData
    {
        public static Dictionary<string, string> AddClientUserListSuccess = new Dictionary<string, string>
    {
            { "client","bhavu" },
            { "Group","Automation" },
            { "VName", "bhavu Automations" },
        { "Name", "kavin Automationss" },
        { "Email", "kavin3@gmail.com" },
        { "Mobile", "91 1434 145 038" },
        { "Employee Id", "13342" },
        { "Password", "kavin3@133" },
        {"Access Id" , "4443311"},
        { "IMobile", "FFF3344525" },
        { "", " " },
    };
        public static Dictionary<string, string> AddNewValidationGroupList = new Dictionary<string, string>
    {
        { "Name", "kavin groups" },

         { "hours", "01" },
          { "mins", "10" },
          { "rhours", "01" },
          { "rmins", "15" },
            {"qty","50" },
        };


    }
}
