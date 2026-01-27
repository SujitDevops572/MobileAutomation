using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas
{
    [TestClass]
    public class AddLocationData
    {
        public static Dictionary<string, string> addLocationSuccess = new Dictionary<string, string>
    {
        { "City", "Annanagar" },
        { "State", "Tamilnadu" },
        { "pluscode", "36M4+QH Chennai, Tamil Nadu" },
        };
    
     public static Dictionary<string, string> addLocationSuccess1 = new Dictionary<string, string>
        {
        { "City", "Ambathur" },
        { "State", "Tamilnadu" },
        { "pluscode", "4594+7H9" },
        };
    }
}
