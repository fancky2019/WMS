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
using DevExpress.XtraEditors;
using WMS.Model.EntityModels;
using WMS.Model.ViewModels;
using WMS.BLL.StockManager;
using WMS.BLL.ProductManager;

namespace WMS.UserControls.ProcuctManager
{
    public partial class ProductEdit : EditUserControl
    {
        StockManagerBll _stockBll = null;
        ProductManagerBll _productBll = null;
        SkuManagerBll _skuBll = null;
        public ProductEdit()
        {
            InitializeComponent();
            _stockBll = new StockManagerBll();
            _productBll = new ProductManagerBll();
            _skuBll = new SkuManagerBll();

        }
        private void ProductEdit_Load(object sender, EventArgs e)
        {
            this.slueStock.Properties.DataSource = _stockBll.LoadData();
            this.slueSku.Properties.DataSource = _skuBll.LoadData();
            if (this.Tag != null)
            {
                ProductVM product = this.Tag as ProductVM;
                this.teProcuctName.Text = product.ProductName;
                this.teProductStyle.Text = product.ProductStyle;
                this.tePrice.Text = product.Price.ToString();
                this.slueStock.EditValue = product.StockID;
                this.slueSku.EditValue = product.SkuID;
            }
        }
        private void sbtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teProcuctName.Text.Trim()))
            {
                XtraMessageBox.Show("产品名称不能为空！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (slueSku.EditValue == null)
            {
                XtraMessageBox.Show("产品单位不能为空！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrEmpty(tePrice.Text.Trim()))
            {
                XtraMessageBox.Show("产品价格不能为空！", "提示", MessageBoxButtons.OK);
                return;
            }

            Product product = new Product()
            {
                ProductName = this.teProcuctName.Text.Trim(),
                SkuID = (int)this.slueSku.EditValue,
                ProductStyle = this.teProductStyle.Text.Trim(),
                Price = decimal.Parse(this.tePrice.Text.Trim()),
                StockID =(int?)this.slueStock.EditValue,
                GUID = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                ModifyTime= DateTime.Now,
                Status=1
            };
            int result = 0;
            if (this.Tag != null)//编辑
            {
                var oldProduct = this.Tag as ProductVM;
                product.GUID = oldProduct.GUID;
                product.ID = oldProduct.ID;
                product.CreateTime = oldProduct.CreateTime;
                product.ModifyTime = DateTime.Now;
                result = _productBll.Update(product);
            }
            else//新增
            {
                if(_productBll.Exist(product))
                {
                    XtraMessageBox.Show("已存在同型号的该产品！", "提示", MessageBoxButtons.OK);
                    return;
                }
                result = _productBll.Add(product);
            }
            if (result > 0)
            {
                XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
