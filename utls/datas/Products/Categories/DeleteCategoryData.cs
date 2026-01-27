using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Categories
{
    public class DeletecategoryData
    {
        public static Dictionary<string, string> DeleteCategorySuccess = new Dictionary<string, string>
    {
        { "Name", "Ksasss xss" }
        };
        public static Dictionary<string, string> DeleteCategoryFailure = new Dictionary<string, string>
    {
        { "categoryId", "BIS0000049" }
        };
    }
}
