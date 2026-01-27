using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyUserRole
{
    public class UpdateMenuAccessData
    {
        public static Dictionary<string, string> UpdateMenuAccessSuccess = new Dictionary<string, string>
    {
         { "Role", "new role test" },

        };

        public static Dictionary<string, string> UpdateMenuAccessBtnDisable = new Dictionary<string, string>
    {{ "Role", "new role test" },
        };
    }

}
