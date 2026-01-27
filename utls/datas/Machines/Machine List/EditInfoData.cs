using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    [TestClass]
    public class EditInfoData
    {
        public static Dictionary<string, string> editInfoSuccess = new Dictionary<string, string>
    {
        { "serialNumber", "123" },
        { "drange", "8/10/2024" },
        { "companyName", "new company" },
        { "contactNo", "9087898790" },
        { "gstNo", "AGSATP893449k3f" },
         { "igstNo", "GSATP893449k3f" },
        { "email", "test@riota.in" },
        { "address", "Annanagar" },
        { "companyAddress", "The hive, VR mall" },
        };
    }
}
