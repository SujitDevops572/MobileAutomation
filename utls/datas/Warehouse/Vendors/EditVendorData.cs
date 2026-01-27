using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.Vendors
{
    public class EditVendorData
    {
        public static Dictionary<string, string> EditVendorSuccess = new Dictionary<string, string>
    {
        {"vendorId","KAS0000034"},
        { "name", "Kassrtdgh"+(char)new Random().Next('A', 'Z' + 1) },
        { "email", "nseww@gmail.com" },
        { "contactPersonEmail", "nesww@gmail.com" },
        { "contactPersonMobile", "9087678917" },
        { "branch", "new test branch" },
        { "mobile", "9087678917" },
        { "contactPersonName", "sample" },
        };

        public static Dictionary<string, string> EditVendorFailure = new Dictionary<string, string>
    {
        {"vendorId","KAS0000034"},
        { "name", "Kasrstdgh chikr" },
        { "email", "neww@gmail.com" },
        { "contactPersonEmail", "neww@gmail.com" },
        { "contactPersonMobile", "9087678917" },
        { "branch", "new test branch" },
        { "mobile", "9087678917" },
        { "contactPersonName", "sample" },
        };
    }
}
