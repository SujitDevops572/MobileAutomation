using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Brand
{
    public class AddBrandData
    {
        public static Dictionary<string, string> addBrandSuccess = new Dictionary<string, string>
    {
        { "name", "new chikr" },
        { "branch", "new test branch" },
    };

        public static Dictionary<string, string> addBrandFailure = new Dictionary<string, string>
    {
        { "name", "Kasrtdgh chikr" },
        { "branch", "new test branch" },
    };
        public static Dictionary<string, string> addBrandBtnDisable = new Dictionary<string, string>
    {
        { "name", "Kasrtdgh chikr" },
        { "branch", "new test branch" },
    };
        public static Dictionary<string, string> addBrandBtnDisableWithoutRequired = new Dictionary<string, string>
    {
        { "name", "Kasrtdgh chikr" },
        { "branch", "new test branch" },
    };
    }
}
