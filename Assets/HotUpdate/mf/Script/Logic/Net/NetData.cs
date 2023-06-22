using Net.Proto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public readonly struct NetData
{
    public readonly int MsgId;
    //public readonly int ErrorCode;
    public readonly byte[] MsgBody;

    public NetData(int msgId,byte[] msgBody)
    {
        MsgId = msgId;
        MsgBody = msgBody;
        //ErrorCode = errorCode;
    }
    public NetData(Pkg pkg)
    {
        MsgId = pkg.Cmd;
        MsgBody = pkg.Body.ToArray();
        //ErrorCode = (int)pkg.Error;
    }
}
