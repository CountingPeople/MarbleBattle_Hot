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
   
public partial class TbMonsterPool
{
    private readonly Dictionary<int, MonsterPool> _dataMap;
    private readonly List<MonsterPool> _dataList;
    
    public TbMonsterPool(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, MonsterPool>();
        _dataList = new List<MonsterPool>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            MonsterPool _v;
            _v = MonsterPool.DeserializeMonsterPool(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.ID, _v);
        }
        PostInit();
    }

    public Dictionary<int, MonsterPool> DataMap => _dataMap;
    public List<MonsterPool> DataList => _dataList;

    public MonsterPool GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public MonsterPool Get(int key) => _dataMap[key];
    public MonsterPool this[int key] => _dataMap[key];

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