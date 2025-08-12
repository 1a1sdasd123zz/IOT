
namespace Vision.Frm
{
    partial class Frm_Tool
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
      this.cmb = new System.Windows.Forms.ComboBox();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.button_LoadLocalImage = new System.Windows.Forms.Button();
      this.btn_Save = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.Controls.Add(this.cogToolBlockEditV21, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.cmb, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1200, 675);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // cogToolBlockEditV21
      // 
      this.cogToolBlockEditV21.AllowDrop = true;
      this.cogToolBlockEditV21.ContextMenuCustomizer = null;
      this.cogToolBlockEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cogToolBlockEditV21.Location = new System.Drawing.Point(4, 49);
      this.cogToolBlockEditV21.Margin = new System.Windows.Forms.Padding(4);
      this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(734, 0);
      this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
      this.cogToolBlockEditV21.ShowNodeToolTips = true;
      this.cogToolBlockEditV21.Size = new System.Drawing.Size(1192, 562);
      this.cogToolBlockEditV21.SuspendElectricRuns = false;
      this.cogToolBlockEditV21.TabIndex = 0;
      // 
      // cmb
      // 
      this.cmb.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cmb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.cmb.FormattingEnabled = true;
      this.cmb.Location = new System.Drawing.Point(4, 4);
      this.cmb.Margin = new System.Windows.Forms.Padding(4);
      this.cmb.Name = "cmb";
      this.cmb.Size = new System.Drawing.Size(1192, 32);
      this.cmb.TabIndex = 1;
      this.cmb.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Controls.Add(this.button_LoadLocalImage, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.btn_Save, 1, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 619);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(1192, 52);
      this.tableLayoutPanel2.TabIndex = 3;
      // 
      // button_LoadLocalImage
      // 
      this.button_LoadLocalImage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button_LoadLocalImage.Location = new System.Drawing.Point(4, 4);
      this.button_LoadLocalImage.Margin = new System.Windows.Forms.Padding(4);
      this.button_LoadLocalImage.Name = "button_LoadLocalImage";
      this.button_LoadLocalImage.Size = new System.Drawing.Size(588, 44);
      this.button_LoadLocalImage.TabIndex = 4;
      this.button_LoadLocalImage.Text = "加载本地图片";
      this.button_LoadLocalImage.UseVisualStyleBackColor = true;
      this.button_LoadLocalImage.Click += new System.EventHandler(this.button_LoadLocalImage_Click);
      // 
      // btn_Save
      // 
      this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btn_Save.Location = new System.Drawing.Point(600, 4);
      this.btn_Save.Margin = new System.Windows.Forms.Padding(4);
      this.btn_Save.Name = "btn_Save";
      this.btn_Save.Size = new System.Drawing.Size(588, 44);
      this.btn_Save.TabIndex = 2;
      this.btn_Save.Text = "保存";
      this.btn_Save.UseVisualStyleBackColor = true;
      this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
      // 
      // Frm_Tool
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1200, 675);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "Frm_Tool";
      this.Text = "Frm_Tool";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Tool_FormClosing);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        private System.Windows.Forms.ComboBox cmb;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button_LoadLocalImage;
    }
}