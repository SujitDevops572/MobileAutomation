using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Categories
{
    public class SearchCategoryData
    {
        public static Dictionary<string, string> searchCategorySuccess = new Dictionary<string, string>
    {
            { "Name","a" },
            { "Category Id","6" },
            { "Brand Id","7" },
            { "Brand Name","coco" },
            { "Branch Id","0" },
            { "Branch Name","a" },
    };

        public static Dictionary<string, string> searchCategoryFailure = new Dictionary<string, string>
    {
            { "Name","zzzzzzzzzzzzzzz" },
            { "Category Id","zzzzzz" },
            { "Brand Id","zzzzzzzzzzzzzzzzzzz" },
            { "Brand Name","zzzzzzz" },
            { "Branch Id","zzzzzzzzz" },
            { "Branch Name","zzzzzzzzz" },
    };
    }
}
