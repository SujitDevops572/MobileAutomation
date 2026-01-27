using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.ProductList
{
    public class DeleteBranchLevelProductData
    {
        public static Dictionary<string, string> DeleteProductSuccess = new Dictionary<string, string>
    {
        { "name", "Choco Bytes" },
        { "search", "Wayanadd" }
        };
        public static Dictionary<string, string> DeleteProductFailure = new Dictionary<string, string>
    {
        { "productId", "223BHA0000272" }
        };
    }
}
