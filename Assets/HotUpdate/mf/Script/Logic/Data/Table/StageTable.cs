using System.Collections.Generic;

public partial class StageTable: TableBase<StageTable>
{
    private List<StageTable> _data_StageTable;
    private Dictionary<int, StageTable> _dataDic = new Dictionary<int, StageTable>();
    public List<StageTable> data_StageTable { get { return _data_StageTable; } }
    public override void Init(List<StageTable> initData)
    {
        _data_StageTable = initData;
        _data_StageTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public StageTable GetData(int cfgId){  return _dataDic[cfgId];}
}

