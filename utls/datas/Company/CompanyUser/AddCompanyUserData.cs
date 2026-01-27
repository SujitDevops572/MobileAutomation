using AventStack.ExtentReports.Gherkin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Company.CompanyUser
{
    public class AddCompanyUserData
    {
        public static Dictionary<string, string> addCompanyUserSuccess = new Dictionary<string, string>
    {
        { "name", "new test"+(char)new Random().Next('A', 'Z' + 1) +new Random().Next(1000, 9999)},
        { "username", "newtest"+(char)new Random().Next('A', 'Z' + 1) +"@riota.in" +new Random().Next(1000, 9999)},
        { "email", "test"+(char)new Random().Next('A', 'Z' + 1)+"@riota.in" +new Random().Next(1000, 9999)},
        { "mobile", "908767"+new Random().Next(1000, 9999)},
        { "client", "Automation" },
        { "password", "test@123" },
        { "image", "C:\\Users\\Babu Office\\Downloads\\Group 6.png" },
    };

        public static Dictionary<string, string> addCompanyUserFailure = new Dictionary<string, string>
    {
        { "name", "Test" },
        { "username", "karthik@riota.in" },
        { "email", "karthik@riota.in" },
        { "mobile", "9087678917" },
        { "client", "new user role" },
        { "password", "karthik@123" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
        public static Dictionary<string, string> addCompanyUserBtnDisable = new Dictionary<string, string>
    {
        { "name", "xxxxxxxxxx" },
        { "username", "xxxxxxxxxx" },
        { "email", "xxxxxxxxxxxxx" },
     
    };
        public static Dictionary<string, string> addCompanyUserBtnDisableWithoutRequired = new Dictionary<string, string>
    {
        { "name", "" },
        { "username", "Test" },
        { "email", "Test" },
        { "mobile", "90876789176788999" },
        { "client", "new user role" },
        { "password", "karthik@123" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
    }
}
