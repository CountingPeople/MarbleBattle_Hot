using System.Collections.Generic;

public partial class PropTable: TableBase<PropTable>
{
    private List<PropTable> _data_PropTable;
    private Dictionary<int, PropTable> _dataDic = new Dictionary<int, PropTable>();
    public List<PropTable> data_PropTable { get { return _data_PropTable; } }
    public override void Init(List<PropTable> initData)
    {
        _data_PropTable = initData;
        _data_PropTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public PropTable GetData(int cfgId){  return _dataDic[cfgId];}
}

