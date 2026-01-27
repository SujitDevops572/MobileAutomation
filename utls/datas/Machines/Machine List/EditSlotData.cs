using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    public class EditSlotData
    {
        public static Dictionary<string, string> EditSlotSuccess = new Dictionary<string, string>
    {
        { "TotalSlotRowCount", "10" },
        { "TotalColumnRowCountCount", "10" },
        { "StartingRow", "1" },
         { "StartingColumn", "1" },
        { "EndingRow", "10" },
        { "EndingColumn", "10" },
        };
        public static Dictionary<string, string> EditSlotSuccess1 = new Dictionary<string, string>
        {
            { "TotalCount", "10" },
            { "UnitColumnCount", "10" }
            };
    }
}
