using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyAdmin
{
    public class AddCompanyAdminData
    {
        public static Dictionary<string, string> addCompanyAdminSuccess = new Dictionary<string, string>
    {
        { "name", "Automation Script"+ DateTime.Now.ToString("yyyy-MM-dd_ss.ff") },
        { "username", "karsadthik"+DateTime.Now.ToString("ss.ff")+"@riota.in" },
        { "email", "kartsadhik"+DateTime.Now.ToString("ss.ff")+"@riota.in" },
        { "mobile", "9087678917" },
        { "client", "new user role" },
        { "password", "karthik@123" },
        { "image", "C:\\Users\\Babu Office\\Downloads\\Group 6.png" },
    };

        public static Dictionary<string, string> addCompanyAdminFailure = new Dictionary<string, string>
    {
        { "name", "Test" },
        { "username", "karthik@riota.in" },
        { "email", "karthik@riota.in" },
        { "mobile", "9087678917" },
        { "client", "new user role" },
        { "password", "karthik@123" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
        public static Dictionary<string, string> addCompanyAdminBtnDisable = new Dictionary<string, string>
    {
        { "name", "Test" },
        { "username", "Test" },
        { "email", "Test" },
        { "mobile", "90876789176788999" },
        { "client", "new user role" },
        { "password", "ka" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
        public static Dictionary<string, string> addCompanyAdminBtnDisableWithoutRequired = new Dictionary<string, string>
    {
        { "name", "" },
        { "username", "Test" },
        { "email", "Test" },
        { "mobile", "90876789176788999" },
        { "client", "new user role" },
        { "password", "karthik@123" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
    }
}
