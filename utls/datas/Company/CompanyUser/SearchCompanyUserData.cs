using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyUser
{
    public class SearchCompanyUserData
    {
        public static Dictionary<string, string> searchCompanyUserSuccess = new Dictionary<string, string>
    {
            { "Name", "karthi" },
        { "Email", "karthik@riota.in" },
        { "Username", "karthi" },
    };
        public static Dictionary<string, string> searchCompanyUserFailure = new Dictionary<string, string>
    {
            { "Name", "xxxxxxx" },
        { "Email", "xxxxxxxxxxx" },
        { "Username", "xxxxxxx" },
    };

    }
}
