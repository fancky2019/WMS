using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace WMS.UserControls
{
    public partial class EditUserControl : XtraUserControl
    {
        public EditUserControl()
        {
            InitializeComponent();
        }

        public virtual void Close()
        {
            Control control = this.Parent;
            while(true)
            {
                if (control is FrmPop)
                {
                    break;
                }
                else
                {
                    control = control.Parent;
                }
            }
            FrmPop frmPop = control as FrmPop;
            frmPop.Close();
        }
    }
}
