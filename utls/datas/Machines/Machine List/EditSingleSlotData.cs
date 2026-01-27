using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    public class EditSingleSlotData
    {
        //2VE0000216
        public static Dictionary<string, string> EditSingleSlotSuccess = new Dictionary<string, string>
    {
        { "purchaseLimitPerUser", "1" },
        { "purchaseLimitPerTransaction", "10" },
         { "machine id"  ,"STE0000031" },
        };
        public static Dictionary<string, string> EditSingleSlotSuccess1 = new Dictionary<string, string>
    {
        { "purchaseLimitPerUser", "2" },
        { "purchaseLimitPerTransaction", "10" }
        };
    }
}
