﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.ViewModels
{
    public class ProductVM
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
        public int? StockID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? BarCodeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SkuID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStyle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        
        /// <summary>
        /// 0、删除 1、正常
        /// </summary>
        public short Status { get; set; }

        public int Count { get; set; }
        public string Code { get; set; }
        public string StockName { get; set; }
        public string Unit { get; set; }
    }
}
