using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.UserControls;

namespace WMS
{
    public partial class FrmPop :XtraForm
    {
        public FrmPop()
        {
            InitializeComponent();
        }

        public static void Show(UserControl sourceControl, string text, EditUserControl child, Action action)
        {
            //加边框大小(16,48)
            Show(sourceControl, text, child, action, child.Width + 16, child.Height + 48);
        }

        public static void Show(UserControl sourceControl, string text, EditUserControl child, Action action, int width, int height)
        {
            Control control = sourceControl.Parent;
            while (true)
            {
                if (!(control is FrmMain))
                {
                    control = control.Parent;
                    continue;
                }
                else
                {
                    break;
                }
            }
            FrmPop frmPop = new FrmPop();
            frmPop.Width = width;
            frmPop.Height = height;
            frmPop.Text = text;
            frmPop.Owner = (FrmMain)control;
            frmPop.Controls.Add(child);
            child.Dock = DockStyle.Fill;
            frmPop.FormClosed += (sender, e) =>
            {
                action?.Invoke();
            };
            frmPop.ShowDialog();

        }


    }
}
