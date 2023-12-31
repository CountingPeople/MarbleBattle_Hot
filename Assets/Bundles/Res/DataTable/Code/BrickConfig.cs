//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace cfg
{
public sealed partial class BrickConfig :  Bright.Config.BeanBase 
{
    public BrickConfig(ByteBuf _buf) 
    {
        BrickId = _buf.ReadString();
        BrickRewardType = (MarbleReward)_buf.ReadInt();
        BrickRewardId = _buf.ReadString();
        BackColor = _buf.ReadUnityVector3();
        Icon = _buf.ReadString();
        PostInit();
    }

    public static BrickConfig DeserializeBrickConfig(ByteBuf _buf)
    {
        return new BrickConfig(_buf);
    }

    /// <summary>
    /// 标靶配置表，1:1对应标靶奖励枚举
    /// </summary>
    public string BrickId { get; private set; }
    public MarbleReward BrickRewardType { get; private set; }
    public string BrickRewardId { get; private set; }
    public UnityEngine.Vector3 BackColor { get; private set; }
    public string Icon { get; private set; }

    public const int __ID__ = -1129346365;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "BrickId:" + BrickId + ","
        + "BrickRewardType:" + BrickRewardType + ","
        + "BrickRewardId:" + BrickRewardId + ","
        + "BackColor:" + BackColor + ","
        + "Icon:" + Icon + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}