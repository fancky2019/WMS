using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Common;
using WMS.Dal;
using WMS.Model.EntityModels;
using WMS.Model.QueryModels;
using WMS.Model.ViewModels;

namespace WMS.BLL.InOutStockManager
{
    public class InOutStockManagerBll
    {
        InOutStockDal _dal = null;
        InOutStockDetailDal _inOutStockDetailDal = null;
        public InOutStockManagerBll()
        {
            _dal = new InOutStockDal();
            _inOutStockDetailDal = new InOutStockDetailDal();
        }
        public int AddInOutStockAndDetail(InOutStock inOutStock, List<InOutStockDetail> inOutStockDetailList)
        {
            return _dal.AddInOutStockAndDetail(inOutStock, inOutStockDetailList);
        }
        public int UpdateInOutStockAndDetail(InOutStock inOutStock, List<InOutStockDetail> inOutStockDetailList)
        {
            return _dal.UpdateInOutStockAndDetail(inOutStock, inOutStockDetailList);
        }
        public Tuple<int, List<InOutStockVM>> LoadData(InOutStockQM qm)
        {
            return _dal.LoadData(qm);
        }

        public List<ProductVM> GetInOutStockDetail(int inOutStockID)
        {
            return _inOutStockDetailDal.GetInOutStockDetail(inOutStockID);
        }
        public int DeleteOrder(int inOutStockID)
        {
            return _dal.DeleteOrder(inOutStockID);
        }
    }
}
