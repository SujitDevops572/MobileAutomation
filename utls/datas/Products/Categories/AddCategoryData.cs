using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Categories
{
    public class AddCategoryData
    {
        public static Dictionary<string, string> addCategorySuccess = new Dictionary<string, string>
    {
        { "name", "Ksasss xss" },
        { "branch", "new test branch" },
        { "brand", "sdfsdf" },
    };

        public static Dictionary<string, string> addCategoryFailure = new Dictionary<string, string>
    {
        { "name", "Kasrtdgh chikr" },
        { "branch", "new test branch" },
        { "brand", "sdfsdf" },
    };
        public static Dictionary<string, string> addWCategoryBtnDisable = new Dictionary<string, string>
    {
        { "name", "Kasrtdgh chikr" },
        { "email", "neww@gmail.com" },
        { "contactPersonEmail", "neww@gmail.com" },
        { "contactPersonMobile", "9087678917" },
        { "branch", "new test branch" },
        { "mobile", "9087678917" },
        { "contactPersonName", "sample" },
    };
        public static Dictionary<string, string> adCategoryBtnDisableWithoutRequired = new Dictionary<string, string>
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
