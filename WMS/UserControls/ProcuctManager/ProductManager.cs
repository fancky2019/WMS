using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.BLL.SystemManager;
using WMS.Model.EntityModels;
using DevExpress.XtraEditors;
using WMS.Model.QueryModels;
using WMS.Model.ViewModels;
using WMS.BLL.ProductManager;

namespace WMS.UserControls.ProcuctManager
{
    public partial class ProductManager : UserControl
    {
        ProductManagerBll _bll = null;
        public ProductManager()
        {
            InitializeComponent();
            _bll = new ProductManagerBll();
            InitData();
        }

        private void gridViewProduct_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = this.gridViewProduct.CalcHitInfo(e.Location);
            if (hi.InRow && e.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(MousePosition);
            }
        }

        private void sbtAdd_Click(object sender, EventArgs e)
        {
            ProductEdit productEdit = new ProcuctManager.ProductEdit();
            FrmPop.Show(this, "新增产品", productEdit, (() =>
             {
                 RefreshGridData();
             }));

        }

        private void gridViewProduct_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        //双击编辑行
        private void gridViewProduct_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridViewProduct.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内  
                if (hInfo.InRow)
                {
                    //取得选定行信息  
                    ProductVM product = this.gridViewProduct.GetRow(hInfo.RowHandle) as ProductVM;//获取选中行的实体
                    ProductEdit productEdit = new ProcuctManager.ProductEdit();
                    productEdit.Tag = product;
                    FrmPop.Show(this, "编辑产品", productEdit, (() =>
                    {
                        RefreshGridData();
                    }));
                }
            }
        }

        private void barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "update":
                    Edit();
                    break;
                case "delete":
                    Delete();
                    break;
            }
        }

        private void Edit()
        {
            ProductEdit productEdit = new ProcuctManager.ProductEdit();
            ProductVM product = this.gridViewProduct.GetRow(this.gridViewProduct.FocusedRowHandle) as ProductVM;
            productEdit.Tag = product;
            FrmPop.Show(this, "编辑产品", productEdit, (() =>
            {
                RefreshGridData();
            }));
        }

        private void Delete()
        {
            if (DialogResult.OK == XtraMessageBox.Show($"您确定要删除该产品信息吗？", "警告", MessageBoxButtons.OKCancel))
            {
                ProductVM product = this.gridViewProduct.GetRow(this.gridViewProduct.FocusedRowHandle) as ProductVM;
                if (_bll.Delete(new Product { ID=product.ID}) > 0)
                {
                    XtraMessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK);
                    RefreshGridData();
                }
            }
        }

        private void InitData()
        {
            ProductQM qm = new ProductQM();
            qm.Skip = this.pageNavigator.Skip;
            qm.Take = this.pageNavigator.Take;
            qm.ProductName = this.teProductName.Text.Trim();
            qm.ProductStyle = this.teProductStyle.Text.Trim();
            qm.Code = this.teBarCode.Text.Trim();
            Tuple<int, List<ProductVM>> result = _bll.LoadData(qm);
            this.gridProduct.DataSource = null;
            this.pageNavigator.Total = result.Item1;
            this.gridProduct.DataSource = result.Item2;
        }

        private void pageNavigator1_PageIndexChanged(int take, int skip)
        {
            InitData();
        }
        private void RefreshGridData()
        {
            this.pageNavigator.Skip = 0;
            InitData();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            ProductQM qm = new ProductQM();
            qm.Skip = 0;
            qm.Take = this.pageNavigator.Take;
            qm.ProductName = this.teProductName.Text.Trim();
            qm.ProductStyle = this.teProductStyle.Text.Trim();
            qm.Code = this.teBarCode.Text.Trim();
            Tuple<int, List<ProductVM>> result = _bll.LoadData(qm);
            this.gridProduct.DataSource = null;
            this.pageNavigator.Total = result.Item1;
            this.gridProduct.DataSource = result.Item2;
        }

        //没有查询到数据时候，显示提示
        private void gridViewProduct_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (this.gridViewProduct.RowCount == 0)
            {
                Font f = new Font("宋体", 12, FontStyle.Bold);
                Rectangle r = new Rectangle(gridViewProduct.GridControl.Width / 2 - 100, e.Bounds.Top + 5, e.Bounds.Right - 5, e.Bounds.Height - 5);
                e.Graphics.DrawString("没有查询到数据!", f, Brushes.Red, r);
            }
        }
    }
}
