using System.Linq;

namespace Vision.BaseClass;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Helper;

public class ClientState
{
    public Socket workSocket;
    public const int BufferSize = 256;
    public byte[] buffer = new byte[BufferSize];
    public StringBuilder sb = new StringBuilder();
    public string ClientID => $"{((IPEndPoint)workSocket.RemoteEndPoint).Address}:{((IPEndPoint)workSocket.RemoteEndPoint).Port}";
}

// 定义服务器事件委托
public delegate void ClientConnectedHandler(string clientID);
public delegate void ClientDisconnectedHandler(string clientID);
public delegate void ServerMessageHandler(object sender,string clientID, string message);

public class TcpServer
{
    // 服务器事件
    public event ClientConnectedHandler ClientConnected;
    public event ClientDisconnectedHandler ClientDisconnected;
    public event ServerMessageHandler ServerMessageReceived;

    private Socket _listener;                         // 监听Socket
    private List<ClientState> _clients = new List<ClientState>();  // 客户端列表
    private bool _isRunning;                  // 服务器运行状态
    private Thread _acceptThread;                     // 接受连接线程

    public int Port { get; private set; }
    public string IP { get; private set; }

    public bool Start(string ip, int port)
    {
        try
        {
            IP = ip;
            Port = port;
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // 创建监听Socket
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(localEndPoint);
            _listener.Listen(100);

            _isRunning = true;

            // 启动接受连接线程
            _acceptThread = new Thread(AcceptConnections)
            {
                IsBackground = true
            };
            _acceptThread.Start();

            LogUtil.Log($"服务器已启动，监听地址：{ip}:{port}");
            return true;
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"服务器启动失败: {ex.Message}");
            return false;
        }
    }

    private void AcceptConnections()
    {
        while (_isRunning)
        {
            try
            {
                // 异步接受客户端连接
                _listener.BeginAccept(AcceptCallback, _listener);
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                if (_isRunning)
                    LogUtil.LogError($"接受连接时发生错误: {ex.Message}");
            }
        }
    }

    private void AcceptCallback(IAsyncResult ar)
    {
        Socket listener = (Socket)ar.AsyncState;
        try
        {
            Socket client = listener.EndAccept(ar);

            // 创建客户端状态对象
            ClientState state = new ClientState();
            state.workSocket = client;

            lock (_clients)
            {
                _clients.Add(state);
            }

            // 触发客户端连接事件
            ClientConnected?.Invoke(state.ClientID);
            OnConnected(true);
            LogUtil.Log($"客户端连接: {state.ClientID}");

            // 开始接收数据
            client.BeginReceive(state.buffer, 0, ClientState.BufferSize, 0,
                ReceiveCallback, state);
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"处理连接时发生错误: {ex.Message}");
        }
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        ClientState state = (ClientState)ar.AsyncState;
        Socket client = state.workSocket;

        try
        {
            int bytesRead = client.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));

                // 处理可能的分包情况
                string content = state.sb.ToString();
                state.sb.Clear();
                LogUtil.Log(content);
                var  messages = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries); ;
                // 触发消息接收事件
                ServerMessageReceived?.Invoke(this,state.ClientID, content);

                // 继续接收数据
                client.BeginReceive(state.buffer, 0, ClientState.BufferSize, 0,
                    ReceiveCallback, state);
            }
            else
            {
                DisConnect(state);
            }
        }
        catch (SocketException ex)
        {
            if (10054 == ex.ErrorCode)
            {
                //客户端主动断开连接--远程主机强迫关闭一个现有的连接
                DisConnect(state);
            }
        }
        catch (Exception ex)
        {
            RemoveClient(state);
            LogUtil.LogError($"接收数据时发生错误: {ex.Message}");
        }
    }

    private void DisConnect(ClientState state)
    {
        // 客户端断开连接
        LogUtil.Log($"客户端断开: {state.ClientID}");
        RemoveClient(state);
        OnConnected(false);
    }
    public void Broadcast(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        lock (_clients)
        {
            foreach (var client in _clients)
            {
                SendToClient(client.workSocket, data);
            }
        }
    }

    public bool SendToClient(Socket client, string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.BeginSend(data, 0, data.Length, 0,
                SendCallback, client);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void SendToClient(Socket client, byte[] data)
    {
        try
        {
            client.BeginSend(data, 0, data.Length, 0,
                SendCallback, client);
        }
        catch
        {
            // 处理发送失败
        }
    }

    public void SendToClient(string clientID, string message)
    {
        lock (_clients)
        {
            var clientState = _clients.FirstOrDefault(c => c.ClientID == clientID);
            if (clientState != null && clientState.workSocket.Connected)
            {
                SendToClient(clientState.workSocket, message);
                return;
            }

            return;
        }
    }

    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;
            int bytesSent = client.EndSend(ar);
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"发送数据时发生错误: {ex.Message}");
        }
    }

    private void RemoveClient(ClientState state)
    {
        lock (_clients)
        {
            if (_clients.Contains(state))
            {
                ClientDisconnected?.Invoke(state.ClientID);
                state.workSocket.Close();
                _clients.Remove(state);
            }
        }
    }

    private void OnConnected(bool isConnected)
    {
        // 将状态变更发布到事件中心
        var args = new ConnectionStatusEventArgs(isConnected, Port.ToString());
        EventAggregator.Publish(args);
    }

    public void Stop()
    {
        _isRunning = false;

        // 关闭所有客户端连接
        lock (_clients)
        {
            foreach (var client in _clients)
            {
                client.workSocket.Close();
            }
            _clients.Clear();
        }

        // 关闭监听Socket
        if (_listener != null)
        {
            _listener.Close();
            _listener = null;
        }

        LogUtil.Log("服务器已停止");
    }
}