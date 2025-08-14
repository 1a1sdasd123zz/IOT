using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro.Display;
using Vision.BaseClass;
using Vision.BaseClass.EnumHelper;
using Vision.BaseClass.Helper;
using Vision.Hardware;
using Vision.UserControlLibrary;
using static ControllerDllCSharp.ClassLibControllerDll;
using ICogImage = Cognex.VisionPro.ICogImage;
using JobData = Vision.BaseClass.VisionConfig.JobData;


namespace Vision.TaskFlow
{
#if _WIN64
    using ControllerHandle = Int32;
#else
    using ControllerHandle = Int64;
#endif

    public class WorkFlow
    {
        /// <summary>
        /// 康视达
        /// </summary>
        public int Rs232_or_Ethernet = Rs232Mode; //EthernetMode
        private ControllerHandle controllerHandleCom3;

        //光源控制器

        
        SerialPortUtil SerialPort;
        public bool bTakeLight;
        public bool bPutLight;
        //

        //private Stopwatch stp = new Stopwatch();

        //private JobInfoCollection mJobInfos = jobInfos;

        private JobData mJobData;
        private bool bRun { get; set; }

        public TCPClient mClient;


        public ConcurrentQueue<ImageInfo> ImageQueue = new();

        private int[] counts = new int[3];

        private int Row = 0;
        private int Col = 0;

        public WorkFlow(JobData jobData)
        {
            mJobData = jobData;
            InitTcp();
            //InitPort();
            //InitializeSerialPort();
        }
        public void InitWarkFlow(JobData jobData)
        {
            mJobData = jobData;
        }

        public void Start()
        {
            bRun = true;
            while (ImageQueue.TryDequeue(out var _)) {}
            InspectFlowStart();
        }
        public void Stop()
        {
            bRun = false;
        }

        #region[光源操作]

        #region[常规串口通讯]
        private void InitializeSerialPort()
        {
            try
            {
                string portName = "COM3";
                SerialPortBaudRates baud = SerialPortBaudRates.BaudRate_9600;
                Parity parity = Parity.None;
                SerialPortDatabits serialPortDatabits = SerialPortDatabits.EightBits;
                StopBits stopBits = StopBits.One;
                SerialPort = new SerialPortUtil(portName, baud, parity, serialPortDatabits, stopBits);

                if (!SerialPort.IsOpen)
                {
                    SerialPort.OpenPort();
                    if (SerialPort.IsOpen)
                    {
                        CloseLightAll();
                        LogUtil.Log($"串口{portName}打开成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Log(GlobalValue.CommunicationParam.SerialPort + "打开失败：" + ex.Message);
            }

        }

        public void OpenLight(int chanle)
        {
            
            try
            {
                SetLight(chanle, 255);
                //string data = $"#1{chanle}06412";
                //SerialPort.WriteData(data);
            }
            catch
            {
                // ignored
            }
        }

        private void SetLight(int chanle,int val)
        {
            var data = GenerateCommand(3,chanle,val.ToString("X3"));
            SerialPort.WriteData(data);
        }

        public void CloseLight(int chanle)
        {
            
            try
            {
                SetLight(chanle, 0);
                //string data = $"#2{chanle}02918";
                //SerialPort.WriteData(data);
            }
            catch
            {
                // ignored
            }
        }

        public void OpenLightAll()
        {

            try
            {
                string data = "SA0255SB0255SC0255SD0255#";
                SerialPort.WriteData(data);
            }
            catch { }
        }
        public void CloseLightAll()
        {

            try
            {
                SetLight(1, 0);
                SetLight(2, 0);
            }
            catch { }
        }

        #region[孚根光源通讯协议]

            /// <summary>
            /// 生成完整指令（含异或校验和）
            /// </summary>
            /// <param name="commandType">命令字（1-4）</param>
            /// <param name="channel">通道字（1-4）</param>
            /// <param name="data">3字节数据（十六进制字符串，如"0FF"）</param>
            public string GenerateCommand(int commandType, int channel, string data)
            {
                // 参数校验
                if (commandType < 1 || commandType > 4)
                    throw new ArgumentException("命令字必须为1-4");
                if (channel < 1 || channel > 4)
                    throw new ArgumentException("通道字必须为1-4");
                if (data.Length != 3 || !IsHex(data))
                    throw new ArgumentException("数据必须为3位十六进制字符（如0FF）");

                // 构建指令主体（不含校验和）
                char commandChar = (char)('0' + commandType);
                char channelChar = (char)('0' + channel);
                string commandBody = $"#{commandChar}{channelChar}{data}";

                // 计算异或校验和
                byte checksum = CalculateChecksum(commandBody);
                return $"{commandBody}{checksum:X2}";
            }

            /// <summary>
            /// 计算异或校验和（高低半字节分别异或）
            /// </summary>
            private  byte CalculateChecksum(string input)
            {
                byte highXor = 0;
                byte lowXor = 0;

                foreach (char c in input)
                {
                    byte ascii = (byte)c;
                    byte high = (byte)((ascii >> 4) & 0x0F); // 提取高4位
                    byte low = (byte)(ascii & 0x0F);         // 提取低4位

                    highXor ^= high;
                    lowXor ^= low;
                }

                return (byte)((highXor << 4) | lowXor); // 合并结果
            }

            /// <summary>
            /// 验证是否为3位十六进制字符串
            /// </summary>
            private bool IsHex(string input)
            {
                foreach (char c in input)
                {
                    if (!Uri.IsHexDigit(c)) return false;
                }
                return true;
            }
        #endregion

        #endregion

        #region[康视达光源]
        private void InitPort()
        {
            if (CreateSerialPort(3, ref controllerHandleCom3) == SUCCESS)
            {
                LogUtil.Log("串口打开成功");
            }
            else
            {
                LogUtil.LogError("串口打开失败!");
            }
        }

        public bool SetLightBrightnessValue(int channelIndex, int value, ControllerHandle handle)
        {
            //通过设置亮度控制光源亮灭
            if (SetDigitalValue(Rs232_or_Ethernet, channelIndex, value, handle) == SUCCESS)
                return true;
            else
                return false;
        }

        public bool SetLightBrightnessValue(MulDigitalValue[] mulDigValArray, ControllerHandle handle)
        {
            //通过设置亮度控制光源亮灭
            if (SetMulDigitalValue(Rs232_or_Ethernet, mulDigValArray, mulDigValArray.Length, handle) == SUCCESS)
                return true;
            else
                return false;
        }

        private void OpenLight()
        {
            //打开光源
            MulDigitalValue[] mulDigValArray = new MulDigitalValue[4];
            mulDigValArray[0].channelIndex = 1;
            mulDigValArray[0].DigitalValue = 255;
            mulDigValArray[1].channelIndex = 2;
            mulDigValArray[1].DigitalValue = 255;
            mulDigValArray[2].channelIndex = 3;
            mulDigValArray[2].DigitalValue = 255;
            mulDigValArray[3].channelIndex = 4;
            mulDigValArray[3].DigitalValue = 255;
            SetLightBrightnessValue(mulDigValArray, controllerHandleCom3);
            //SetLightBrightnessValue(mulDigValArray, controllerHandleCom4);
        }


        public void CloseLight()
        {
            MulDigitalValue[] mulDigValArray = new MulDigitalValue[4];
            mulDigValArray[0].channelIndex = 1;
            mulDigValArray[0].DigitalValue = 0;
            mulDigValArray[1].channelIndex = 2;
            mulDigValArray[1].DigitalValue = 0;
            mulDigValArray[2].channelIndex = 3;
            mulDigValArray[2].DigitalValue = 0;
            mulDigValArray[3].channelIndex = 4;
            mulDigValArray[3].DigitalValue = 0;

            SetLightBrightnessValue(mulDigValArray, controllerHandleCom3);
            //SetLightBrightnessValue(mulDigValArray, controllerHandleCom4);
        }

        #endregion



        #endregion

        #region PLC操作

        /// <summary>
        /// 连接PLC
        /// </summary>
        private void InitializePLC()
        {
                //mModbusTcpPlc = new ConnectHelper();
                //mModbusTcpPlc.OpenKeyence("192.168.1.88", 502, HslCommunication.Core.DataFormat.CDAB);
                //if (mModbusTcpPlc.IsKeyence)
                //{
                //    mThreadListenPLC = new Thread(ReadPlcAddress)
                //    {
                //        IsBackground = true
                //    };
                //    mThreadListenPLC.Start();

                //    LogUtil.Log("PLC连接成功");
                //}
        }
        private void ReadPlcAddress()
        {

            while (true)
            {
                try
                {
                    //var trg = mModbusTcpPlc.read_KeyenceInt(TrgAddress[0]);
                    //if (trg == 1)
                    //{
                    //    LogUtil.Log($"[A上相机]收到拍照信号");
                    //    mModbusTcpPlc.write_KeyenceInt(TrgAddress[0], 0);
                    //    CameraRun_2D(EnumStation.A上相机);
                    //}

                    Thread.Sleep(50);
                }
                catch { LogUtil.LogError("读取PLC地址异常"); }
            }

        }

        #endregion


        #region Tcp操作

        private void InitTcp()
        {
            mClient = new TCPClient(new TcpInfo("127.0.0.1", "8899"));
            if (mClient.IsConnected)
            {
                SubscribeEvents();
            }
        }
        // 使用具名方法订阅事件
        private void SubscribeEvents()
        {
            mClient.m_SocketMsgDelegate = OnReceive;
        }

        private void OnReceive(string msg)
        {
            try
            {
                LogUtil.Log($"收到数据:{msg}");
                msg = msg.TrimEnd('\n').TrimEnd('\r');
                string[] strMsg = msg.Split(',');
                if (strMsg.Length > 0)
                {
                    var flag = strMsg[0];
                    switch (flag)
                    {
                        case "T":
                        {
                            var station = strMsg[1];
                            LogUtil.Log($"收到触发信号[{station}]");

                            //switch (station)
                            //{
                            //    case "OP":
                            //    {
                            //        Row = int.Parse(strMsg[2]);
                            //        Col = int.Parse(strMsg[3]);
                            //        var type = EnumStation.上料相机;
                            //        counts[(int)type] = 0;
                            //        CameraRun_2D(type);
                            //        break;
                            //    }
                            //    default:
                            //    {
                            //        LogUtil.Log("消息解析与协议不一致");
                            //        return;
                            //    }

                            //}
                            Row = int.Parse(strMsg[2]);
                            Col = int.Parse(strMsg[3]);
                            var type = EnumStation.上料相机;
                            counts[(int)type] = 0;
                            CameraRun_2D(type);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError($"{msg}消息处理流程异常!" + ex.ToString());
            }
        }

        #endregion


        public void CameraRun_2D(EnumStation type)
        {

            Task.Run(delegate
            {
                try
                {
                    string cameraName = type.GetDescription();
                    string SN = mJobData.mCameraData[cameraName].CamSN;
                    CameraOperator.camera2DCollection[SN].UpdateImage = delegate(ImageData imageData)
                    {
                        counts[(int)type]++;
                        ImageInfo item = default(ImageInfo);
                        item.CogImage = imageData.CogImage;
                        item.Type = type;
                        item.Index = counts[(int)type];
                        ImageQueue.Enqueue(item);
                        LogUtil.Log($"单个2D相机工位{cameraName}，序列号{SN} 拍照完成次数{counts[(int)type]}");
                    };
                    CameraOperator.camera2DCollection[SN].SetCameraSetting(mJobData.mCameraData[cameraName]);
                    //LogUtil.Log("2D相机" + cameraName + "," + SN + "设置拍照参数用时");
                    if (mJobData.mCameraData[cameraName].SettingParams.TriggerMode == 0)
                    {
                        CameraOperator.camera2DCollection[SN].SoftwareTriggerOnce();
                        //LogUtil.Log("2D相机" + cameraName + "," + SN + "触发拍照指令执行时间");
                    }
                }
                catch (Exception e)
                {
                    LogUtil.LogError("执行拍照流程异常!" + e);
                }
            });
        }

        private void InspectFlowStart()
        {
            Task.Factory.StartNew(delegate
            {
                while (bRun)
                {
                    try
                    {
                        if (ImageQueue.TryDequeue(out var result))
                        {
                            LogUtil.Log($"{result.Type.GetDescription()}检测线程开始！");
                            RunTool(result);
                        }
                    }
                    catch (Exception e)
                    {
                        LogUtil.LogError("图片出队异常" + e);
                    }

                    Thread.Sleep(10);
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void ManualRun(EnumStation type,ICogImage img)
        {
            ImageInfo item = default(ImageInfo);
            item.CogImage = img;
            item.Type = type;
            ImageQueue.Enqueue(item);
        }

        private void RunTool(ImageInfo imageInfo)
        {
            try
            {
                var alg_key = imageInfo.Type.GetDescription();
                var tb = mJobData.mTools[alg_key];
                tb.Inputs["Image"].Value = imageInfo.CogImage;
                switch (imageInfo.Type)
                {
                    case EnumStation.上料相机:
                        {
                            //tb.Inputs["TrayRow"].Value = Row;
                            //tb.Inputs["TrayCol"].Value = Col;
                            tb.Run();

                            var sendData = (string)tb.Outputs["outData"].Value;
                            mClient.Send(sendData);
                            LogUtil.Log($"发送数据:{sendData}");
                            break;
                        }
                    default:
                    {
                        return;
                    }
                }
                

                var result = (bool)tb.Outputs["Result"].Value;
                var RecoredIndex = (int)tb.Outputs["RecordIndex"].Value;
                LogUtil.Log($"{alg_key}检测完成");


                var display = mJobData.mUIControl.mImageDisplays[(int)imageInfo.Type];
                var record = tb.CreateLastRunRecord().SubRecords[RecoredIndex];
                display.RecordDisplay.Record = record;
                ImageRecordInfo imageRecodInfo = default(ImageRecordInfo);
                imageRecodInfo.ImageInfo = imageInfo;
                imageRecodInfo.CogRecord = record;
                Task.Factory.StartNew(delegate
                {
                    LogUtil.Log("界面图像刷新线程开始！");
                    ShowRecord(imageRecodInfo, display, result);
                }, TaskCreationOptions.LongRunning);
            }
            catch (Exception e)
            {
                LogUtil.LogError("检测流程执行异常" + e);
            }
        }

        private void ShowRecord(ImageRecordInfo imageRecodInfo, ImageDisplay display,bool result)
        {
            try
            {
                display.Record = imageRecodInfo.CogRecord;
                imageRecodInfo.ToolImage = display.RecordDisplay.CreateContentBitmap(CogDisplayContentBitmapConstants.Image, null, 0);
                
                if(!result)
                {
                    SaveImage(imageRecodInfo);
                } 
            }
            catch (Exception ex)
            {
                LogUtil.LogError("界面显示或存图出错:" + ex.Message);
            }
        }

        private void SaveImage(ImageRecordInfo imageRecodInfo)
        {
            string stationName = imageRecodInfo.ImageInfo.Type.GetDescription();
            bool isOKorNG = imageRecodInfo.ImageSaveInfo.IsOKorNG;
            bool isSaveImageLocally = imageRecodInfo.ImageSaveInfo.IsSaveImageLocally;
            string imageName = imageRecodInfo.ImageSaveInfo.ImageName;
            ImageInfo imageInfo = imageRecodInfo.ImageInfo;
            ImageType imageType = imageRecodInfo.ImageSaveInfo.ImageType;
            string station = "NG";
            if (mJobData.mSystemConfigData.SaveRaw)
            {
                RawImageInfo imageInfo_Raw = null;
                imageInfo_Raw = new RawImageInfo();
                imageInfo_Raw.Path = mJobData.mSystemConfigData.PicPath + "\\Raw";
                imageInfo_Raw.Station = stationName + imageRecodInfo.ImageSaveInfo.Code;
                imageInfo_Raw.Info = station;
                imageInfo_Raw.ImageName = imageName;
                imageInfo_Raw.mImageType = mJobData.mSystemConfigData.ImageType;
                imageInfo_Raw.ThumbPercent = mJobData.mSystemConfigData.ThumbPercent;
                imageInfo_Raw.Image = imageInfo.CogImage;
                VisionPro_ImageSave.SaveRawImageAsync(imageInfo_Raw);
                LogUtil.Log("[工位]" + stationName + "，" + "图像入队完成(原图)！");
            }

            if (mJobData.mSystemConfigData.SaveDeal)
            {
                ToolImageInfo imageInfo_Tool = null;
                imageInfo_Tool = new ToolImageInfo();
                imageInfo_Tool.Path = mJobData.mSystemConfigData.PicPathRes + "\\Deal";
                imageInfo_Tool.Station = stationName + imageRecodInfo.ImageSaveInfo.Code;
                imageInfo_Tool.Info = station;
                imageInfo_Tool.ImageName = imageName;
                imageInfo_Tool.mImageType = mJobData.mSystemConfigData.ImageTypeTool;
                imageInfo_Tool.ThumbPercent = mJobData.mSystemConfigData.ThumbPercentRes;
                imageInfo_Tool.Image = imageRecodInfo.ToolImage;
                VisionPro_ImageSave.SaveToolImageAsync(imageInfo_Tool);
                LogUtil.Log("[工位]" + stationName + "，" + "图像入队完成（处理图）！");
            }
        }
    }
}
