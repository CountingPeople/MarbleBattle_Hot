using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Tool;
using Framework;
using Net.Proto;
using Google.Protobuf;
using System.IO;

public class NetMsgData {
    public int msgId;
    public byte[] body;

    public NetMsgData(int _msgId, byte[] _body) {
        msgId = _msgId;
        body = _body;
    }

}

public class NetModule : BaseModule<NetModule>
{

    private Action onConnectCallback = () =>
    {
        Debug.Log("连接成功");
        MessageModule.Instance.OnEvent(GameEventEnum.OnConnect);

    };
    private Action onDisconnectCallback = () =>
    {

        Debug.Log("断开连接");
        MessageModule.Instance.OnEvent(GameEventEnum.OnDisconnect);

    };

    private Action<int, byte[]> netCallback = (MsgId, MsgBody) =>
    {
        //Debug.Log("收到消息   "+MsgId);
        NetMsgData msgData = new NetMsgData(MsgId, MsgBody);
        MessageModule.Instance.OnEvent(GameEventEnum.OnBackData, msgData);
    };


    /// <summary>
    /// Tcp连接
    /// </summary>
    private TcpPeer _tcpClient;

    private const int PING_TIME = 10;
    private float _swapTime = 0.1f;
    private float _tempTime = 0.1f;
    /// <summary>
    /// 网络连接
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public async void Connect(string ip, int port)
    {
        if (_tcpClient != null)
        {
            _tcpClient.Disconnect();
        }
        _tcpClient = new TcpPeer();
        await _tcpClient.Connect(ip, port);
        onConnectCallback();

    }

    /// <summary>
    /// 断开连接
    /// </summary>
    public void Disconnect()
    {
        if (_tcpClient != null)
        {
            _tcpClient.Disconnect();
        }
        onDisconnectCallback();
    }
    public override void Update(float deltaTime)
    {
        if (_tcpClient != null)
        {
            _tempTime += deltaTime;
            if (_tempTime > _swapTime)
            {
                _tcpClient.SwapAndRun();
                _tempTime = 0;
            }
        }
    }

    /// <summary>
    /// 序列化protobuf
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static byte[] Serialize(IMessage msg)
    {
        //using (MemoryStream rawOutput = new MemoryStream())
        //{
        //    CodedOutputStream output = new CodedOutputStream(rawOutput);
        //    //output.WriteRawVarint32((uint)len);
        //    output.WriteMessage(msg);
        //    output.Flush();
        //    byte[] result = rawOutput.ToArray();

        //    return result;
        //}
        return msg.ToByteArray();
    }

    /// <summary>
    /// 反序列化protobuf
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataBytes"></param>
    /// <returns></returns>
    public static T Deserialize<T>(byte[] dataBytes) where T : IMessage, new()
    {
        CodedInputStream stream = new CodedInputStream(dataBytes);
        T msg = new T();
        msg.MergeFrom(stream);
        //stream.ReadMessage(msg);
        ////msg= (T)msg.Descriptor.Parser.ParseFrom(dataBytes);
        //return msg;

        return msg;
    }


    internal void Send(int msgId, IMessage data)
    {
        byte[] body = Serialize(data);
        if (_tcpClient != null)
        {
            _tcpClient.Send(msgId, body);
        }
    }


    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="body"></param>
    internal void Send<T>(int msgId, byte[] body)
    {
        if (_tcpClient != null)
        {
            _tcpClient.Send(msgId, body);
        }
    }

    internal void NetCallback(NetData netData)
    {
        if (netCallback != null)
        {
            netCallback(netData.MsgId, netData.MsgBody);
        }
    }
    public override void Freed()
    {
        if (_tcpClient != null)
        {
            _tcpClient.Disconnect();
        }
    }


}

