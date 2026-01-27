using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.W.Transactions.Purchase
{
    public class PurchaseAdd    {
        public static Dictionary<string, string> AddPurchase = new Dictionary<string, string>
    {
           { "warehouse", "test2" },
        { "vendor", "Test" },
        { "product", "Lays Test Product - Id: DEVLAY0000212" },
        { "qty", "30"},
        { "batch id", "TEST001" },
         { "I_warehouse", "xxxyyyzzz" },
        { "I_vendor", "xxx111" },
        { "I_product", "Silk - Id: DAY212" },
        { "I_qty", "30"},
        { "I_batch id", "T001" }

    };

        public static Dictionary<string, string> ExpiryDate = new Dictionary<string, string>
    {
            {  "date","12" },
            { "month","dec" },
            { "year","2025" }
          
    };
        public static Dictionary<string, string> BillDate = new Dictionary<string, string>
    {
            {  "date","14" },
            { "month","aug" },
            { "year","2026" }

    };
        public static Dictionary<string, string> UpdateExpiryDate = new Dictionary<string, string>
    {
            {  "date", new Random().Next(1, 10).ToString() },
            { "month","feb" },
            { "year","2027" }

    };
    }
}
