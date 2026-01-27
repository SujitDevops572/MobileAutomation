using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.ProductList
{
    public class AddBranchLevelProductData
    {
        public static Dictionary<string, string> addProductSuccess = new Dictionary<string, string>
    {
        { "name", "Choco Bytes" },
        { "branch", "Wayanadd" },
        { "hsnCode", "123" },
        { "customProductId", "123" },
        { "cost", "test address" },
        { "mrp", "10" },
        { "cgst", "1" },
        { "sgst", "1" },
        { "utgst", "1" },
        { "cess", "1" },
        { "taxablePriceCovS", "0.5" },
        { "sCost", "0.5" },
        { "taxablePriceCovUT", "0.5" },
        { "utCost", "0.5" },
      //  { "utCost", "5" },
         { "fileUpload", "D:\\projects\\VMS_automations\\VMS-WebUI-AT\\utls\\Files\\OIP.jpg" },
        };
    }
}
