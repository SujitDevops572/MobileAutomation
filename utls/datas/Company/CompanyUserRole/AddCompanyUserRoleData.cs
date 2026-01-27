using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyUserRole
{
    public class AddCompanyUserRoleData
    {
        public static Dictionary<string, string> addCompanyUserRoleSuccess = new Dictionary<string, string>
    {
        { "role", "Automation "+(char)new Random().Next('A','Z' + 1)+(char)new Random().Next('A','Z' + 1) },
        

    };
        public static Dictionary<string, string> addCompanyUserRoleDuplicate = new Dictionary<string, string>
    {
        { "role", "new role test" },

    };

    }
}
