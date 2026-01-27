using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyUserRole
{
    public class UpdateFeatureAccessData
    {
        public static Dictionary<string, string> UpdateFeatureAccessSuccess = new Dictionary<string, string>
    {
         { "Role", "new role test" },
        
        };

        public static Dictionary<string, string> UpdateFeatureAccessBtnDisable = new Dictionary<string, string>
    {{ "Role", "new role test" },
        };
    }
}
