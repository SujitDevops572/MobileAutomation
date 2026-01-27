using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Products.Brand
{
    public class ImportBrandData
    {
        public static Dictionary<string, string> ImportBrandSuccess = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\Babu Office\\Downloads\\Brands_at_2024-10-28 03_20_19.xlsx" },
    };

        public static Dictionary<string, string> ImportBrandMissingColumn = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\Babu Office\\Downloads\\Brands_at_2024-10-28 03_20_19 - Copy.xlsx" },
    };

        public static Dictionary<string, string> ImportBrandFailure = new Dictionary<string, string>
    {
        { "inputfile","C:\\Users\\Babu Office\\Downloads\\Brands_at_2024-10-28 03_20_19 - Copy (2).xlsx" },
    };
    }
}
