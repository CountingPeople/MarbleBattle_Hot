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
/// <summary>
/// 怪物配置
/// </summary>
public sealed partial class MonsterRefreshConfig :  Bright.Config.BeanBase 
{
    public MonsterRefreshConfig(ByteBuf _buf) 
    {
        ID = _buf.ReadString();
        Weight = _buf.ReadInt();
        PostInit();
    }

    public static MonsterRefreshConfig DeserializeMonsterRefreshConfig(ByteBuf _buf)
    {
        return new MonsterRefreshConfig(_buf);
    }

    /// <summary>
    /// 怪物ID
    /// </summary>
    public string ID { get; private set; }
    /// <summary>
    /// 概率
    /// </summary>
    public int Weight { get; private set; }

    public const int __ID__ = 1949196387;
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
        + "ID:" + ID + ","
        + "Weight:" + Weight + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}