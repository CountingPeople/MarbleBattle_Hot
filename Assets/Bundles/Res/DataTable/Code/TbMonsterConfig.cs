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
   
public partial class TbMonsterConfig
{
    private readonly Dictionary<string, MonsterConfig> _dataMap;
    private readonly List<MonsterConfig> _dataList;
    
    public TbMonsterConfig(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, MonsterConfig>();
        _dataList = new List<MonsterConfig>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            MonsterConfig _v;
            _v = MonsterConfig.DeserializeMonsterConfig(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.ID, _v);
        }
        PostInit();
    }

    public Dictionary<string, MonsterConfig> DataMap => _dataMap;
    public List<MonsterConfig> DataList => _dataList;

    public MonsterConfig GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public MonsterConfig Get(string key) => _dataMap[key];
    public MonsterConfig this[string key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}