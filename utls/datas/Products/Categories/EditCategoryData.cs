using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Categories
{
    public class EditCategoryData
    {
        public static Dictionary<string, string> EdiCategorySuccess = new Dictionary<string, string>
    {
        { "name", "Kasrtdgh chikr" },
            { "categoryId", "XXX0000010" },
            {"name1", "Kasrtdgh chikr"+ DateTime.Now.ToString("yyyy-MM-dd_ss.ff") }
        };

        public static Dictionary<string, string> EditCategoryFailure = new Dictionary<string, string>
    {
        { "name", "Kasbranch" },
            { "categoryId", "KAS0000077" }
        };
    }
}
