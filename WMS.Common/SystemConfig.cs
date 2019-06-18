using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Common
{
    public class SystemConfig
    {
        public static string WMSConnectionString;
        static SystemConfig()
        {
            //WMSConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            WMSConnectionString = ConfigurationManager.ConnectionStrings["WMSConnectionString"].ConnectionString;
        }
    }
}
