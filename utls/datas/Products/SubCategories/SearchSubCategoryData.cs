using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.SubCategories
{
    public class SearchSubCategoryData
    {
        public static Dictionary<string, string> searchSubCategorySuccess = new Dictionary<string, string>
    {
            { "Name","sdf" },
            { "Sub Category Id","73" },
            { "Category Id","68" },
            { "Category Name","chi" },
            { "Brand Id","70" },
            { "Brand Name","coco" },
            { "Branch Id","07" },
            { "Branch Name","a" },
    };

        public static Dictionary<string, string> searchSubCategoryFailure = new Dictionary<string, string>
    {
            { "Name","zzzzzzzzzzzzzzz" },
            { "Sub Category Id","zzzzzzzzzzzzzzz" },
            { "Category Id","zzzzzzzzzzzzzzz" },
            { "Category Name","zzzzzzzzzzzzzzz" },
            { "Brand Id","zzzzzzzzzzzzzzz" },
            { "Brand Name","zzzzzzzzzzzzzzz" },
            { "Branch Id","zzzzzzzzzzzzzzz" },
            { "Branch Name","zzzzzzzzzzzzzzz" },
    };
    }
}
