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
   
public partial class TbGlobalConfig
{

     private readonly BattleConfig _data;

    public TbGlobalConfig(ByteBuf _buf)
    {
        int n = _buf.ReadSize();
        if (n != 1) throw new SerializationException("table mode=one, but size != 1");
        _data = BattleConfig.DeserializeBattleConfig(_buf);
        PostInit();
    }


    /// <summary>
    /// 玩家最大血量（开局）
    /// </summary>
     public int PlayerMaxHp => _data.PlayerMaxHp;

    public void Resolve(Dictionary<string, object> _tables)
    {
        _data.Resolve(_tables);
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        _data.TranslateText(translator);
    }

    
    partial void PostInit();
    partial void PostResolve();
}

}