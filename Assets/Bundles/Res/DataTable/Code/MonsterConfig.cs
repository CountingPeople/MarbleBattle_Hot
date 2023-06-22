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
public sealed partial class MonsterConfig :  Bright.Config.BeanBase 
{
    public MonsterConfig(ByteBuf _buf) 
    {
        ID = _buf.ReadString();
        HP = _buf.ReadFloat();
        AttackCD = _buf.ReadFloat();
        BulletNum = _buf.ReadInt();
        BulletID = _buf.ReadFloat();
        Speed = _buf.ReadFloat();
        Res = _buf.ReadString();
        PostInit();
    }

    public static MonsterConfig DeserializeMonsterConfig(ByteBuf _buf)
    {
        return new MonsterConfig(_buf);
    }

    /// <summary>
    /// 怪物配置表
    /// </summary>
    public string ID { get; private set; }
    public float HP { get; private set; }
    public float AttackCD { get; private set; }
    public int BulletNum { get; private set; }
    public float BulletID { get; private set; }
    public float Speed { get; private set; }
    public string Res { get; private set; }

    public const int __ID__ = -55174244;
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
        + "HP:" + HP + ","
        + "AttackCD:" + AttackCD + ","
        + "BulletNum:" + BulletNum + ","
        + "BulletID:" + BulletID + ","
        + "Speed:" + Speed + ","
        + "Res:" + Res + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}