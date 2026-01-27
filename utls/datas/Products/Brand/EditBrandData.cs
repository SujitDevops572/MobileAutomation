using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Brand
{
    public class EditBrandData
    {
        public static Dictionary<string, string> EditBrandSuccess = new Dictionary<string, string>
    {
                { "name", "Kasrtdgh"+ DateTime.Now.ToString("yyyy-MM-dd'  'mm.ss.ff") },
                { "brandId", "BR00003" },
        };

        public static Dictionary<string, string> EditBrandFailure = new Dictionary<string, string>
    {
                { "name", "trivandrum" },
                { "branch", "Test Branch" },
                { "brandId", "TES0000058" },
        };
    }
}
