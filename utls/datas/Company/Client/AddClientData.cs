using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.Client
{
    public class AddClientData
    {
        public static Dictionary<string, string> addClientSuccess = new Dictionary<string, string>
    {
        { "name", "Cadbursy" },
        { "branch", "A2B Test" },
        { "contactno", "9087678917" },
        { "email", "testCslient@riota.in" },
        { "address", "test address" },
        { "gstNo", "AGSATP893449k3f" },
        { "companyName", "Vendolite" },
        { "companyContactNo", "9087898789" },
        { "companyEmail", "testCsmpany@riota.in" },
        { "companyAddress", "Annanagar" },
        { "conversionRateFor1Point", "12" },
        { "lpUsageRestrictionPercentage", "10" },
        { "maturityTimeInDays", "9" },
        { "registrationPoints", "5" },

        };

        public static Dictionary<string, string> addClientFailure = new Dictionary<string, string>
    {
        { "name", "Test Client" },
        { "branch", "A2B Test" },
        { "contactno", "9087678917" },
        { "email", "testClient@riota.in" },
        { "address", "test address" },
        { "gstNo", "AGSATP893449k3f" },
        { "companyName", "Vendolite" },
        { "companyContactNo", "9087898789" },
        { "companyEmail", "testCompany@riota.in" },
        { "companyAddress", "Annanagar" },
        { "conversionRateFor1Point", "12" },
        { "lpUsageRestrictionPercentage", "10" },
        { "maturityTimeInDays", "9" },
        { "registrationPoints", "5" },

        };

        public static Dictionary<string, string> addClientInvalidMobileNumber = new Dictionary<string, string>
    {
        { "name", "Test Client" },
        { "branch", "A2B Test" },
        { "contactno", "9087678" },
        { "email", "testClient@riota.in" },
        { "address", "test address" },
        };

        public static Dictionary<string, string> addClientInvalidEmail = new Dictionary<string, string>
    {
        { "name", "Test Client" },
        { "branch", "A2B Test" },
        { "contactno", "9087678089" },
        { "email", "testClient" },
        { "address", "test address" },
        };

        public static Dictionary<string, string> addClientWithoutRequiredFields = new Dictionary<string, string>
    {
        { "name", "Test Client" },
        { "branch", "A2B Test" },
        { "contactno", "" },
        { "email", "" },
        { "address", "test address" },
        };

        public static Dictionary<string, string> addClientInvalidGstno = new Dictionary<string, string>
    {
        { "name", "Test Client" },
        { "branch", "A2B Test" },
        { "contactno", "9054676567" },
        { "email", "testClient@riota.in" },
        { "address", "test address" },
         { "gstNo", "123" }
        };
    }
}




