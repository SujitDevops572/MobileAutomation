using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.SubCategories
{
    public class DeleteSubcategoryData
    {
        public static Dictionary<string, string> DeleteSubCategorySuccess = new Dictionary<string, string>
    {
         { "name", "Kasss" },
        };
        public static Dictionary<string, string> DeleteSubCategoryFailure = new Dictionary<string, string>
    {
        { "subCategoryId", "COC0000009" }
        };
    }
}
