using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyAdmin
{
    public class SearchCompanyAdminData
    {
        public static Dictionary<string, string> searchCompanyAdminSuccess = new Dictionary<string, string>
    {
            { "Name","kesavan" },
            { "Email","vendomatic2@gmail.com" },
            { "Username","Rocka761" }
    };

        public static Dictionary<string, string> searchCompanyAdminFailure = new Dictionary<string, string>
    {
        { "Name","xxxxxxxx" },
        { "Email","xxxx" },
        { "Username","xxxxxxxxx" }
    };
    }
}
