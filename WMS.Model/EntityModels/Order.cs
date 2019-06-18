﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Model.EntityModels
{
    public class Order
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
        public string OrderNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 1、销售单、2、退货单
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 0、删除 1、正常
        /// </summary>
        public short Status { get; set; }
    }
}