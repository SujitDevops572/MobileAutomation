using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Client
{
     public class SearchClientData
    {
        public static Dictionary<string, string> searchClientSuccess = new Dictionary<string, string>
    {
        { "Name","HR&CE" },
        { "Client Id","CL0142" },
        { "Branch Id","BR00001" },
        { "Branch Name","IndiQube" },

    };

        public static Dictionary<string, string> searchClientFailure = new Dictionary<string, string>
    {
        { "Name","zzzzzzzzzzzzzzzzzzzzzzzz" },
        { "Client Id","zzzzzzzzz" },
        { "Branch Id","zzzzzzzzzzz" },
        { "Branch Name","zzzzzzzzzz" },
    };
    }
}
