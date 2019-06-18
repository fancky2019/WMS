using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Common;
using WMS.Model.EntityModels;
using WMS.Model.QueryModels;
using WMS.Model.ViewModels;

namespace WMS.Dal
{
   public class InOutStockDal
    {
        public Tuple<int, List<InOutStockVM>> LoadData(InOutStockQM qm)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    var inOutStockList = dbContext.InOutStock.AsQueryable();
                    if (qm.CreateTime != null)
                    {
                        inOutStockList = inOutStockList.Where(p => qm.CreateTime >= qm.CreateTime);
                    }
                   var productList= dbContext.Product.AsQueryable();
                    if (!string.IsNullOrEmpty(qm.ProductName))
                    {
                        productList = productList.Where(p => p.ProductName.Contains(qm.ProductName));
                    }
                    if (!string.IsNullOrEmpty(qm.ProductStyle))
                    {
                        productList = productList.Where(p => p.ProductStyle.Contains(qm.ProductStyle));
                    }
                    var result = (from s in inOutStockList
                                 join d in dbContext.InOutStockDetail on s.ID equals d.InOutStockID
                                 join p in productList on d.ProductID equals p.ID
                                 where s.Status==1
                                 select new InOutStockVM()
                                 {
                                     CreateTime=s.CreateTime,
                                     ID=s.ID,
                                     GUID=s.GUID,
                                     IsComplete=s.IsComplete,
                                     Type=s.Type
                                 }).Distinct();
                    var count = result.Count();
                    var list = result.OrderByDescending(p => p.CreateTime).Skip(qm.Skip).Take(qm.Take).ToList();
                    return Tuple.Create<int, List<InOutStockVM>>(count, list);
                }
            }
            catch (Exception ex)
            {
                Log.Error<InOutStockDal>(ex.ToString());
                return null;
            }
        }
        public int AddInOutStockAndDetail(InOutStock inOutStock,List<InOutStockDetail> inOutStockDetailList)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    dbContext.InOutStock.Add(inOutStock);
                    dbContext.SaveChanges();
                    inOutStockDetailList.ForEach(p =>
                    {
                        p.InOutStockID = inOutStock.ID;
                    });
                    dbContext.InOutStockDetail.AddRange(inOutStockDetailList);
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Error<InOutStockDal>(ex.ToString());
                return 0;
            }
        }

        public int UpdateInOutStockAndDetail(InOutStock inOutStock, List<InOutStockDetail> inOutStockDetailList)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    //dbContext.InOutStock.Add(inOutStock);
                    //dbContext.SaveChanges();
                    dbContext.InOutStockDetail.RemoveRange(dbContext.InOutStockDetail.Where(p=>p.InOutStockID==inOutStock.ID));
                    dbContext.InOutStockDetail.AddRange(inOutStockDetailList);
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Error<InOutStockDal>(ex.ToString());
                return 0;
            }
        }

        public int DeleteOrder(int inOutStockID)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    //dbContext.InOutStock.Add(inOutStock);
                    //dbContext.SaveChanges();
                    dbContext.InOutStock.RemoveRange(dbContext.InOutStock.Where(p => p.ID == inOutStockID));
                    dbContext.InOutStockDetail.RemoveRange(dbContext.InOutStockDetail.Where(p => p.InOutStockID == inOutStockID));
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Error<InOutStockDal>(ex.ToString());
                return 0;
            }
        }
    }
}
