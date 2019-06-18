using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Dal;
using WMS.Model.EntityModels;

namespace WMS.BLL.StockManager
{
    public class StockManagerBll
    {
        StockDal _dal = null;
        public StockManagerBll()
        {
            _dal = new StockDal();
        }
        public List<Stock> LoadData()
        {
            return _dal.LoadData();
        }
        public int Add(Stock stock)
        {
            return _dal.Add(stock);
        }
    }
}
