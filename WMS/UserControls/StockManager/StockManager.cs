using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Model.EntityModels;
using WMS.Dal;
using WMS.BLL.SystemManager;
using DevExpress.XtraEditors;
using WMS.BLL.StockManager;

namespace WMS.UserControls.StockManager
{
    public partial class StockManager : UserControl
    {
        StockManagerBll _bll = null;
        public StockManager()
        {
            InitializeComponent();
            _bll = new StockManagerBll();
            this.gridStock.DataSource = _bll.LoadData();
        }

        private void sbtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teStockName.Text.Trim()))
            {
                return;
            }
            if (string.IsNullOrEmpty(meStockLocation.Text.Trim()))
            {
                return;
            }
            Stock stock = new Stock()
            {
                GUID=Guid.NewGuid(),
                StockName = teStockName.Text.Trim(),
                Location = meStockLocation.Text.Trim(),
            };
            int reuslt = _bll.Add(stock);

            if (reuslt > 0)
            {
                XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK);
                this.gridStock.DataSource = _bll.LoadData();
            }
            else
            {
                XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
