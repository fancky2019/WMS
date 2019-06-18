using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Dal;
using WMS.Model;
using WMS.Model.EntityModels;

namespace WMS.BLL.SystemManager
{
    public class SkuManagerBll
    {
        SkuDal _dal = null;
        public SkuManagerBll()
        {
            _dal = new SkuDal();
        }
        public List<Sku> LoadData()
        {
            return _dal.LoadData();
        }
        public int Add(Sku sku)
        {
            return _dal.Add(sku);
        }

        public bool Exist(Sku sku)
        {
            return _dal.Exist(sku);
        }

        public int Update(Sku sku)
        {
            return _dal.Update(sku);
        }

        public int Delete(List<int> idList)
        {
            return _dal.Delete(idList);
        }
    }
}
