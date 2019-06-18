using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Common;
using WMS.Model.ViewModels;

namespace WMS.Dal
{
    public class InOutStockDetailDal
    {
        public List<ProductVM> GetInOutStockDetail(int inOutStockID)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    var result = (from s in dbContext.InOutStockDetail
                                  join p in dbContext.Product on s.ProductID equals p.ID
                                  join k in dbContext.Sku on p.SkuID equals k.ID
                                  where s.InOutStockID == inOutStockID
                                  select new ProductVM()
                                  {
                                      ID=p.ID,
                                      GUID=p.GUID,
                                      ProductName = p.ProductName,
                                      ProductStyle = p.ProductStyle,
                                      Count = s.Count,
                                      Unit = k.Unit
                                  }).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error<InOutStockDal>(ex.ToString());
                return null;
            }
        }
    }

}
