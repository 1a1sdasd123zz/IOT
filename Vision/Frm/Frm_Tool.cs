using System;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Vision.BaseClass;
using Vision.BaseClass.Collection;

namespace Vision.Frm
{
    public partial class Frm_Tool : Form
    {
        MyDictionaryEx<CogToolBlock> TBs;
        string VppPath;
        public Frm_Tool(MyDictionaryEx<CogToolBlock> tbs,string vppPath)
        {
            InitializeComponent();
            TBs = tbs;
            VppPath = vppPath;
            this.cmb.Items.Clear();
            this.cmb.Items.AddRange(TBs.GetKeys().ToArray());
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb.SelectedIndex>=0)
            {
                cogToolBlockEditV21.Subject = TBs[cmb.SelectedItem.ToString()];
            }
        }

        private void Frm_Tool_FormClosing(object sender, FormClosingEventArgs e)
        {
            cogToolBlockEditV21.Subject = null;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb.SelectedIndex >= 0)
                {
                    CogSerializer.SaveObjectToFile(TBs[cmb.SelectedItem.ToString()], VppPath + TBs.Current_Key + ".vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                    LogUtil.Log($"保存检测工具{TBs.Current_Key}成功！");
                    MessageBox.Show($"保存检测工具{TBs.Current_Key}成功！");
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError($"保存检测工具{TBs.Current_Key}失败！");
                MessageBox.Show($"保存检测工具{TBs.Current_Key}失败！");
            }
        }

        private void button_LoadLocalImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                   string path = ofd.FileName;
                    Bitmap bmp = new Bitmap(path);
                    CogImage8Grey img = new CogImage8Grey(bmp);
                    CogToolBlock tb = cogToolBlockEditV21.Subject;
                    tb.Inputs["Image"].Value = img;
                }

            }
            catch
            {
                MessageBox.Show("加载本地图像异常", "", MessageBoxButtons.OK);
            }
        }
    }
}
