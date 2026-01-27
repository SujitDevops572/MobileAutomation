using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Brand
{
    public class DeleteBrandData
    {
        public static Dictionary<string, string> DeleteBrandSuccess = new Dictionary<string, string>
    {
        { "brand", "new chikr" }
        };
        public static Dictionary<string, string> DeleteBrandFailure = new Dictionary<string, string>
    {
        { "brandId", "022" }
        };
    }
}
