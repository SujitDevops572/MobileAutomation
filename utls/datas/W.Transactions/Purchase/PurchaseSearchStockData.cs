using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.W.Transactions.Purchase
{
    public class PurchaseSearchStockData
    {
        public static Dictionary<string, string> PurchaseSearchStockSuccess = new Dictionary<string, string>
    {
            { "Vendor Id","VEN0000002" },
            { "Batch Id","sdf" },
            { "Warehouse Id","JPH0000003" },
            { "Vendor Name","Vendol" },
                  { "Warehouse Name","test2" },
            { "Product Id","473BUT0000022" },
            { "Product Name","Butter Cookies - 10" },



    };

        public static Dictionary<string, string> PurchaseSearchStockFailure = new Dictionary<string, string>
    {
            {  "Vendor Id","xxxxxxx" },
            { "Batch Id","xxxxx" },
            { "Warehouse Id","xxxxxxxx" },
            { "Warehouse Name","xxxxrty" },
            { "Product Id","xcvcvcv" },
            { "Product Name","xxvxcvcv" },
            { "Vendor Name","yyycccnnn" }
    };


      public static Dictionary<string, string> DateRange = new Dictionary<string, string>
      {

          { "Request Id", "RREQ000315" },
          { "Machine Id", "2VE0000204" },
          { "Created By", "vendomatic1(CA)" },
          { "Status", "Completed" },
          { "Start Date", "1" },
          { "End Date", "20" },
          { "Start Month", "feb" },
          { "End Month", "may" },
          { "year", "2025" }
      };
    }
}