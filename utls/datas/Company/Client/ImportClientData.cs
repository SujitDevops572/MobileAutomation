using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Client
{
    public class ImportClientData
    {
        public static Dictionary<string, string> ImportClientSuccess = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\ashin\\Downloads\\Clients_at_2024-10-01 11_12_17.xlsx" },
    };

        public static Dictionary<string, string> ImportClientMissingColumn = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\ashin\\Downloads\\Clients_at_2024-10-01 11_42_54.xlsx" },
    };

        public static Dictionary<string, string> ImportClientFailure = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\ashin\\Downloads\\Clients_at_2024-10-01 11_12_17 (1).xlsx" },
    };
    }
}
