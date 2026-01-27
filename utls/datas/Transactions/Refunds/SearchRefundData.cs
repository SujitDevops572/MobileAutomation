using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Transactions.Refunds
{
    public class SearchRefundData
    {
        public static Dictionary<string, string> searchRefundSuccess = new Dictionary<string, string>
    {
            { "Refund Id","1" },
            { "Machine Id","5" },
            { "Transaction Id","63" },
    };

        public static Dictionary<string, string> searchRefundFailure = new Dictionary<string, string>
    {
            { "Refund Id","zzzzzzzzzzzzzzz" },
            { "Machine Id","zzzzzzzzzzzzzzzzzzz" },
            { "Transaction Id","zzzzzzzzz" },
    };
    }
}
