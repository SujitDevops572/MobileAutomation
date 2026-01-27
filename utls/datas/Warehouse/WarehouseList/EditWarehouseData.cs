using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Warehouse.WarehouseList
{
    public class EditWarehouseData
    {
        public static Dictionary<string, string> EditWarehouseSuccess = new Dictionary<string, string>
    {
         { "warehouseId", "KOL0000024" },
        { "name", "Nestsle21"+(char)new Random().Next('A', 'Z' + 1) }
        };

        public static Dictionary<string, string> EditWarehouseFailure = new Dictionary<string, string>
    {{ "warehouseId", "KOL0000024" },
        { "name", "Vignesh" }
        };
    }
}
