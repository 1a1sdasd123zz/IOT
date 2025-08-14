using System.Windows.Forms;

namespace Vision.Frm.MainForm
{
    partial class Frm_Main
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.登录toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产品型号配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.系统在线toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯模块toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.硬件模块toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.硬件配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Camera2DtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.算法模块toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.算法配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.定位参数配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图适应ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Error = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_2D = new System.Windows.Forms.ToolStripMenuItem();
            this.Window_2DChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_3D = new System.Windows.Forms.ToolStripMenuItem();
            this.Window_3DChange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip_Auth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_User = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_Logout = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStrip_JobNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_State = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_CommStateA = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_MemoryConsume = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.chineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dockPanel_Main = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登录toolStripMenuItem,
            this.系统配置toolStripMenuItem,
            this.通讯模块toolStripMenuItem,
            this.硬件模块toolStripMenuItem,
            this.算法模块toolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1718, 32);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 登录toolStripMenuItem
            // 
            this.登录toolStripMenuItem.Name = "登录toolStripMenuItem";
            this.登录toolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.登录toolStripMenuItem.Text = "登录";
            this.登录toolStripMenuItem.Click += new System.EventHandler(this.登录toolStripMenuItem_Click);
            // 
            // 系统配置toolStripMenuItem
            // 
            this.系统配置toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.产品型号配置toolStripMenuItem,
            this.toolStripMenuItem1,
            this.系统在线toolStripMenuItem});
            this.系统配置toolStripMenuItem.Enabled = false;
            this.系统配置toolStripMenuItem.Name = "系统配置toolStripMenuItem";
            this.系统配置toolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.系统配置toolStripMenuItem.Text = "系统配置";
            // 
            // 产品型号配置toolStripMenuItem
            // 
            this.产品型号配置toolStripMenuItem.Name = "产品型号配置toolStripMenuItem";
            this.产品型号配置toolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.产品型号配置toolStripMenuItem.Text = "产品型号配置";
            this.产品型号配置toolStripMenuItem.Click += new System.EventHandler(this.产品型号配置toolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(218, 34);
            this.toolStripMenuItem1.Text = "文件参数配置";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click_1);
            // 
            // 系统在线toolStripMenuItem
            // 
            this.系统在线toolStripMenuItem.Name = "系统在线toolStripMenuItem";
            this.系统在线toolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.系统在线toolStripMenuItem.Text = "系统在线";
            this.系统在线toolStripMenuItem.Click += new System.EventHandler(this.系统在线toolStripMenuItem_Click);
            // 
            // 通讯模块toolStripMenuItem
            // 
            this.通讯模块toolStripMenuItem.Enabled = false;
            this.通讯模块toolStripMenuItem.Name = "通讯模块toolStripMenuItem";
            this.通讯模块toolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.通讯模块toolStripMenuItem.Text = "通讯模块";
            // 
            // 硬件模块toolStripMenuItem
            // 
            this.硬件模块toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.硬件配置toolStripMenuItem,
            this.toolStripMenuItem3});
            this.硬件模块toolStripMenuItem.Enabled = false;
            this.硬件模块toolStripMenuItem.Name = "硬件模块toolStripMenuItem";
            this.硬件模块toolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.硬件模块toolStripMenuItem.Text = "硬件模块";
            // 
            // 硬件配置toolStripMenuItem
            // 
            this.硬件配置toolStripMenuItem.Name = "硬件配置toolStripMenuItem";
            this.硬件配置toolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.硬件配置toolStripMenuItem.Text = "硬件配置";
            this.硬件配置toolStripMenuItem.Click += new System.EventHandler(this.硬件配置toolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Camera2DtoolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(182, 34);
            this.toolStripMenuItem3.Text = "相机配置";
            // 
            // Camera2DtoolStripMenuItem
            // 
            this.Camera2DtoolStripMenuItem.Name = "Camera2DtoolStripMenuItem";
            this.Camera2DtoolStripMenuItem.Size = new System.Drawing.Size(243, 34);
            this.Camera2DtoolStripMenuItem.Text = "2D面阵相机配置";
            this.Camera2DtoolStripMenuItem.Click += new System.EventHandler(this.Camera2DtoolStripMenuItem_Click);
            // 
            // 算法模块toolStripMenuItem
            // 
            this.算法模块toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.算法配置toolStripMenuItem,
            this.定位参数配置toolStripMenuItem});
            this.算法模块toolStripMenuItem.Enabled = false;
            this.算法模块toolStripMenuItem.Name = "算法模块toolStripMenuItem";
            this.算法模块toolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.算法模块toolStripMenuItem.Text = "算法模块";
            // 
            // 算法配置toolStripMenuItem
            // 
            this.算法配置toolStripMenuItem.Name = "算法配置toolStripMenuItem";
            this.算法配置toolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.算法配置toolStripMenuItem.Text = "算法配置";
            this.算法配置toolStripMenuItem.Click += new System.EventHandler(this.算法配置toolStripMenuItem_Click_1);
            // 
            // 定位参数配置toolStripMenuItem
            // 
            this.定位参数配置toolStripMenuItem.Name = "定位参数配置toolStripMenuItem";
            this.定位参数配置toolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.定位参数配置toolStripMenuItem.Text = "视觉参数配置";
            this.定位参数配置toolStripMenuItem.Click += new System.EventHandler(this.定位参数配置toolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.视图适应ToolStripMenuItem,
            this.tsmi_Log,
            this.tsmi_Error,
            this.tsmi_2D,
            this.tsmi_3D});
            this.视图ToolStripMenuItem.Enabled = false;
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.视图ToolStripMenuItem.Text = "视图编辑";
            // 
            // 视图适应ToolStripMenuItem
            // 
            this.视图适应ToolStripMenuItem.Name = "视图适应ToolStripMenuItem";
            this.视图适应ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.视图适应ToolStripMenuItem.Text = "视图适应";
            this.视图适应ToolStripMenuItem.Click += new System.EventHandler(this.视图适应ToolStripMenuItem_Click);
            // 
            // tsmi_Log
            // 
            this.tsmi_Log.Name = "tsmi_Log";
            this.tsmi_Log.Size = new System.Drawing.Size(182, 34);
            this.tsmi_Log.Text = "日志栏";
            // 
            // tsmi_Error
            // 
            this.tsmi_Error.Name = "tsmi_Error";
            this.tsmi_Error.Size = new System.Drawing.Size(182, 34);
            this.tsmi_Error.Text = "错误栏";
            // 
            // tsmi_2D
            // 
            this.tsmi_2D.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Window_2DChange});
            this.tsmi_2D.Name = "tsmi_2D";
            this.tsmi_2D.Size = new System.Drawing.Size(182, 34);
            this.tsmi_2D.Text = "2D视图";
            // 
            // Window_2DChange
            // 
            this.Window_2DChange.Name = "Window_2DChange";
            this.Window_2DChange.Size = new System.Drawing.Size(182, 34);
            this.Window_2DChange.Text = "视图编辑";
            this.Window_2DChange.Click += new System.EventHandler(this.Window_2DChange_Click);
            // 
            // tsmi_3D
            // 
            this.tsmi_3D.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Window_3DChange});
            this.tsmi_3D.Name = "tsmi_3D";
            this.tsmi_3D.Size = new System.Drawing.Size(182, 34);
            this.tsmi_3D.Text = "3D视图";
            this.tsmi_3D.Visible = false;
            // 
            // Window_3DChange
            // 
            this.Window_3DChange.Name = "Window_3DChange";
            this.Window_3DChange.Size = new System.Drawing.Size(182, 34);
            this.Window_3DChange.Text = "视觉编辑";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(62, 28);
            this.toolStripMenuItem2.Text = "光源";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(182, 34);
            this.toolStripMenuItem4.Text = "取料光源";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(182, 34);
            this.toolStripMenuItem5.Text = "放料光源";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Auth,
            this.toolStrip_User,
            this.toolStrip_Logout,
            this.toolStrip_JobNo,
            this.toolStrip_State,
            this.toolStrip_CommStateA,
            this.toolStripStatusLabel1,
            this.tssl_MemoryConsume,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 666);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1718, 35);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip_Auth
            // 
            this.toolStrip_Auth.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStrip_Auth.Name = "toolStrip_Auth";
            this.toolStrip_Auth.Size = new System.Drawing.Size(46, 30);
            this.toolStrip_Auth.Text = "权限";
            this.toolStrip_Auth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip_User
            // 
            this.toolStrip_User.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_User.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStrip_User.Name = "toolStrip_User";
            this.toolStrip_User.Size = new System.Drawing.Size(50, 30);
            this.toolStrip_User.Text = "用户";
            // 
            // toolStrip_Logout
            // 
            this.toolStrip_Logout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStrip_Logout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Logout.Margin = new System.Windows.Forms.Padding(0, 2, 20, 0);
            this.toolStrip_Logout.Name = "toolStrip_Logout";
            this.toolStrip_Logout.Size = new System.Drawing.Size(67, 33);
            this.toolStrip_Logout.Text = "注销";
            // 
            // toolStrip_JobNo
            // 
            this.toolStrip_JobNo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStrip_JobNo.Name = "toolStrip_JobNo";
            this.toolStrip_JobNo.Size = new System.Drawing.Size(50, 28);
            this.toolStrip_JobNo.Text = "型号";
            // 
            // toolStrip_State
            // 
            this.toolStrip_State.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip_State.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_State.ForeColor = System.Drawing.Color.Red;
            this.toolStrip_State.Name = "toolStrip_State";
            this.toolStrip_State.Size = new System.Drawing.Size(130, 28);
            this.toolStrip_State.Text = "系统：OffLine";
            // 
            // toolStrip_CommStateA
            // 
            this.toolStrip_CommStateA.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_CommStateA.ForeColor = System.Drawing.Color.Red;
            this.toolStrip_CommStateA.Name = "toolStrip_CommStateA";
            this.toolStrip_CommStateA.Size = new System.Drawing.Size(163, 28);
            this.toolStrip_CommStateA.Text = "通讯：Disconnect";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Navy;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(136, 28);
            this.toolStripStatusLabel1.Text = "当前内存消耗：";
            // 
            // tssl_MemoryConsume
            // 
            this.tssl_MemoryConsume.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssl_MemoryConsume.Name = "tssl_MemoryConsume";
            this.tssl_MemoryConsume.Size = new System.Drawing.Size(54, 28);
            this.tssl_MemoryConsume.Text = "0MB";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chineseToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(103, 32);
            this.toolStripSplitButton1.Text = "语言切换";
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Name = "chineseToolStripMenuItem";
            this.chineseToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.chineseToolStripMenuItem.Text = "中文";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.englishToolStripMenuItem.Text = "English";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dockPanel_Main, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1718, 634);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // dockPanel_Main
            // 
            this.dockPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel_Main.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.dockPanel_Main.Location = new System.Drawing.Point(3, 3);
            this.dockPanel_Main.Name = "dockPanel_Main";
            this.dockPanel_Main.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel_Main.ShowAutoHideContentOnHover = false;
            this.dockPanel_Main.Size = new System.Drawing.Size(1712, 628);
            this.dockPanel_Main.TabIndex = 11;
            this.dockPanel_Main.Theme = this.vS2015BlueTheme1;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1718, 701);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 登录toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 产品型号配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 系统在线toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通讯模块toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 硬件模块toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 硬件配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem Camera2DtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 算法模块toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 算法配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 定位参数配置toolStripMenuItem;
            private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStrip_Auth;
        private ToolStripStatusLabel toolStrip_User;
        private ToolStripSplitButton toolStrip_Logout;
        private ToolStripStatusLabel toolStrip_JobNo;
        private ToolStripStatusLabel toolStrip_State;
        private ToolStripStatusLabel toolStrip_CommStateA;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tssl_MemoryConsume;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem chineseToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripMenuItem 视图ToolStripMenuItem;
        private ToolStripMenuItem 视图适应ToolStripMenuItem;
        public ToolStripMenuItem tsmi_2D;
        private ToolStripMenuItem Window_2DChange;
        public ToolStripMenuItem tsmi_3D;
        private ToolStripMenuItem Window_3DChange;
        public ToolStripMenuItem tsmi_Log;
        public ToolStripMenuItem tsmi_Error;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
        private TableLayoutPanel tableLayoutPanel1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel_Main;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
    }
}

