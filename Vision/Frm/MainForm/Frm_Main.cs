using NovaVision.VisionForm.MainForm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Vision.BaseClass;
using Vision.BaseClass.Authority;
using Vision.BaseClass.EnumHelper;
using Vision.BaseClass.Helper;
using Vision.BaseClass.VisionConfig;
using Vision.Frm.CarameFrm;
using Vision.Frm.StationFrm;
using Vision.Hardware;
using Vision.TaskFlow;
using Color = System.Drawing.Color;


namespace Vision.Frm.MainForm
{
    public partial class Frm_Main : Form
    {
        // 在主窗体类中保持委托的强引用
        private Action<ConnectionStatusEventArgs> _statusHandler;
        public static CancellationTokenSource cts = new CancellationTokenSource();//主窗体关闭标志

        public JobCollection mJobs;
        public JobData mJobData;
        private WorkFlow mWorkFlow;

        public string CurrentAuthority;


        private static int iOperCount;//退出登录计时
        bool IsOnLine;

        System.Timers.Timer timerDelete = new System.Timers.Timer();

        #region 界面控件
        Frm_JobConfig frmJobConfig;
        public Frm_Error frm_Error = Frm_Error.GetInstance(null, null);
        public Frm_Log frm_Log = Frm_Log.GetInstance(null, null);
        public Frm_2D frm_2D;
        #endregion


        public Frm_Main()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            InitializeComponent();
            SubscribeEvents();
        }



        private void Form_Load(object sender, EventArgs e)
        {
            if (!HslCommunication.Authorization.SetAuthorizationCode("cc792fa4-0c45-4748-a1d9-a18db8c5c3ab"))
            {
                MessageBox.Show("通信组件授权失败！请联系厂商！");
                string logmsg = "PLC 通讯组件授权失败";
                MessageBox.Show(logmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //CogStringCollection LicensedFeatures = CogLicense.GetLicensedFeatures(false, false);//9.3版本以后写法
            //if (LicensedFeatures.Count < 0 || LicensedFeatures.HasChanged == false)
            //{
            //    IsLicense=false;
            //    Monitor("软件安全许可证不存在" + ":" + "安全性冲突 (268435462)。如果软件特定功能的许可证位不存在或找不到，则会触发此错误。以下情况中有时会找不到已启用的位：Cognex 硬件设备驱动程序停止运行，“Cognex 安全性服务”或“Cognex 软件许可服务”未运行，或是已超过基于时间的许可证的到期日期。如需有关排除系统故障的进一步详细信息，请参阅文档或与“技术支持”联系。", 3);
            //     this.bt_Alarm.Text = "No License";
            //     this.bt_Alarm.BackColor = Color.Red;
            //    logmsg = "License 授权失败，请联系厂商";
            //    ImagePro.SaveLog(logmsg);
            //}

            FrmSplash frm_Splash = new FrmSplash();
            frm_Splash.Show();
            frm_Splash.lbl_Splash.Text = "系统启动中……";
            frm_Splash.lbl_Splash.Refresh();
            // 强制创建控件句柄
            var _ = frm_Error.Handle;
            _ = frm_Log.Handle;
            LogUtil.path = AppDomain.CurrentDomain.BaseDirectory + "Project\\Log";
            LogUtil.TextBoxInfo = frm_Log.txt_Info;
            LogUtil.TextBoxError = frm_Error.txt_Error;
            frm_Splash.lbl_Splash.Text = "作业加载中……";
            mJobs = new JobCollection();

            mJobs.JobChangedEvent += Jobs_JobChangedEvent;
            if (mJobs.Jobs.Count > 0)
            {
                if (mJobs.CurrentID > 0)
                {
                    mJobData = mJobs.Jobs[mJobs.CurrentName];
                    frm_Splash.lbl_Splash.Text = "作业加载完成";
                    frm_Splash.lbl_Splash.Refresh();
                    frm_Splash.lbl_Splash.Text = "硬件加载中……";
                    frm_Splash.lbl_Splash.Refresh();
                    mJobData.InitHardWare();
                    frm_Splash.lbl_Splash.Text = "硬件加载完成";
                    frm_Splash.lbl_Splash.Refresh();
                    mJobData.RegisterEvents();
                    toolStrip_JobNo.Text = $"型号：{mJobs.CurrentID}";
                }
                else
                {
                    mJobData = new JobData();
                    toolStrip_JobNo.Text = "型号：无";
                }
            }
            else
            {
                mJobData = new JobData();
                toolStrip_JobNo.Text = "型号：无";
            }
            CurrentAuthority = "空";

            mWorkFlow = new WorkFlow(mJobData);
            Load_Authority(AuthorityName.Empty, CurrentAuthority);
            // 显示主窗体后标记控件就绪
            this.Shown += (s, args) =>
            {
                LogUtil.SetControlsReady();
            };
            Show_Frm();
            //this.label_ProductName.Text = "产品名：" + mJobs.CurrentName;
            if (System.IO.File.Exists(GlobalValue.UserInfosPath))
            {
                CommonMethod commonMethod = new CommonMethod();
                GetUserInfosValue(commonMethod.Deserialize<UserInfos>(GlobalValue.UserInfosPath));
            }
            else
            {
                GlobalValue.CurrentUser = new UserInfo(UserManagement.UserType.Logout, "", "");
            }


            timerDelete.Enabled = true;
            timerDelete.Interval = 1000;
            timerDelete.Elapsed += TimerDelete_Elapsed;
            timerDelete.Start();
            IsOnLine = true;
            OnLineStateChange();
            frm_Splash.Close();
            WindowState = FormWindowState.Maximized;
        }
        private int CheckJobIsExist()
        {
            if (mJobData.ID == -1)
            {
                MessageBox.Show(@"当前作业号为空，请设置一个型号再进行相关操作！", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                return 1;
            }
            if (IsOnLine)
            {
                MessageBox.Show(@"系统在线不能进行相关操作，请将系统置为离线状态！", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                return 2;
            }
            return 0;
        }

        private void OnLineStateChange()
        {
            if (IsOnLine)
            {
                toolStrip_State.Text = "系统：OnLine";
                toolStrip_State.ForeColor = Color.Green;
                系统在线toolStripMenuItem.Text = "系统离线";
                mWorkFlow.Start();
            }
            else
            {
                toolStrip_State.Text = "系统：OffLine";
                toolStrip_State.ForeColor = Color.Red;
                系统在线toolStripMenuItem.Text = "系统在线";
                mWorkFlow.Stop();
            }
        }

        #region[窗体适应]


        public void Show_Frm()
        {
            try
            {
                dockPanel_Main.DockLeftPortion = mJobData.mUIControl.mAppConfig.DS_DockLeftPortion;
                dockPanel_Main.DockRightPortion = mJobData.mUIControl.mAppConfig.DS_DockRighttPortion;
                dockPanel_Main.DockBottomPortion = mJobData.mUIControl.mAppConfig.DS_DockBottomPortion;
                Show_Frm_2D();
                //Show_Frm_3D();
                ////Show_Frm_Statistics();
                ////Show_Frm_Data();
                Show_Frm_Log();
                Show_Frm_Error();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Show_Frm_2D()
        {
            try
            {
                frm_2D = Frm_2D.GetInstance(this, mJobData.mUIControl);
                if (mJobData.mUIControl.mAppConfig.DS_Frm_2D_Dsiplay)
                {
                    frm_2D.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_2D);
                    frm_2D.HideOnClose = true;
                    tsmi_2D.Checked = true;
                }
                else
                {
                    frm_2D.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        public void Show_Frm_3D()
        {
            try
            {
                //frm_3D = Frm_3D.GetInstance(this, mJobData.mUIControl);
                //if (mJobData.mUIControl.mAppConfig.DS_Frm_3D_Dsiplay && mJobData.mUIControl.mAppConfig.DS_Frm_2D_Dsiplay)
                //{
                //    frm_3D.Show(frm_2D.Pane, DockAlignment.Left, mJobData.mUIControl.mAppConfig.DS_3D_Size);
                //    frm_3D.HideOnClose = true;
                //    tsmi_3D.Checked = true;
                //}
                //else if (mJobData.mUIControl.mAppConfig.DS_Frm_3D_Dsiplay && !mJobData.mUIControl.mAppConfig.DS_Frm_2D_Dsiplay)
                //{
                //    frm_3D.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_3D);
                //    frm_3D.HideOnClose = true;
                //    tsmi_3D.Checked = true;
                //}
                //else
                //{
                //    frm_3D.Visible = false;
                //}
            }
            catch (Exception)
            {
            }
        }

        public void Show_Frm_Data()
        {
            //frm_Data = Frm_Data.GetInstance(this, mJobData.mUIControl.mAppConfig);
            //MultiLanguage.LoadLanguage(frm_Data, MultiLanguage.GetDefaultLanguage());
            //frm_Data.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Data);
            //frm_Data.HideOnClose = true;
            //tsmi_Data.Checked = true;
        }

        public void Show_Frm_Statistics()
        {
            //frm_Statistics = Frm_Statistics.GetInstance(this, mJobData.mUIControl.mAppConfig);
            //frm_Statistics.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Statistics);
            //frm_Statistics.HideOnClose = true;
            //tsmi_Statistics.Checked = true;
        }

        public void Show_Frm_Error()
        {
            frm_Error = Frm_Error.GetInstance(this, mJobData.mUIControl.mAppConfig);
            frm_Error.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Error);
            frm_Error.HideOnClose = true;
            tsmi_Error.Checked = true;
        }
        public void Show_Frm_Log()
        {
            frm_Log = Frm_Log.GetInstance(this, mJobData.mUIControl.mAppConfig);
            frm_Log.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Log);
            frm_Log.HideOnClose = true;
            tsmi_Log.Checked = true;
        }
        private void tsmi_2D_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                Show_Frm_2D();
            }
        }
        private void tsmi_3D_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                //Show_Frm_3D();
            }
        }
        private void tsmi_Log_Click(object sender, EventArgs e)
        {
            Show_Frm_Log();
        }
        private void tsmi_Error_Click(object sender, EventArgs e)
        {
            Show_Frm_Error();
        }

        #endregion

        private void OnConnectionStatusChanged(ConnectionStatusEventArgs args)
        {
            Invoke((Action)delegate
            {
                if (!args.IsConnected)
                {
                    toolStrip_CommStateA.Text = "通讯：Disconnect";
                    toolStrip_CommStateA.ForeColor = Color.Red;
                }
                else
                {
                    toolStrip_CommStateA.Text = "通讯：Connect";
                    toolStrip_CommStateA.ForeColor = Color.Green;
                }
            });
        }

        // 使用具名方法订阅事件
        private void SubscribeEvents()
        {
            _statusHandler = OnConnectionStatusChanged;
            EventAggregator.Subscribe(_statusHandler);
        }

        // 明确的取消订阅方法
        private void UnsubscribeEvents()
        {
            EventAggregator.Unsubscribe(_statusHandler);
            _statusHandler = null;
        }


        private void TimerDelete_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region 图片删除处理

            try
            {
                //timerDelete.Enabled = false;
                iOperCount++;
                if (iOperCount == 180)
                {
                    iOperCount = 0;
                    if (CurrentAuthority != "空")
                    {
                        Invoke((Action)delegate
                        {
                            this.toolStripLogout_ButtonClick(null, null);
                        });
                    }
                }

                TimerLogin_Elapsed();
                TimerDisk_Elapsed();
                UpdateMemoryConsume();
                //timerDelete.Enabled = true;
            }
            catch { LogUtil.LogError("图片删除异常！"); }

            #endregion
        }

        private void TimerDisk_Elapsed()
        {
            long freeSpace = new long();
            long TotalSpace = new long();

            string diskName = "";
            try
            {
                //ch:获取磁盘剩余容量
                DriveInfo[] driveInfos = DriveInfo.GetDrives();
                foreach (DriveInfo drive in driveInfos)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);//ch：获取磁盘剩余容量
                    diskName += drive.Name.Trim('\\') + freeSpace.ToString() + "GB" + " ";
                    if (drive.Name == GlobalValue.SaveFileDisks)//ch:计算剩余容量所占比
                    {
                        TotalSpace = drive.TotalSize / (1024 * 1024 * 1024);
                        double a = Convert.ToDouble(freeSpace) / Convert.ToDouble(TotalSpace);
                        if (a < GlobalValue.DisksAlarm)
                        {
                            LogUtil.LogError(drive.Name + "剩下容量剩余不足" + (GlobalValue.DisksAlarm * 100).ToString() + "%" + " 请清理该磁盘！");
                        }
                    }

                }
            }
            catch { }
        }

        private void TimerLogin_Elapsed()
        {
            #region 权限监控
            try
            {
                if (GlobalValue.CurrentUser.userType <= UserManagement.UserType.管理员)
                {
                    this.Invoke(new System.Action(() =>
                    {
                        this.系统配置toolStripMenuItem.Enabled = true;
                        this.硬件模块toolStripMenuItem.Enabled = true;
                        this.算法模块toolStripMenuItem.Enabled = true;
                        this.通讯模块toolStripMenuItem.Enabled = true;
                        this.视图ToolStripMenuItem.Enabled = true;
                    }));

                }
                if (GlobalValue.CurrentUser.userType == UserManagement.UserType.Logout)
                {
                    this.Invoke(new System.Action(() =>
                    {
                        toolStrip_Auth.Text = "空";
                        toolStrip_User.Text = "空";
                        this.系统配置toolStripMenuItem.Enabled = false;
                        this.硬件模块toolStripMenuItem.Enabled = false;
                        this.算法模块toolStripMenuItem.Enabled = false;
                        this.通讯模块toolStripMenuItem.Enabled = false;
                        this.视图ToolStripMenuItem.Enabled = false;
                    }));

                }
                else
                {
                    foreach (UserInfo item in GlobalValue.userInfos)

                    {
                        if (item.userType == GlobalValue.CurrentUser.userType)

                        {
                            this.Invoke(new System.Action(() =>
                            {
                                toolStrip_Auth.Text = GlobalValue.CurrentUser.userType.ToString();
                                toolStrip_User.Text = GlobalValue.CurrentUser.userName.ToString();
                            }));

                            break;
                        }
                    }
                }
            }
            catch { }

            #endregion
        }

        private void Jobs_JobChangedEvent(int id, string name)
        {
            JobData jobDataTemp = mJobData;
            if (mWorkFlow == null)
            {
                mJobData = mJobs.Jobs[mJobs.CurrentName];
            }
            else
            {
                mJobData = mJobs.Jobs[name];
                mWorkFlow.InitWarkFlow(mJobData);
            }

            Invoke(() => { toolStrip_JobNo.Text = $"型号：{id}"; });
            mJobData.mUIControl.ResumeLayout();
            Show_Frm();
            OnLineStateChange();
            ClearMemory();
            LogUtil.Log($"当前切换型号 {mJobs.CurrentID},名称 {mJobs.CurrentName}");
        }
        public void Load_Authority(AuthorityName authorityName, string name)
        {
            try
            {
                switch (authorityName)
                {
                    case AuthorityName.Empty:
                        CurrentAuthority = "空";
                        toolStrip_Auth.Text = "权限";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.OPN:
                        CurrentAuthority = "OPN";
                        toolStrip_Auth.Text = "OPN";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.OPNTech:
                        CurrentAuthority = "OPN技师";
                        toolStrip_Auth.Text = "OPN技师";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.ME:
                        CurrentAuthority = "ME";
                        toolStrip_Auth.Text = "ME";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        //UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.PE:
                        CurrentAuthority = "PE";
                        toolStrip_Auth.Text = "PE";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        //UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.Manager:
                        CurrentAuthority = "管理员";
                        toolStrip_Auth.Text = "管理员";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        //UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.Engineer:
                        CurrentAuthority = "工程师";
                        toolStrip_Auth.Text = "工程师";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        //UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.Operator:
                        CurrentAuthority = "操作员";
                        toolStrip_Auth.Text = "操作员";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        //UpdataAuthority(CurrentAuthority);
                        break;
                    default:
                        CurrentAuthority = "空";
                        toolStrip_Auth.Text = "权限";
                        toolStrip_User.Text = name;
                        //UserName = name;
                        //UpdataAuthority(CurrentAuthority);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败，" + ex.Message);
            }
        }

        public void UpdataAuthority(string currentauthority)
        {
            try
            {
                //MauthorityInfo = AuthorityInfo.ReadXML(mJobData.AuthorityFilePath);
                //系统配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemSetModule;
                //产品型号配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].JobConfig;
                //工位配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].StationSet;
                //参数配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemPar;
                //权限配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AuthoritySet;
                //检测项参数配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].InspectParamsSet;
                //系统在线ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemState;
                //用户管理ToolStripMenuItem.Enabled = UserAuth(currentauthority);
                //图片回放ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].PicPlayBack;
                //通讯模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommModule;
                //通讯类型ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommType;
                //通讯配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommSet;
                //硬件模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CameraModule;
                //cameraToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CameraSet;
                //算法模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmModule;
                //AlgCognex.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmVpp;
                //算法输入配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmParam;
                //MES模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesModule;
                //mes数据配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesData;
                //mes通讯配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesParam;
                //toolStrip_Manual.Visible = MauthorityInfo.Dicauth[currentauthority].MesData_Manual;
                //数据管理ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].DataModule;
                //数据库ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].DataBaseSet;
                //视图ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].ViewModule;
                //视图适应ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].ViewAdaptation;
                //tsmi_2D.Enabled = MauthorityInfo.Dicauth[currentauthority].Display2D;
                //tsmi_3D.Enabled = MauthorityInfo.Dicauth[currentauthority].Display3D;
                //Refresh();
            }
            catch (Exception)
            {
            }
        }

        private void UpdateMemoryConsume()
        {
            var (workingSet, privateWorkingSet) = MemoryInfo.GetMemoryUsage();
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    tssl_MemoryConsume.Text = $"{workingSet / 1024.0 / 1024.0:F2} MB";
                    tssl_MemoryConsume.ForeColor = Color.Red;
                });
            }
            catch (Exception)
            {
            }
        }

        [DllImport("kernel32.dll")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public void GetUserInfosValue(UserInfos userInfos1)
        {
            try
            {
                GlobalValue.userInfos = userInfos1.m_UserInfos;
                GlobalValue.CurrentUser = new UserInfo(UserManagement.UserType.Logout, "", "");

                //foreach (UserInfo item in GlobalValue.userInfos)
                //{
                //    GlobalValue.user_Name.Add(UserManagement.RSADecrypt(item.userName));
                //    GlobalValue.user_Permission.Add(item.userType.ToString());                 
                //}
            }
            catch { }

        }

        #region  外部窗体事件相应      

        /// <summary>
        /// 接收来自用户登入设置参数消息
        /// </summary>
        /// <param name="msg"></param>
        private void ReceiveLoginMsg(string msg, int mode)
        {
            if (mode == 1)
            {
                this.Invoke(new System.Action(() =>
                {
                    toolStrip_User.Text = msg;
                }));
                LogUtil.Log("当前登录权限：" + msg);

                //if(GlobalValue.CurrentUser.userType == UserManagement.UserType.管理员)
                //{
                //    this.checkBox_Test.Enabled = true;
                //    this.comboBox_Test.Enabled = true;
                //}
                //else
                //{
                //    this.checkBox_Test.Enabled = false;
                //    this.comboBox_Test.Enabled = false;
                //}
            }
            else
            {
                LogUtil.Log(msg);
            }
        }
        /// <summary>
        /// 接收来自用户注册设置参数消息
        /// </summary>
        /// <param name="msg"></param>
        /// 
        /// <summary>
        /// 接收来自标定参数操作类的消息
        /// </summary>
        /// <param name="msg"></param>


        #endregion

        private void tool_Register_Click(object sender, EventArgs e)
        {
            FrmRegister frmRegister = new FrmRegister();
            frmRegister.ShowDialog();
        }

        private void tool_Permission_Click_1(object sender, EventArgs e)
        {
            FrmPermission frmPermission1 = new FrmPermission();
            frmPermission1.ShowDialog();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("是否确认关闭软件", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    e.Cancel = false;
                    mJobData.UnRegisterEvents();
                    List<string> keys = CameraOperator.camera2DCollection.ListKeys;
                    for (int j = 0; j < keys.Count; j++)
                    {
                        if (CameraOperator.camera2DCollection[keys[j]] != null)
                        {
                            CameraOperator.camera2DCollection[keys[j]].CloseCamera();
                        }
                    }


                    timerDelete.Enabled = false;
                    timerDelete.Elapsed -= TimerDelete_Elapsed;
                    UnsubscribeEvents();
                    mWorkFlow.mClient.CloseSocket();
                    //mWorkFlow.CloseLightAll();
                    LogUtil.Log("软件关闭");
                    cts.Cancel();
                    Thread.Sleep(500);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            finally
            {
                //System.Environment.Exit(0);
            }
        }


        private void toolStripLogout_ButtonClick(object sender, EventArgs e)
        {
            CurrentAuthority = "空";
            Load_Authority(AuthorityName.Empty, CurrentAuthority);
        }


        private void 登录toolStripMenuItem_Click(object sender, EventArgs e)
        {
            iOperCount = 0;
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.PushData += new FrmLogin.ClickEventHandler(ReceiveLoginMsg);
            frmLogin.ShowDialog();
        }

        private void 产品型号配置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            iOperCount = 0;
            frmJobConfig = new Frm_JobConfig(mJobs);
            frmJobConfig.ShowDialog();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            iOperCount = 0;
            Frm_FileParam frm_Param = new Frm_FileParam(mJobData.mSystemConfigData); ;
            frm_Param.ShowDialog();
        }

        private void 系统在线toolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsOnLine = !IsOnLine;
            OnLineStateChange();
        }

        private void 硬件配置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            iOperCount = 0;
            FrmCamera2DSetting _frm2D = new FrmCamera2DSetting(mJobData.mCameraInfo, mJobData.CameraDeviceInfoPath);
            _frm2D.ShowDialog();
        }

        private void Camera2DtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            iOperCount = 0;
            Frm_CameraConfig _frmCameraConfig = new Frm_CameraConfig(mJobData);
            _frmCameraConfig.ShowDialog();
        }

        private void 算法配置toolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            iOperCount = 0;
            Frm_Tool frm_Tool = new Frm_Tool(mJobData.mTools, mJobData.mSystemConfigData.AlgMoudlePath);
            frm_Tool.ShowDialog();
        }

        private void 定位参数配置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            iOperCount = 0;
            Frm_Location frm = new Frm_Location(mJobData);
            frm.ShowDialog();
        }

        private void Window_2DChange_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                Frm_DisplaySet frm_DisplaySet = new Frm_DisplaySet(mJobData.mUIControl, "2D");
                frm_DisplaySet.ShowDialog();
            }
        }

        private void 视图适应ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mJobData.mUIControl.ResumeLayout();
            Show_Frm();
            Refresh();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if(mWorkFlow.bTakeLight)
            {
                mWorkFlow.OpenLight(1);
            }
            else
            {
                mWorkFlow.CloseLight(1);
            }
            mWorkFlow.bTakeLight = !mWorkFlow.bTakeLight;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (mWorkFlow.bPutLight)
            {
                mWorkFlow.OpenLight(2);
            }
            else
            {
                mWorkFlow.CloseLight(2);
            }
            mWorkFlow.bPutLight = !mWorkFlow.bPutLight;
        }
    }
}
