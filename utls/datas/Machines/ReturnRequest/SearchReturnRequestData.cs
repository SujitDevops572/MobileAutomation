using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;

namespace VMS_Phase1PortalAT.utls.datas.W.Transactions.ReturnRequest
{
    [TestClass]
    public class SearchReturnRequestData
    {
        public static Dictionary<string, string> searchReturnRequestSuccess = new Dictionary<string, string>
    {
            { "Request Id","RREQ000247" },
            { "Machine Id","2TE0000065" },
            { "Warehouse Id","CUD0000016" },
            { "Created By","Basha" },
            { "Status","Completed" },
    };

        public static Dictionary<string, string> searchReturnRequestFailure = new Dictionary<string, string>
    {
            { "Request Id","xxxxxxx" },
            { "Machine Id","xxxxx" },
            { "Warehouse Id","xxxxxxxx" },
            { "Created By","xxxxrty" },
            { "Status","xxxx" },
    };
    }
}
