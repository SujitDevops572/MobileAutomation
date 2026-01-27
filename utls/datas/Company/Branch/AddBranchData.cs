using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    public class AddBranchData
    {
        public static Dictionary<string, string> addBranchSuccess = new Dictionary<string, string>
    {
        { "name", "Pudukkottai Branch1"},
        { "location", "dcz" },
        { "code", "344" },
        { "email", "calidcftdut1@gmail.com"},
        { "address", "czx"},
        { "contactDetails", "9087898789" },
        { "gstNo", "AGSATP8934f9k3f"},
        { "companyName", "Venddsolite"},
        { "companyContactNo", "9087898789" },
        { "companyEmail", "testCodmdpany@riota.in" },
        { "companyAddress", "Annanagar" },
        };

        public static Dictionary<string, string> addBranchFailure = new Dictionary<string, string>
    {
        { "name", "Test Branch" },
        { "location", "Thirumagalam" },
        { "code", "123" },
        { "email", "calicut@gmail.com"},
        { "address", "test address" },
        { "contactDetails", "9087898789" },
        { "gstNo", "AGSATP893449k3f" },
        { "companyName", "Vendolite" },
        { "companyContactNo", "9087898789" },
        { "companyEmail", "testCompany@riota.in" },
        { "companyAddress", "Annanagar" },
        };

        public static Dictionary<string, string> addBranchInvalidMobileNumber = new Dictionary<string, string>
    {
        { "name", "Calicut" },
        { "location", "Thirumagalam" },
        { "code", "123" },
        { "email", "calicut@gmail.com"},
        { "address", "test address" },
        { "contactDetails", "908789878999999" },
        { "gstNo", "AGSATP893449k3f" },
        };

        public static Dictionary<string, string> addBranchInvalidEmail = new Dictionary<string, string>
    {
        { "name", "Calicut" },
        { "location", "Thirumagalam" },
        { "code", "123" },
        { "email", "dfdfgdfgdfgfdg"},
        { "address", "test address" },
        { "contactDetails", "9087656765" },
        { "gstNo", "AGSATP893449k3f" },
        };
    }
}
