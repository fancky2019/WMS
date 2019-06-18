using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.UserControls;

namespace WMS
{
    public partial class FrmMain : RibbonForm
    {
        Type[] types = null;
        public FrmMain()
        {
            InitializeComponent();
            Assembly assembly = Assembly.GetExecutingAssembly();
            types = assembly.GetTypes();
        }

        private async void ribbonControl1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ssmAddItem.IsSplashFormVisible)//如果已ShowWaitForm,return
            {
                return;
            }
            //WaitDialogForm
            ssmAddItem.ShowWaitForm();
            ssmAddItem.SetWaitFormCaption("提示");
            ssmAddItem.SetWaitFormDescription("数据加载中...");
            await Task.Run(() =>
            {
                Type type = null;
                if(e.Item.Tag==null)
                {
                    return;
                }
                string controlName = e.Item.Tag.ToString();
                foreach (var t in types)
                {
                    if (t.Name == controlName)
                    {
                        type = t;
                        break;
                    }
                }

                var control = type != null ? Activator.CreateInstance(type) : new Developing();
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    AddXtrTabPages(e.Item.Caption, control);

                }));
            });
            ssmAddItem.CloseWaitForm();
        }

        private void AddXtrTabPages(string header, object obj)
        {
            Control control = obj as Control;
            IEnumerable<XtraTabPage> res = this.xtraTabControl1.TabPages.Where(p => p.Text.Contains(header));
            if (res.Count() > 0)
            {
                this.xtraTabControl1.SelectedTabPage = res.FirstOrDefault();
                return;
            }
            XtraTabPage xtraTabPage = new XtraTabPage();
            xtraTabPage.Name = header;
            xtraTabPage.Size = new System.Drawing.Size(401, 288);
            xtraTabPage.Text = header;
            this.xtraTabControl1.TabPages.Add(xtraTabPage);
            this.xtraTabControl1.SelectedTabPage = xtraTabPage;

            xtraTabPage.Controls.Add(control);
            xtraTabPage.Controls[0].Dock = DockStyle.Fill;

        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            //xtraTabControl1.TabPages.Remove(this.xtraTabControl1.SelectedTabPage);
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs EArg = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            string name = EArg.Page.Text;//得到关闭的选项卡的text
            foreach (XtraTabPage page in xtraTabControl1.TabPages)//遍历得到和关闭的选项卡一样的Text
            {
                if (page.Text == name)
                {
                    xtraTabControl1.TabPages.Remove(page);
                    page.Dispose();
                    return;
                }
            }

        }
    }
}
