using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.SubCategories
{
    public class EditSubCategoryData
    {
        public static Dictionary<string, string> EditSubCategorySuccess = new Dictionary<string, string>
    {
        { "name1", "xdx"+(char)new Random().Next('A', 'Z' + 1) },
            { "subCategoryId", "BIS0000005" },
             { "name", "xdx"  }
        };

        public static Dictionary<string, string> EditSubCategoryFailure = new Dictionary<string, string>
    {
        { "name", "Kas" },
            { "subCategoryId", "KAS0000088" },
             
        };
    }
}
