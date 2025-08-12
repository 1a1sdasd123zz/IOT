using System;
using System.IO;
using System.Windows.Forms;
using Vision.BaseClass.VisionConfig;
using Vision.BaseClass.Helper;
using Vision.BaseClass.Locaction;

namespace Vision.Frm.MainForm;

public partial class Frm_Location : Form
{
    private JobData mJobData;
    public Frm_Location(JobData jobData)
    {
        InitializeComponent();
        mJobData = jobData;
    }

    private void Frm_Location_Load(object sender, EventArgs e)
    {
        SetControlValue();
    }

    private void SetControlValue()
    {
        try
        {
            nUD_TrayRow.Value = mJobData.mVisionParameters.TrayRow;
            nUD_TrayCol.Value = mJobData.mVisionParameters.TrayCol;
        }
        catch
        {
            // ignored
        }
    }

    private void dbtn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            mJobData.mVisionParameters.TrayRow = (int)nUD_TrayRow.Value;
            mJobData.mVisionParameters.TrayCol = (int)nUD_TrayCol.Value;
            if (File.Exists(mJobData.VisionParametersPath))
            {
                XmlHelper.WriteXML(mJobData.mVisionParameters, mJobData.VisionParametersPath, typeof(VisionParameters));
                MessageBox.Show("保存成功");
            }
            else
            {
                File.Create(mJobData.VisionParametersPath);
                XmlHelper.WriteXML(mJobData.mVisionParameters, mJobData.VisionParametersPath, typeof(VisionParameters));
                MessageBox.Show("保存成功");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("保存失败" + ex);
        }
    }
}