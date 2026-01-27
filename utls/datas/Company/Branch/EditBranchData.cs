using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Branch
{
    public class EditBranchData
    {
        public static Dictionary<string, string> EditBranchSuccess = new Dictionary<string, string>
    {
        { "branchId", "BR00074" },
        { "name", "trivandrum - " + DateTime.Now.ToString("yyyy-MM-dd_ss.ff") }
        };

        public static Dictionary<string, string> EditBranchFailure = new Dictionary<string, string>
    {
         { "branchId", "BR00074" },
        { "name", "Test Branch" }
        };
    }
}
