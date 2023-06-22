using System.Collections.Generic;

public partial class LevelTable: TableBase<LevelTable>
{
    private List<LevelTable> _data_LevelTable;
    private Dictionary<int, LevelTable> _dataDic = new Dictionary<int, LevelTable>();
    public List<LevelTable> data_LevelTable { get { return _data_LevelTable; } }
    public override void Init(List<LevelTable> initData)
    {
        _data_LevelTable = initData;
        _data_LevelTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public LevelTable GetData(int cfgId){  return _dataDic[cfgId];}
}

