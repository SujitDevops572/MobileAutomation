using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Branch
{
    public class DeleteBranchData
    {
        public static Dictionary<string, string> DeleteBranchSuccess = new Dictionary<string, string>
    {
        { "Name", "Pudukkottai Branch1" }
        };
        public static Dictionary<string, string> DeleteBranchFailure = new Dictionary<string, string>
    {
        { "branchId", "BR00001" }
        };
    }
}
