using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Common;
using WMS.Model;
using WMS.Model.EntityModels;

namespace WMS.Dal
{
    public class SkuDal
    {
        public List<Sku> LoadData()
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    return dbContext.Sku.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error<SkuDal>(ex.ToString());
                return null;
            }
        }
        public int Add(Sku sku)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    dbContext.Sku.Add(sku);
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Error<SkuDal>(ex.ToString());
                return 0;
            }

        }

        public bool Exist(Sku sku)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    return dbContext.Sku.Any(p=>p.Unit==sku.Unit);
                }
            }
            catch (Exception ex)
            {
                Log.Error<SkuDal>(ex.ToString());
                return false;
            }

        }

        public int Update(Sku sku)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    Sku sk = dbContext.Sku.Where(p => p.ID == sku.ID).FirstOrDefault();
                    sk.Unit = sku.Unit;
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Error<SkuDal>(ex.ToString());
                return 0;
            }

        }

        public int Delete(List<int> idList)
        {
            try
            {
                using (WMSDbContext dbContext = new Dal.WMSDbContext())
                {
                    dbContext.Sku.RemoveRange(dbContext.Sku.Where(p => idList.Contains(p.ID)));
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log.Error<SkuDal>(ex.ToString());
                return 0;
            }

        }
    }
}
