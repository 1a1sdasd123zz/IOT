using System;
using System.Collections.Generic;

//using UW_Vision;

namespace Vision.BaseClass
{    class GlobalValue
    {
        public static List<string> Current_CamData = new List<string>();

        public static List<UserInfo> userInfos = new List<UserInfo>();
        public static UserInfo CurrentUser = new UserInfo();
        public static UserInfo RegisterUser = new UserInfo();
        public static UserInfo DeleteUser = new UserInfo();
        public static string UserInfosPath = Environment.CurrentDirectory + "\\UserInfos";
        public static long LogoutTime = 0;
        public static string  LoginUser="";
        public static List<string> user_Name = new List<string>();
        public static List<string> user_Permission = new List<string>();
        public static List<string> user_Admin = new List<string>();
        public static List<string> user_PE = new List<string>();
        public static List<string> user_ME = new List<string>();
        public static List<string> user_OPNTec = new List<string>();
        public static List<string> user_OPN = new List<string>();

        public static List<string> Recipes = new List<string>();
        
      
        public static string SaveFileDisks;
        public static string SaveImageType;
        public static string SaveImageFormat;
        public static int   SaveOKImageDate;
        public static int SaveNGImageDate;
       
        public static double DisksAlarm;
       

        /*************************通讯地址参数***********************************/
        //public static CommunicationParam mCommunParam;
        public class CommunicationParam
        {
            public static string PLC_Ip { get; set; } //ch:PLC ip地址
            public static int PLC_Port { get; set; } //ch:PLC 端口号
            public static int PLC_SA1 { get; set; } //ch:PLC 欧姆龙PLC-本机网络号                         
            public static int PLC_DA1 { get; set; } //ch:PLC 欧姆龙PLC-网络号
            public static int PLC_DA2 { get; set; } //ch:PLC 欧姆龙PLC-单元号
            public static bool PLC_IsConnect { get; set; } //ch:PLC连接状态 
            public static string HeartAddress { get; set; } //ch:PLC心跳连接地址


            //PLC->PC
            public static string TakeStation1Trigger { get; set; } //1#下相机取料触发
            public static string PutStation1Trigger { get; set; } //1#上相机放料触发
            public static string TakeStation2Trigger { get; set; } //2#下相机触发
            public static string PutStation2Trigger { get; set; } //2#上相机触发
            public static string GlueCamTrigger { get; set; } //胶路相机触发
            public static string GlueResultAddress { get; set; } //胶路结果


            public static string[] DirectionAddressStation1 = new string[6];
            public static string[] ResultAddressStation1 = new string[6] ; 
            public static string[] EsixtAddress1 = new string[6] ; 
            public static string[] TakeDataPosXStation1 = new string[6]; 
            public static string[] TakeDataPosYStation1 = new string[6]; 
            public static string[] TakeDataAngleStation1 = new string[6]; 
            public static string[] PutDataPosXStation1 = new string[6]; 
            public static string[] PutDataPosYStation1 = new string[6]; 
            public static string[] PutDataAngleStation1 = new string[6];
            public static string[] PutStation1StandardX = new string[6];
            public static string[] PutStation1StandardY = new string[6];
            public static string[] PutStation1StandardA = new string[6];

            public static string[] DirectionAddressStation2 = new string[6];
            public static string[] ResultAddressStation2 = new string[6];
            public static string[] EsixtAddress2 = new string[6];
            public static string[] TakeDataPosXStation2 = new string[6];
            public static string[] TakeDataPosYStation2 = new string[6];
            public static string[] TakeDataAngleStation2 = new string[6];
            public static string[] PutDataPosXStation2 = new string[6];
            public static string[] PutDataPosYStation2 = new string[6];
            public static string[] PutDataAngleStation2 = new string[6];
            public static string[] PutStation2StandardX = new string[6];
            public static string[] PutStation2StandardY = new string[6];
            public static string[] PutStation2StandardA = new string[6];


            public static string Server_Ip { get; set; } //ch:PLC ip地址
            public static int Server_Port { get; set; } //ch:PLC 端口号   

            public static string SerialPort { get; set; } //ch:串口端口号

            public static string Baudrate { get; set; } //ch:串口波特率
            public static string Parity { get; set; } //ch:串口校验位
            public static string DataBits { get; set; } //ch:串口数据位
            public static string StopBits { get; set; } //ch:串口停止位

        }

        public class UpLoadData
        {
            public static string Interface;
            public static string LineName;
            public static string EquipmentId;
            public static string ProductModel;
            public static string ProcessName;
            public static string JigBarCode;
            public static string TestTime;
            public static string TestResult;
        }
    }
}
