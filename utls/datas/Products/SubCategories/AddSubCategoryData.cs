using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.SubCategories
{
    public class AddSubCategoryData
    {
        public static Dictionary<string, string> addSubCategorySuccess = new Dictionary<string, string>
    {
        { "name", "Kasss"+(char)new Random().Next('A', 'Z' + 1) },
        { "branch", "trivandrum" },
        { "brand", "britiana" },
        { "category", "biscuits" },    };

        public static Dictionary<string, string> addSubCategoryFailure = new Dictionary<string, string>
    {
        { "name", "sdf nnn" },
        { "branch", "trivandrum" },
        { "brand", "britiana" },
        { "category", "biscuits" },
    };
        public static Dictionary<string, string> addSubCategoryBtnDisableWithoutRequired = new Dictionary<string, string>
    {
        { "name", "Kasrtdgh chikr" },
        { "email", "neww@gmail.com" },
        { "contactPersonEmail", "neww@gmail.com" },
        { "contactPersonMobile", "9087678917" },
        { "branch", "new test branch" },
        { "mobile", "9087678917" },
        { "contactPersonName", "sample" },
    };
    }
}
