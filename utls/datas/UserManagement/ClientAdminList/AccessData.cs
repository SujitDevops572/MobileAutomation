using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.UserManagement.ClientAdminList
{
    public class AccessData
    {
        public static string[] MenuArrowClientAdminListSuccess =
        {
             "Products",                
                
        };
        public static string[] MenuCheckboxClientAdminListSuccess =
       {
             "Brands","Sub Categories",

        };


        public static string[] FeatureArrowClientAdminListSuccess =
        {
             "Brands Page","Sub Categories Page",

        };
        public static string[][] FeatureCheckboxClientAdminListSuccess =
       {
             //Brand checkboxes
             ["Add Brand","Edit Brand"],

             //subCategory Checkboxes
             ["Add Sub Category","Edit Sub Category"],

        };
    }
}
