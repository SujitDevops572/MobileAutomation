using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Advertisement.Mapping
{
    public class SearchMappingData
    {
        public static Dictionary<string, string> SearchMappingSuccess = new Dictionary<string, string>
    {
        { "Machine Id", "STE0000030" },
        { "Adv. Name", "pongal adv test" },

    };
        public static Dictionary<string, string> SearchMappingFailure = new Dictionary<string, string>
    {
       { "Machine Id", "SONUNG" },
        { "Adv. Name", "RGEBFNRGB" },

    };

        public static Dictionary<string, string> SearchScheduleSuccess = new Dictionary<string, string>
    {

        { "Client Id", "CL0003" },
            { "Adv. Name","Automation"},
               { "search","Automation12345"},
            {"Machine Id",       "LLO0000049"},
    };




    }
}
