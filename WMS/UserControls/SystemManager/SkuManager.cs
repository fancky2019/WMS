using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Model;
using WMS.BLL.SystemManager;
using WMS.Common;
using WMS.Model.ViewModels;
using WMS.Model.EntityModels;
using DevExpress.XtraEditors;

namespace WMS.UserControls.SystemManager
{
    public partial class SkuManager : UserControl
    {
        SkuManagerBll _bll = null;
        Sku _selectedSku = null;
        public SkuManager()
        {
            InitializeComponent();
            _bll = new SkuManagerBll();
            this.gridSku.DataSource = _bll.LoadData();
        }

        private void gridViewSku_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        //保存
        private void sbtnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(teUnit.Text.Trim()))
            {
                return;
            }
            if (_bll.Exist(new Sku() { Unit=this.teUnit.Text.Trim()}))
            {
                XtraMessageBox.Show($"已存在单位{teUnit.Text.Trim()}！", "提示", MessageBoxButtons.OK);
                return;
            }
            int reuslt = 0;
            if (_selectedSku == null)//新增
            {
                _selectedSku = new Sku();
                _selectedSku.Unit = this.teUnit.Text.Trim();
                reuslt = _bll.Add(_selectedSku);

            }
            else//编辑
            {
                _selectedSku.Unit = this.teUnit.Text.Trim();
                reuslt = _bll.Update(_selectedSku);
            }

            if (reuslt > 0)
            {
                XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK);
                this.gridSku.DataSource = _bll.LoadData();
                this.teUnit.Text = string.Empty;
                _selectedSku = null;
            }
            else
            {
                XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK);
            }


        }

        //删除
        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            List<int> selectedIDList = new List<int>();
            int[] rowHandles = this.gridViewSku.GetSelectedRows();//获取选中行号；
            if (rowHandles.Length == 0)
            {
                XtraMessageBox.Show("请选择要操作的行记录！", "提示", MessageBoxButtons.OK);
            }
            if (DialogResult.OK == XtraMessageBox.Show("您确定要删除吗？", "警告", MessageBoxButtons.OKCancel))
            {

                foreach (var rowHandle in rowHandles)
                {
                    //获取选中行的ID
                    selectedIDList.Add(int.Parse(this.gridViewSku.GetRowCellValue(rowHandle, "ID").ToString()));
                }
                if (_bll.Delete(selectedIDList) > 0)
                {
                    XtraMessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK);
                    this.gridSku.DataSource = _bll.LoadData();
                }
                else
                {
                    XtraMessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK);
                }
            }
        }

        //双击行编辑
        private void gridViewSku_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridViewSku.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内  
                if (hInfo.InRow)
                {
                    //取得选定行信息  
                    //gridViewSku.UnselectRow(hInfo.RowHandle);//取消选中行
                    gridViewSku.SelectRow(hInfo.RowHandle);//选中行
                    _selectedSku = this.gridViewSku.GetRow(hInfo.RowHandle) as Sku;//获取选中行的实体
                    this.teUnit.Text = _selectedSku.Unit;
                }

            }
        }

        //修改
        private void sbtnUpdate_Click(object sender, EventArgs e)
        {
            int[] rowHandles = this.gridViewSku.GetSelectedRows();//获取选中行号；
            if (rowHandles.Length == 0)
            {
                XtraMessageBox.Show("请选择要操作的行记录！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (rowHandles.Length > 1)
            {
                XtraMessageBox.Show("修改操作只能选择一行记录！", "提示", MessageBoxButtons.OK);
                return;
            }
            _selectedSku = this.gridViewSku.GetRow(rowHandles[0]) as Sku;//获取选中行的实体
            this.teUnit.Text = _selectedSku.Unit;
        }

        //选择改变
        private void gridViewSku_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int[] rowHandles = this.gridViewSku.GetSelectedRows();//获取选中行号；
            if (rowHandles.Length == 0)
            {
                _selectedSku = null;
            }
        }
    }
}
