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
public sealed partial class LevelConfig :  Bright.Config.BeanBase 
{
    public LevelConfig(ByteBuf _buf) 
    {
        LevelID = _buf.ReadInt();
        StageID = _buf.ReadInt();
        StageTime = _buf.ReadFloat();
        Brick = _buf.ReadInt();
        BrickFreshNum = _buf.ReadInt();
        BrickFreshSpace = _buf.ReadFloat();
        MonsterFreshNum = _buf.ReadInt();
        MonsterFreshSpace = _buf.ReadFloat();
        MonsterPool = _buf.ReadInt();
        PostInit();
    }

    public static LevelConfig DeserializeLevelConfig(ByteBuf _buf)
    {
        return new LevelConfig(_buf);
    }

    /// <summary>
    /// 关卡配置表，多主键
    /// </summary>
    public int LevelID { get; private set; }
    public int StageID { get; private set; }
    public float StageTime { get; private set; }
    public int Brick { get; private set; }
    public int BrickFreshNum { get; private set; }
    public float BrickFreshSpace { get; private set; }
    public int MonsterFreshNum { get; private set; }
    public float MonsterFreshSpace { get; private set; }
    public int MonsterPool { get; private set; }

    public const int __ID__ = -1308229690;
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
        + "LevelID:" + LevelID + ","
        + "StageID:" + StageID + ","
        + "StageTime:" + StageTime + ","
        + "Brick:" + Brick + ","
        + "BrickFreshNum:" + BrickFreshNum + ","
        + "BrickFreshSpace:" + BrickFreshSpace + ","
        + "MonsterFreshNum:" + MonsterFreshNum + ","
        + "MonsterFreshSpace:" + MonsterFreshSpace + ","
        + "MonsterPool:" + MonsterPool + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}