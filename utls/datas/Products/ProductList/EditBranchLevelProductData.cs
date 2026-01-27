using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.ProductList
{
    [TestClass]
    public class EditBranchLevelProductData
    {
        public static Dictionary<string, string> EditProductSuccess = new Dictionary<string, string>
    {
        { "name", "Chocso Bytes"+(char)new Random().Next('A', 'Z' + 1) },
        { "branch", "Wayanadd" },
        { "hsnCode", "123" },
        { "customProductId", "123" },
        { "cost", "test address" },
        { "mrp", "10" },
        { "cgst", "1" },
        { "sgst", "1" },
        { "utgst", "1.2" },
        { "cess", "1.5" },
        { "taxablePriceCovS", "2" },
        { "sCost", "1" },
        { "taxablePriceCovUT", "3" },
        { "utCost", "5" },
        };

        public static Dictionary<string, string> EditProductBtnDisable = new Dictionary<string, string>
    {
        { "name", "Choco Bytes" },
        { "branch", "Wayanadd" },
        { "hsnCode", "123" },
        { "customProductId", "123" },
        { "cost", "test address" },
        { "mrp", "10" },
        { "cgst", "1" },
        { "sgst", "1" },
        { "utgst", "1.2" },
        { "cess", "1.5" },
        { "taxablePriceCovS", "11" },
        { "sCost", "1" },
        { "taxablePriceCovUT", "12" },
        { "utCost", "5" },
        };
    }
}
