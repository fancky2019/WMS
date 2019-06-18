using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Common;
using WMS.Model.EntityModels;

namespace WMS.Dal
{
   public class StockDal
    {
        public List<Stock> LoadData()
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    return dbContext.Stock.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error<StockDal>(ex.ToString());
                return null;
            }
        }
        public int Add(Stock stock)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    dbContext.Stock.Add(stock);
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Error<StockDal>(ex.ToString());
                return 0;
            }
        }
    }
}
