using Framework.Tool;
using Google.Protobuf;
using Net.Proto;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

internal sealed class TcpPeer : IDisposable
{
    private const int FALSE = 0;
    private const int TRUE = 1;
    private int _valueLock = 0;
    /// <summary>
    /// 消息的最大长度
    /// </summary>
    private const int MAX_LENGTH = 32767;
    internal TcpPeer()
    {
        ReadLoopIntervalMs = 10;
        _waitHandleQueue = new Queue<NetData>();
    }

    private Thread _rxThread = null;
    private List<byte> _queuedMsg = new List<byte>();
    private int _lenght = 0;
    private TcpClient _client = null;

    //internal Action<ResponseData> NetCallback = null;

    private Queue<NetData> _waitHandleQueue;

    internal bool QueueStop { get; set; }
    internal int ReadLoopIntervalMs { get; set; }
    internal TcpClient TcpClient { get { return _client; } }

    internal async Task<TcpPeer> Connect(string ip, int port)
    {
        if (string.IsNullOrEmpty(ip))
        {
            throw new ArgumentNullException("hostNameOrIpAddress");
        }
        _client = new TcpClient();
        _client.ReceiveTimeout = 30000;//30秒
        _client.SendTimeout = 10000;//10秒
        await _client.ConnectAsync(ip, port);
        StartRxThread();

        return this;
    }

    private void onConnectCallback(IAsyncResult ar)
    {
    }

    private void StartRxThread()
    {
        if (_rxThread != null) { return; }

        _rxThread = new Thread(ListenerLoop);
        _rxThread.IsBackground = true;
        _rxThread.Start();
    }

    internal TcpPeer Disconnect()
    {
        if (_client == null) { return this; }
        _client.Close();
        _client = null;
        QueueStop = false;
        return this;
    }

    private void ListenerLoop(object state)
    {
        while (!QueueStop)
        {
            try
            {
                RunLoopStep();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            System.Threading.Thread.Sleep(ReadLoopIntervalMs);
        }

        _rxThread = null;
    }

    private void RunLoopStep()
    {
        if (_client == null) { return; }
        if (_client.Connected == false) { return; }

        var c = _client;

        //if (c.Client.Poll(10, SelectMode.SelectRead))
        //{
        //    NetModule.Instance.Disconnect();
        //}

        int bytesAvailable = c.Available;
        if (bytesAvailable == 0)
        {
            return;
        }


        while (c.Available > 0 && c.Connected)
        {
            byte[] nextByte = new byte[c.Available];
            c.Client.Receive(nextByte, 0, c.Available, SocketFlags.None);
            Received(nextByte);
        }
    }

    private void Received(byte[] bytes)
    {
        _queuedMsg.AddRange(bytes);
    Begin: if (_lenght == 0 && (_queuedMsg.Count != 0 && _queuedMsg.Count < 4))
        {
            Debug.LogError("消息过短");
            _queuedMsg.Clear();
            return;
        }
        if (_lenght <= 0 && _queuedMsg.Count > 4)
        {
            List<byte> temps = _queuedMsg.GetRange(0, 4);
            _queuedMsg.RemoveRange(0, 4);

            _lenght = GameTool.byte2Int(temps.ToArray());
            if (_lenght > MAX_LENGTH)
            {
                Debug.LogError("消息长度大于:" + MAX_LENGTH);
                _lenght = 0;
                _queuedMsg.Clear();
            }
        }
        if (_lenght > 0 && _queuedMsg.Count >= _lenght)
        {
            List<byte> temps = _queuedMsg.GetRange(0, _lenght);
            _queuedMsg.RemoveRange(0, _lenght);
            _lenght = 0;
            SwapAndRun(temps.ToArray());
            goto Begin;
        }
    }

    internal void SwapAndRun(byte[] bytes = null)
    {
    Begin: if (Interlocked.CompareExchange(ref _valueLock, 1, 0) == FALSE)
        {
            if (bytes == null)
            {
                while (_waitHandleQueue.Count > 0)
                {
                    NetData netData = _waitHandleQueue.Dequeue();
                    //if (NetCallback != null)
                    {
                        try
                        {
                            NetModule.Instance.NetCallback(netData);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"{ex.Message},MsgId:{netData.MsgId}");
                            //_waitHandleQueue.Enqueue(netData);
                            break;
                        }
                    }
                }
            }
            else
            {
                Pkg pkg = Pkg.Parser.ParseFrom(bytes);
                //if (pkg.Error != ErrorCode.Ok)
                //{
                //    Debug.LogError($"协议:{pkg.Cmd},错误,错误码:{pkg.Error}");
                //}
                NetData netData = new NetData(pkg);
                _waitHandleQueue.Enqueue(netData);
            }

            Interlocked.Exchange(ref _valueLock, 0);
        }
        else
        {
            Thread.Sleep(100);
            goto Begin;
        }
    }

    internal async void Send(int msgId, byte[] data)
    {
        await new Framework.WaitForBackgroundThread();
        if (_client == null) { throw new Exception("Cannot send data to a null TcpClient (check to see if Connect was called)"); }
        if (data.Length > int.MaxValue)
        {
            throw new Exception("发送数据过长");
        }

        Pkg pkg = new Pkg();
        pkg.Cmd = msgId;
        pkg.Body = ByteString.CopyFrom(data);
        byte[] bytes = pkg.ToByteArray();
        //byte[] bytes = new byte[pkg.CalculateSize()];
        //CodedOutputStream output = new CodedOutputStream(bytes);
        //pkg.WriteTo(output);
        List<byte> temp = new List<byte>();
        temp.AddRange(GameTool.int2Byte(bytes.Length));
        //Debug.LogError(bytes.Length);
        //Debug.LogError(data.Length);
        temp.AddRange(bytes);
        try
        {
            string str = "";
            foreach (var item in temp)
            {
                str += item + " ";
            }
            //Debug.LogError(str);
            _client.GetStream().Write(temp.ToArray(), 0, temp.Count);
            //Debug.LogError("发送协议:" + msgId);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            NetModule.Instance.Disconnect();
        }
    }

    internal void FirstConnect()
    {
        //NetModule.Instance.Send(new LoginCommand());
    }

    //internal void Send(RequestData data)
    //{
    //    if (data == null) { return; }
    //    byte[] bytes = new byte[data.CalculateSize()];
    //    CodedOutputStream output = new CodedOutputStream(bytes);
    //    data.WriteTo(output);
    //    Send(bytes);
    //}
    //internal void Send(int protocolEnum, byte[] data)
    //{
    //    byte[] bytes;
    //    if (data == null)
    //    {
    //        bytes = new byte[0];
    //    }
    //    else
    //    {
    //        bytes = new byte[data.CalculateSize()];
    //        CodedOutputStream output = new CodedOutputStream(bytes);
    //        data.WriteTo(output);
    //    }
    //    RequestData requestData = new RequestData();
    //    requestData.ProtocolEnum = protocolEnum;
    //    requestData.Data = Google.Protobuf.txttring.CopyFrom(bytes);
    //    Send(requestData);
    //}
    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    public void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).

            }

            QueueStop = true;
            if (_client != null)
            {
                try
                {
                    _client.Close();
                    if (_rxThread != null)
                    {
                        _rxThread.Abort();
                        _rxThread = null;
                    }
                }
                catch { }
                _client = null;
            }

            disposedValue = true;
        }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        Dispose(true);
    }
    #endregion

}
