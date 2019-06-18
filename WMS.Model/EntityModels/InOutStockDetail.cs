using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Model.EntityModels
{
    public class InOutStockDetail
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
        /// 
        /// </summary>
        public int InOutStockID { get; set; }
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
        public DateTime CrateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifyTime { get; set; }
    }
}