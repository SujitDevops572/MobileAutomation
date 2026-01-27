using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Branch
{
    public class importBranchData
    {
        public static Dictionary<string, string> ImportBranchSuccess = new Dictionary<string, string>
    {
        { "inputfile","D:\\projects\\VMS_automations\\VMS-WebUI-AT\\utls\\Files\\Branches_at_2025-09-30 12_51_31.xlsx" },
    };

        public static Dictionary<string, string> ImportBranchMissingColumn = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\ashin\\Downloads\\Branches_at_2024-10-03 05_34_58 (2).xlsx" },
    };
        

            public static Dictionary<string, string> ImportBranchFailure = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\ashin\\Downloads\\Branches_at_2024-10-03 05_34_58 (6).xlsx" },
    };
    }
}
