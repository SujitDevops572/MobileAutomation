using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyAdmin
{
    public class CompanyAdminResetPassword
    {
        public static Dictionary<string, string> resetPasswordSuccess = new Dictionary<string, string>
    {
        { "newPassword", "Test@123" },

    };
        public static Dictionary<string, string> resetPasswordFailure = new Dictionary<string, string>
    {
        { "newPassword", "" },

    };
    }
}
