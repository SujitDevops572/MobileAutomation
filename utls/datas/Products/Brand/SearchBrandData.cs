using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Brand
{
    public class SearchBrandData
    {
        public static Dictionary<string, string> searchBrandSuccess = new Dictionary<string, string>
    {
            { "Name","ar" },
            { "Brand Id","50" },
            { "Branch Id","37" },
            { "Branch Name","bo" },
    };

        public static Dictionary<string, string> searchBrandFailure = new Dictionary<string, string>
    {
            { "Name","zzzzzzzzzzzzzzz" },
            { "Brand Id","zzzzzzzzzzzzzzzzzzz" },
            { "Branch Id","zzzzzzzzz" },
            { "Branch Name","zzzzzzzzz" },
    };
    }
}
