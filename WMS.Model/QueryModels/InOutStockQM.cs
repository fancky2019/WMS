using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.QueryModels
{
    public class InOutStockQM : PageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStyle { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
