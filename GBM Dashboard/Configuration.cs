﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraVerticalGrid;

namespace GBM_Dashboard
{
    public partial class Configuration : UserControl
    {
        private static Configuration _instance;
        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Configuration();
                return _instance;
            }
        }

        public Configuration()
        {
            InitializeComponent();
        }

        private void update_db()
        {
            this.Validate();
            this.configuration_tblTableAdapter.ClearBeforeFill = true;

            this.gbm_ivaTableAdapter.Update(this.dashboardDataSet.gbm_iva);
            this.configuration_type_tblTableAdapter.Update(this.dashboardDataSet.configuration_type_tbl);
            this.configuration_tblTableAdapter.Update(this.dashboardDataSet.configuration_tbl);
            this.camera_configuration_tblTableAdapter.Update(this.dashboardDataSet.camera_configuration_tbl);

            this.gbm_ivaTableAdapter.Fill(this.dashboardDataSet.gbm_iva);
            this.configuration_type_tblTableAdapter.Fill(this.dashboardDataSet.configuration_type_tbl);
            this.configuration_tblTableAdapter.Fill(this.dashboardDataSet.configuration_tbl);
            this.camera_configuration_tblTableAdapter.Fill(this.dashboardDataSet.camera_configuration_tbl);

            this.configuration_tblTableAdapter.GetData();
            this.dashboardDataSet.configuration_tbl.AcceptChanges();

            this.camera_configuration_tblTableAdapter.Fill(this.dashboardDataSet.camera_configuration_tbl);
            this.configuration_tblTableAdapter.Fill(this.dashboardDataSet.configuration_tbl);
            this.configuration_type_tblTableAdapter.Fill(this.dashboardDataSet.configuration_type_tbl);
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dashboardDataSet.camera_configuration_tbl' table. You can move, or remove it, as needed.
            this.camera_configuration_tblTableAdapter.Fill(this.dashboardDataSet.camera_configuration_tbl);
            // TODO: This line of code loads data into the 'dashboardDataSet.configuration_tbl' table. You can move, or remove it, as needed.
            this.configuration_tblTableAdapter.Fill(this.dashboardDataSet.configuration_tbl);
            // TODO: This line of code loads data into the 'dashboardDataSet.configuration_type_tbl' table. You can move, or remove it, as needed.
            this.configuration_type_tblTableAdapter.Fill(this.dashboardDataSet.configuration_type_tbl);
            // TODO: This line of code loads data into the 'dashboardDataSet.camera_configuration_tbl' table. You can move, or remove it, as needed.
            this.camera_configuration_tblTableAdapter.Fill(this.dashboardDataSet.camera_configuration_tbl);
            // TODO: This line of code loads data into the 'dashboardDataSet.configuration_tbl' table. You can move, or remove it, as needed.
            this.configuration_tblTableAdapter.Fill(this.dashboardDataSet.configuration_tbl);
            // TODO: This line of code loads data into the 'dashboardDataSet.configuration_type_tbl' table. You can move, or remove it, as needed.
            this.configuration_type_tblTableAdapter.Fill(this.dashboardDataSet.configuration_type_tbl);
            //this.repositoryItemButtonEdit1.Click += new EventHandler(this.repositoryItemButtonEdit1_Click);

            this.gbm_ivaTableAdapter.Fill(this.dashboardDataSet.gbm_iva);

            load_ratios();

        }

        public IEnumerable<Control> GetAll(Control control)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl))
                                      .Concat(controls);
            // .Where(c => c.GetType() == type);
        }

        private void load_ratios()
        {
            const double screenHeight = 1080.0;
            const double screenWidth = 1920.0;
            //Main_Form new_form = new Main_Form();
            //double height = new_form.Height;
            //double width = new_form.Width;
            double height = Screen.AllScreens[0].Bounds.Height;
            double width = Screen.AllScreens[0].Bounds.Width;
            double height_ratio = height / screenHeight;
            double width_ratio = width / screenWidth;
            //MessageBox.Show(height_ratio.ToString() + "   " + height.ToString());
            //MessageBox.Show(width_ratio.ToString() + "   " + width.ToString());
            //            MessageBox.Show( panel1.Left.ToString());
            // (int(110 * width_ratio) + int(6 * width_ratio), 1), (int(300 * width_ratio), int(251 * height_ratio))
            var c = GetAll(this);
            //MessageBox.Show("Total Controls: " + c.Count());
            foreach (Control _control in c)
            {
                //if (_control.Visible == true )
                //{
                //    _control.Left = (int)(_control.Left * width_ratio);
                //    _control.Top = (int)(_control.Top * height_ratio);
                //    _control.Width = (int)(_control.Width * width_ratio);
                //    _control.Height = (int)(_control.Height * height_ratio);
                //}
                _control.Left = (int)(_control.Left * width_ratio);
                _control.Top = (int)(_control.Top * height_ratio);
                _control.Width = (int)(_control.Width * width_ratio);
                _control.Height = (int)(_control.Height * height_ratio);
            }
        }



        private void controlNavigator1_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            //MessageBox.Show(e.Button.ButtonType.ToString());
            if (e.Button.ButtonType.ToString() != "Append")
            {
                //MessageBox.Show(e.Button.ButtonType.ToString());
                update_db();
            }
            if (e.Button.ButtonType.ToString() != "Remove")
            {
                //Testing git commit
                //MessageBox.Show(e.Button.ButtonType.ToString());
                update_db();
            }
        }

        private void gridControl1_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Up || e.KeyChar == (char)Keys.Down
             || e.KeyChar == (char)Keys.PageUp || e.KeyChar == (char)Keys.PageDown)
            {
                update_db();
            }
            if (e.KeyChar == (char)Keys.Insert)
            {
                this.controlNavigator1.NavigatableControl.DoAction(NavigatorButtonType.Append);
            }
            if (e.KeyChar.ToString() == Keys.Delete.ToString())
            {
                this.controlNavigator1.NavigatableControl.DoAction(NavigatorButtonType.Remove);
                update_db();
            }
        }

        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down
              || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
            {
                update_db();
            }
            if (e.KeyCode == Keys.Insert)
            {
                this.controlNavigator1.NavigatableControl.DoAction(NavigatorButtonType.Append);
            }
            if (e.KeyCode.ToString() == Keys.Delete.ToString())
            {
                //////MessageBox.Show("Remove");
                this.controlNavigator1.NavigatableControl.DoAction(NavigatorButtonType.Remove);
                update_db();
            }
        }

        private void gridControl2_ProcessGridKey(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down
              || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
            {
                update_db();
            }
            if (e.KeyCode == Keys.Insert)
            {
                this.controlNavigator3.NavigatableControl.DoAction(NavigatorButtonType.Append);
            }
            if (e.KeyCode.ToString() == Keys.Delete.ToString())
            {
                this.controlNavigator3.NavigatableControl.DoAction(NavigatorButtonType.Remove);
                update_db();
            }
        }

        private void vGridControl1_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Up || e.KeyChar == (char)Keys.Down
              || e.KeyChar == (char)Keys.PageUp || e.KeyChar == (char)Keys.PageDown)
            {
                update_db();
            }
            if (e.KeyChar == (char)Keys.Insert)
            {
                this.controlNavigator2.NavigatableControl.DoAction(NavigatorButtonType.Append);
            }
            if (e.KeyChar.ToString() == Keys.Delete.ToString())
            {
                this.controlNavigator2.NavigatableControl.DoAction(NavigatorButtonType.Remove);
            }
        }

        private void vGridControl1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            VGridControl vGrid = sender as VGridControl;
            if (e.KeyCode == Keys.Delete)
            {
                //VGridControl vGrid = vGridControl1 as VGridControl;
                BindingSource bindingSource = vGrid.DataSource as BindingSource;
                if (bindingSource != null)
                {
                    DataRowView sourceView = bindingSource.Current as DataRowView;
                    DialogResult result = MessageBox.Show("Delete record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        sourceView.Delete();
                        update_db();
                    }
                }
            }

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                (sender as ButtonEdit).EditValue = ofd.FileName;

        }

        private void controlNavigator2_Click(object sender, EventArgs e)
        {

        }

        private void controlNavigator2_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType.ToString() == "Remove")
            {
                {
                    return;
                }

            }
            if ((e.Button.ButtonType.ToString() == "CancelEdit") || (e.Button.ButtonType.ToString() == "EndEdit"))
            {

            }
            if (e.Button.ButtonType.ToString() != "Append")
            {
                update_db();
            }
        }

        private void controlNavigator3_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType.ToString() == "Remove")
            {
                DialogResult result = MessageBox.Show("Delete record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    return;
                }
                update_db();
            }
            if ((e.Button.ButtonType.ToString() == "CancelEdit") || (e.Button.ButtonType.ToString() == "EndEdit"))
            {

            }
            if (e.Button.ButtonType.ToString() != "Append")
            {
                update_db();
            }
        }

        private void controlNavigator3_Click(object sender, EventArgs e)
        {

        }

        private void gridControl3_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down
              || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
            {
                update_db();
            }
            if (e.KeyCode == Keys.Insert)
            {
                this.controlNavigator4.NavigatableControl.DoAction(NavigatorButtonType.Append);
            }
            if (e.KeyCode.ToString() == Keys.Delete.ToString())
            {
                this.controlNavigator4.NavigatableControl.DoAction(NavigatorButtonType.Remove);
                update_db();
            }
        }

        private void controlNavigator4_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType.ToString() != "Append")
            {
                update_db();
            }
            if (e.Button.ButtonType.ToString() != "Remove")
            {
                update_db();
            }
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                (sender as ButtonEdit).EditValue = ofd.FileName;

        }

        private void gridControl3_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Up || e.KeyChar == (char)Keys.Down
             || e.KeyChar == (char)Keys.PageUp || e.KeyChar == (char)Keys.PageDown)
            {
                update_db();
            }
            if (e.KeyChar == (char)Keys.Insert)
            {
                this.controlNavigator4.NavigatableControl.DoAction(NavigatorButtonType.Append);
            }
            if (e.KeyChar.ToString() == Keys.Delete.ToString())
            {
                this.controlNavigator4.NavigatableControl.DoAction(NavigatorButtonType.Remove);
                update_db();
            }
        }

        private void gridControl3_KeyDown(object sender, KeyEventArgs e)
        {
            GridControl gridControl = sender as GridControl;
            if (e.KeyCode == Keys.Delete)
            {
                //VGridControl vGrid = vGridControl1 as VGridControl;
                BindingSource bindingSource = gridControl.DataSource as BindingSource;
                if (bindingSource != null)
                {
                    DataRowView sourceView = bindingSource.Current as DataRowView;
                    DialogResult result = MessageBox.Show("Delete record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        sourceView.Delete();
                        update_db();
                    }
                }
            }
        }

        private void gridControl3_BackColorChanged(object sender, EventArgs e)
        {
            gridControl3.BackColor = Color.LightGreen;
        }
    }

}