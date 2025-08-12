namespace Vision.Frm.MainForm
{
    partial class Frm_Location
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.nUD_TrayRow = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.nUD_TrayCol = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.dbtn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_TrayRow)).BeginInit();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_TrayCol)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dbtn_Save);
            this.tabPage1.Controls.Add(this.nUD_TrayCol);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.nUD_TrayRow);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(919, 649);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "上料工位";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 45;
            this.label6.Text = "料盘行";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUD_TrayRow
            // 
            this.nUD_TrayRow.Location = new System.Drawing.Point(115, 51);
            this.nUD_TrayRow.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nUD_TrayRow.Name = "nUD_TrayRow";
            this.nUD_TrayRow.Size = new System.Drawing.Size(90, 28);
            this.nUD_TrayRow.TabIndex = 46;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(927, 681);
            this.tabControl1.TabIndex = 30;
            // 
            // nUD_TrayCol
            // 
            this.nUD_TrayCol.Location = new System.Drawing.Point(115, 91);
            this.nUD_TrayCol.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nUD_TrayCol.Name = "nUD_TrayCol";
            this.nUD_TrayCol.Size = new System.Drawing.Size(90, 28);
            this.nUD_TrayCol.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 47;
            this.label1.Text = "料盘列";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dbtn_Save
            // 
            this.dbtn_Save.Location = new System.Drawing.Point(372, 517);
            this.dbtn_Save.Name = "dbtn_Save";
            this.dbtn_Save.Size = new System.Drawing.Size(112, 34);
            this.dbtn_Save.TabIndex = 49;
            this.dbtn_Save.Text = "保存";
            this.dbtn_Save.Click += new System.EventHandler(this.dbtn_Save_Click);
            // 
            // Frm_Location
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 681);
            this.Controls.Add(this.tabControl1);
            this.Name = "Frm_Location";
            this.Text = "Frm_Location";
            this.Load += new System.EventHandler(this.Frm_Location_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_TrayRow)).EndInit();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_TrayCol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown nUD_TrayCol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUD_TrayRow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private DevExpress.XtraEditors.SimpleButton dbtn_Save;
    }
}