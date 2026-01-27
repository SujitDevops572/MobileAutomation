using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Branch
{
    public class SearchBranchData
    {
        public static Dictionary<string, string> searchBranchSuccess = new Dictionary<string, string>
    {
            { "Branch Id","BR00240" },
        { "Name","Calicut" },
        { "Code","223" },
    };

        public static Dictionary<string, string> searchBranchFailure = new Dictionary<string, string>
    {
            { "Branch Id","zzzzzzzzzzzzzzzz" },
        { "Name","zzzzzzzzzzzzzzz" },
        { "Code","zzzzzzzzzzzzzzzzzzzzzz" },
    };
    }
}
