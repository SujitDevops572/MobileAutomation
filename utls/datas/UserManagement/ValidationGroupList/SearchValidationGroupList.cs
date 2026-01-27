using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.UserManagement.ValidationGroupList
{
    public class SearchValidationGroupListData
    {
        public static Dictionary<string, string> SearchValidationGroupListSuccess = new Dictionary<string, string>
    {
        { "Name", "Vicky" },
        { "Id", "18" },
        { "Client", "Riota" },
        };
        public static Dictionary<string, string> SearchValidationGroupListFailure = new Dictionary<string, string>
    {
        { "Name", "CCCCCCCC" },
        { "Id", "OKKKKKKKKK" },
        { "Client", "LLSGHVSvCd" },
        };
        public static Dictionary<string, string> EditValidationGroupList = new Dictionary<string, string>
    {
        { "Name1", "bethoven"+(char)new Random().Next('A', 'Z' + 1) },
        { "qty", "10" },
        { "hour", "10" },
        { "mins", "45" },
         { "Name", "bethoven" },
        };
    }
}
