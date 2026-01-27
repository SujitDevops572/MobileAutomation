using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.ProductList
{
    public class SearchClientLevelProductData
    {
        public static Dictionary<string, string> searchProductSuccess = new Dictionary<string, string>
    {
            { "Name","c" },
            { "Product Id","GAL0000652" },
            { "Sub Category Id","DEM0000001" },
            { "Sub Category Name","Demo" },
            { "Category Id","DEM0000001" },
            { "Category Name","Demo Product" },
            { "Brand Id","RIO0000001" },
            { "Brand Name","Riota" },
    };

        public static Dictionary<string, string> searchProductFailure = new Dictionary<string, string>
    {
            { "Name","zzzzzzzzzzzzzzz" },
            { "Product Id","zzzzzz" },
            { "Sub Category Id","zzzzzzzzzzzzzzzzzzz" },
            { "Sub Category Name","xxxxxxx" },
             { "Category Id","xxxxxxxxxx" },
            { "Category Name","xxxxxxxxxxxx" },
            { "Brand Id","xxxxxxxxx" },
            { "Brand Name","xxxxxxxxxx" },
    };
    }
}
