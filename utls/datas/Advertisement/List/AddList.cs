using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS_Phase1PortalAT.utls.datas.Advertisement.List
{
    public class AddListData
    {
        public static Dictionary<string, string> AddListSuccess = new Dictionary<string, string>
    {
        { "Title", "Automation"+ DateTime.Now.ToString("' 'mm.ss.ff") },
        { "Adv File", "D:\\projects\\VMS_automations\\VMS-WebUI-AT\\utls\\Files\\videoplayback.mp4" },
         { "Content", "mp4" },
          { "Client", "Capgemini HYD" },
    };



    }
}
