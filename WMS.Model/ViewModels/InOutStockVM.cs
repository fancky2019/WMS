using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.ViewModels
{
    public class InOutStockVM
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid GUID { get; set; }
        /// <summary>
        /// 0、删除 1、正常
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 1、入库 2、出库
        /// </summary>
        public short Type { get; set; }
        public string TypeString
        {
            get
            {
                return Type == 1 ? "入库" : "出库";
            }
        }
        /// <summary>
        /// 0:未完成，1、已完成
        /// </summary>
        public bool IsComplete { get; set; }
        public string IsCompleteString
        {
            get
            {
                return !IsComplete ? "未完成" : "已完成";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStyle { get; set; }
    }
}
