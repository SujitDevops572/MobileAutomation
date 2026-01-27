using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Advertisement.List
{
    public class SearchListData
    {
        public static Dictionary<string, string> SearchListSuccess = new Dictionary<string, string>
    {
        { "Id", "ADV00230250" },
        { "Title", "bhavya" },
        { "ContentType", "PNG" },
        { "Client Id", "CL0025" },
    };
        public static Dictionary<string, string> SearchListFailure = new Dictionary<string, string>
    {
        { "Id", "FKDSTATA" },
        { "Title", "UPIFEWQ" },
        { "ContentType", "LOGDAI" },
        { "Client Id", "YTABSH_Y" },
    };
        public static Dictionary<string, string> Deletesearch = new Dictionary<string, string>
    {
        { "Title", "Automation " },
        
    };
        public static Dictionary<string, string> SetasDefaultsearch = new Dictionary<string, string>
    {
        { "Client Id", "CL0001" },

    };
      
    }
}

