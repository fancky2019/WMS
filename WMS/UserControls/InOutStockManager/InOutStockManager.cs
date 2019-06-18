using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Model.QueryModels;
using WMS.Model.ViewModels;
using WMS.BLL.InOutStockManager;
using WMS.UserControls.ProcuctManager;
using DevExpress.XtraEditors;

namespace WMS.UserControls.InOutStockManager
{
    public partial class InOutStockManager : UserControl
    {
        private InOutStockManagerBll _bll = null;
        public InOutStockManager()
        {
            InitializeComponent();
            _bll = new InOutStockManagerBll();
            InitData();
        }

        private void sbtAdd_Click(object sender, EventArgs e)
        {
            InOutStockEdit inOutStockEdit = new InOutStockEdit();
            FrmPop.Show(this, "新增出入库", inOutStockEdit, (() =>
            {
                this.pageNavigator.Skip = 0;
                InitData();
            }));

        }

        private void InitData()
        {
            InOutStockQM qm = new InOutStockQM();
            qm.ProductName = this.teProductName.Text.Trim();
            qm.ProductStyle = this.teProductStyle.Text.Trim();
            qm.CreateTime = (DateTime?)this.deCreateTime.EditValue;
            qm.Skip = this.pageNavigator.Skip;
            qm.Take = this.pageNavigator.Take;
            Tuple<int, List<InOutStockVM>> result = _bll.LoadData(qm);
            this.gcInOutStockOrder.DataSource = null;
            this.pageNavigator.Total = result.Item1;
            this.gcInOutStockOrder.DataSource = result.Item2; ;
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            this.pageNavigator.Skip = 0;
            InitData();
        }

        private void gvInOutStockOrder_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = this.gvInOutStockOrder.CalcHitInfo(e.Location);
            if (hi.InRow && e.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(MousePosition);
            }
        }



        private void sbtnInStockComplete_Click(object sender, EventArgs e)
        {

        }

        private void bbiOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InOutStockVM order = this.gvInOutStockOrder.GetRow(this.gvInOutStockOrder.FocusedRowHandle) as InOutStockVM;//获取选中行的实体
            switch (e.Item.Tag.ToString())
            {
                case "update":
                    Edit(order);
                    break;
                case "delete":
                    Delete(order);
                    break;
            }
        }
        private void Edit(InOutStockVM inOutStockVM)
        {
            InOutStockEdit inOutStockEdit = new InOutStockEdit();
            inOutStockEdit.Tag = inOutStockVM;
            FrmPop.Show(this, "编辑出入库", inOutStockEdit, (() =>
            {
                this.pageNavigator.Skip = 0;
                InitData();
            }));
        }

        private void Delete(InOutStockVM inOutStockVM)
        {
            if (_bll.DeleteOrder(inOutStockVM.ID) > 0)
            {
                XtraMessageBox.Show($"删除成功！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                XtraMessageBox.Show($"删除失败！", "提示", MessageBoxButtons.OK);
            }
            this.pageNavigator.Skip = 0;
            InitData();
        }
        private void gvInOutStockOrder_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = this.gvInOutStockOrder.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内  
                if (hInfo.InRow)
                {
                    int[] selectedRowHandles = this.gvInOutStockOrder.GetSelectedRows();
                    foreach (var i in selectedRowHandles)
                    {
                        this.gvInOutStockOrder.UnselectRow(i);
                    }
                    this.gvInOutStockOrder.SelectRow(hInfo.RowHandle);
                    List<InOutStockVM> list = this.gcInOutStockOrder.DataSource as List<InOutStockVM>;

                    ////取得选定行信息  
                    InOutStockVM order = this.gvInOutStockOrder.GetRow(hInfo.RowHandle) as InOutStockVM;//获取实体
                    this.gcInOutStockDetail.DataSource = _bll.GetInOutStockDetail(order.ID);
                }
            }
        }

        private void ribtDeleteOrder_Click(object sender, EventArgs e)
        {
            InOutStockVM inOutStockVM = this.gvInOutStockOrder.GetRow(gvInOutStockOrder.FocusedRowHandle) as InOutStockVM;//获取实体
            Delete(inOutStockVM);
        }

        private void pageNavigator_PageIndexChanged(int take, int skip)
        {
            InitData();
        }
    }
}
