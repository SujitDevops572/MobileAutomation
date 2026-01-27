using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Machines
{
    public class SearchMachineListData
    {
        public static Dictionary<string, string> MachineSearchSuccess = new Dictionary<string, string>
    {
        { "Machine Id", "2VE0000091" },
           };

        public static Dictionary<string, string> MachineSearchFailure = new Dictionary<string, string>
    {
        { "Machine Id", "xxxxxxxx" },
           };
    }
}
