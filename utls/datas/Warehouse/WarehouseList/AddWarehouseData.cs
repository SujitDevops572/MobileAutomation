using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.WarehouseList
{
    public class AddWarehouseData
    {
        public static Dictionary<string, string> addWarehouseSuccess = new Dictionary<string, string>
    {
        { "name", "fffschfgikr" },
        { "lat", "81" },
        { "lon", "12" },
        { "city", "chennai" },
        { "state", "Tamilnadu" },
        { "branch", "new test branch" },
        { "phoneNo", "9087678917" },
        { "image", "C:\\Users\\Babu Office\\Downloads\\Group 6.png" },
    };

        public static Dictionary<string, string> addWarehouseFailure = new Dictionary<string, string>
    {
        { "name", "Kasrthikr" },
        { "lat", "81" },
        { "lon", "12" },
        { "city", "chennai" },
        { "state", "Tamilnadu" },
        { "branch", "new test branch" },
        { "phoneNo", "9087678917" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
        public static Dictionary<string, string> addWarehouseBtnDisable = new Dictionary<string, string>
    {
        { "name", "Test" },
        { "lat", "81" },
        { "lon", "12" },
        { "city", "chennai" },
        { "state", "Tamilnadu" },
        { "branch", "new test branch" },
        { "phoneNo", "23123424" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
        public static Dictionary<string, string> addWarehouseBtnDisableWithoutRequired = new Dictionary<string, string>
    {
        { "name", "Kasrthikr" },
         { "lat", "81" },
        { "lon", "12" },
        { "city", "chennai" },
        { "state", "Tamilnadu" },
        { "branch", "new test branch" },
        { "phoneNo", "9087678917" },
        { "image", "C:\\Users\\ashin\\Downloads\\image-1715967175491.png" },
    };
    }
}
