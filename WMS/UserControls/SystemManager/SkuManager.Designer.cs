namespace WMS.UserControls.SystemManager
{
    partial class SkuManager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sbtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.teUnit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.sbtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gridSku = new DevExpress.XtraGrid.GridControl();
            this.gridViewSku = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSku)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSku)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbtnUpdate);
            this.panelControl1.Controls.Add(this.sbtnDelete);
            this.panelControl1.Controls.Add(this.sbtnQuery);
            this.panelControl1.Controls.Add(this.textEdit2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(685, 80);
            this.panelControl1.TabIndex = 0;
            // 
            // sbtnUpdate
            // 
            this.sbtnUpdate.Location = new System.Drawing.Point(5, 51);
            this.sbtnUpdate.Name = "sbtnUpdate";
            this.sbtnUpdate.Size = new System.Drawing.Size(58, 23);
            this.sbtnUpdate.TabIndex = 7;
            this.sbtnUpdate.Text = "修改";
            this.sbtnUpdate.Click += new System.EventHandler(this.sbtnUpdate_Click);
            // 
            // sbtnDelete
            // 
            this.sbtnDelete.Location = new System.Drawing.Point(79, 51);
            this.sbtnDelete.Name = "sbtnDelete";
            this.sbtnDelete.Size = new System.Drawing.Size(58, 23);
            this.sbtnDelete.TabIndex = 5;
            this.sbtnDelete.Text = "删除";
            this.sbtnDelete.Click += new System.EventHandler(this.sbtnDelete_Click);
            // 
            // sbtnQuery
            // 
            this.sbtnQuery.Location = new System.Drawing.Point(217, 15);
            this.sbtnQuery.Name = "sbtnQuery";
            this.sbtnQuery.Size = new System.Drawing.Size(63, 23);
            this.sbtnQuery.TabIndex = 4;
            this.sbtnQuery.Text = "查询";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(79, 16);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(100, 20);
            this.textEdit2.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(25, 18);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "单位名称：";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.teUnit);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.sbtnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(435, 80);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(250, 612);
            this.panelControl2.TabIndex = 2;
            // 
            // teUnit
            // 
            this.teUnit.Location = new System.Drawing.Point(71, 30);
            this.teUnit.Name = "teUnit";
            this.teUnit.Size = new System.Drawing.Size(137, 20);
            this.teUnit.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 33);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "单位名称：";
            // 
            // sbtnSave
            // 
            this.sbtnSave.Location = new System.Drawing.Point(133, 77);
            this.sbtnSave.Name = "sbtnSave";
            this.sbtnSave.Size = new System.Drawing.Size(75, 23);
            this.sbtnSave.TabIndex = 5;
            this.sbtnSave.Text = "保存";
            this.sbtnSave.Click += new System.EventHandler(this.sbtnSave_Click);
            // 
            // gridSku
            // 
            this.gridSku.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSku.Location = new System.Drawing.Point(0, 80);
            this.gridSku.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            this.gridSku.MainView = this.gridViewSku;
            this.gridSku.Name = "gridSku";
            this.gridSku.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridSku.Size = new System.Drawing.Size(435, 612);
            this.gridSku.TabIndex = 3;
            this.gridSku.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSku});
            // 
            // gridViewSku
            // 
            this.gridViewSku.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridViewSku.GridControl = this.gridSku;
            this.gridViewSku.IndicatorWidth = 33;
            this.gridViewSku.Name = "gridViewSku";
            this.gridViewSku.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewSku.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewSku.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewSku.OptionsCustomization.AllowFilter = false;
            this.gridViewSku.OptionsMenu.EnableColumnMenu = false;
            this.gridViewSku.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gridViewSku.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewSku.OptionsSelection.MultiSelect = true;
            this.gridViewSku.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewSku.OptionsView.ShowGroupPanel = false;
            this.gridViewSku.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.gridViewSku.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewSku_CustomDrawRowIndicator);
            this.gridViewSku.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridViewSku_SelectionChanged);
            this.gridViewSku.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewSku_MouseDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "单位名称";
            this.gridColumn2.FieldName = "Unit";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 397;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // SkuManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSku);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "SkuManager";
            this.Size = new System.Drawing.Size(685, 692);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSku)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSku)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sbtnQuery;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridSku;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSku;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.TextEdit teUnit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton sbtnSave;
        private DevExpress.XtraEditors.SimpleButton sbtnUpdate;
        private DevExpress.XtraEditors.SimpleButton sbtnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
