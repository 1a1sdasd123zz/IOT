using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vision.BaseClass;

public class StateObject
{
    public Socket workSocket = null;
    public const int BufferSize = 256;
    public byte[] buffer = new byte[BufferSize];
    public StringBuilder sb = new StringBuilder();
}

public delegate void DelConnected(bool isConnected);
public delegate void SocketMessage(string str);         //定义一个有一个string参数无返回值委托
public class TCPClient  //网口客户端类
{   
    public SocketMessage m_SocketMsgDelegate = null;       //定义一个委托变量===》有一个string参数无返回值的方法（Send()）
    public Socket m_Handler = null;                         //定义一个收发数据的客户端Socket
    public event DelConnected Connected;    //通讯是否连接
    public TcpInfo mTcpInfo;

    // 用于等待异步连接完成的信号量
    private ManualResetEvent _connectDone = new ManualResetEvent(false);
    // 标识是否正在重连，避免重复启动重连线程
    private bool _isReconnecting = false;
    // 确保多线程下_IsConnected的可见性
    private volatile bool _IsConnected = false;

    public bool IsConnected
    {
        get
        {
            return _IsConnected;
        }
    }
    public TCPClient(TcpInfo tcpInfo)
    {
        mTcpInfo = tcpInfo;
        Connect(tcpInfo);
    }

    public void Connect()
    {
        // 重置信号量（每次连接前重置）
        _connectDone.Reset();
        try
        {
            // 端口及IP  
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(mTcpInfo.IP), Convert.ToInt32(mTcpInfo.Port));
            // 创建套接字  
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 开始异步连接服务器  
            client.BeginConnect(ipe, new AsyncCallback(ConnectCallback), client);
            // 等待连接完成（最多等待5秒超时，可根据需求调整）
            _connectDone.WaitOne(5000);
        }
        catch (Exception ex)
        {
            _IsConnected = false;
            Connected?.Invoke(false);
            LogUtil.LogError($"连接初始化异常: {ex.Message}");
            _connectDone.Set(); // 释放等待
        }
    }

    // 带参数的Connect方法同样修改
    private void Connect(TcpInfo tcpInfo)
    {
        _connectDone.Reset();
        try
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(tcpInfo.IP), Convert.ToInt32(tcpInfo.Port));
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.BeginConnect(ipe, new AsyncCallback(ConnectCallback), client);
            _connectDone.WaitOne(5000);
        }
        catch (Exception ex)
        {
            _IsConnected = false;
            Connected?.Invoke(false);
            LogUtil.LogError($"连接初始化异常: {ex.Message}");
            _connectDone.Set();
        }
    }

    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Thread.Sleep(100);
            m_Handler = (Socket)ar.AsyncState;
            m_Handler.EndConnect(ar);

            if (m_Handler.Connected)
            {
                _IsConnected = true;
                Connected?.Invoke(_IsConnected);
                LogUtil.Log($"连接服务器:{mTcpInfo.IP}成功");
                // 连接成功后停止重连
                _isReconnecting = false;
                // 启动接收数据
                StartReceiving();
            }
            else
            {
                _IsConnected = false;
                LogUtil.Log($"连接服务器:{mTcpInfo.IP}失败！");
                Connected?.Invoke(false);
                // 触发重连
                StartReconnect();
            }
        }
        catch (Exception ex)
        {
            _IsConnected = false;
            Connected?.Invoke(false);
            LogUtil.LogError($"通讯连接失败: {ex.Message}");
            // 触发重连
            StartReconnect();
        }
        finally
        {
            // 无论成功失败，都释放等待
            _connectDone.Set();
        }
    }

    // 提取接收数据的逻辑为单独方法，连接成功后调用
    private void StartReceiving()
    {
        StateObject state = new StateObject();
        state.workSocket = m_Handler;
        m_Handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
    }

    private void StartReconnect()
    {
        // 避免重复启动重连线程
        lock (this)
        {
            if (_isReconnecting || _IsConnected)
                return;
            _isReconnecting = true;
        }

        // 启动后台重连线程
        Task.Factory.StartNew(() =>
        {
            while (_isReconnecting && !_IsConnected)
            {
                try
                {
                    LogUtil.Log($"尝试重连服务器:{mTcpInfo.IP}...");
                    // 调用连接方法（会自动等待连接结果）
                    Connect(mTcpInfo);
                    // 重连间隔3秒
                    Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    LogUtil.LogError($"重连异常: {ex.Message}");
                    Thread.Sleep(3000);
                }
            }
        }, TaskCreationOptions.LongRunning);
    }

    private void ReceiveCallBack(IAsyncResult iar)
    {
        try
        {
            String content = String.Empty;
            StateObject state = (StateObject)iar.AsyncState;
            Socket handler = state.workSocket;

            //读取数据   
            int bytesRead =0;
            bytesRead = handler.EndReceive(iar);
            if (bytesRead > 0)
            {
                //state.buffer = new byte[256];
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
                content = state.sb.ToString();


                m_SocketMsgDelegate?.Invoke(content);

                state.sb.Clear();
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
            }
            else
            {
                if (Connected != null)
                {
                    Connected(false);
                }
                _IsConnected = false;

                Task.Factory.StartNew(() => 
                {
                    while (!_IsConnected)
                    {
                        Connect(mTcpInfo);
                        Thread.Sleep(3000);
                    }
                });
                LogUtil.LogError($"通讯连接断开");
            }
        }
        catch (Exception ex)
        {
            if (Connected != null)
            {
                Connected(false);
            }
            _IsConnected = false;
            LogUtil.LogError($"通讯连接异常 {ex.ToString()}");
        }

    }

    public bool Send(String data)
    {
        try
        {
            byte[] byteData = new byte[4];
            byteData = Encoding.UTF8.GetBytes(data);
            m_Handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), m_Handler);
            return true;
        }
        catch { return false; }
    }

    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket handler = (Socket)ar.AsyncState;
            int bytesSent = 0;
            bytesSent =handler.EndSend(ar);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public void CloseSocket()
    {
        _isReconnecting = false; // 停止重连
        if (m_Handler != null)
        {
            m_Handler.Close();
            m_Handler = null;
        }
        _IsConnected = false;
        Connected?.Invoke(false);
    }
}