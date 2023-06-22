using System.Collections.Generic;

public partial class EquipTable: TableBase<EquipTable>
{
    private List<EquipTable> _data_EquipTable;
    private Dictionary<int, EquipTable> _dataDic = new Dictionary<int, EquipTable>();
    public List<EquipTable> data_EquipTable { get { return _data_EquipTable; } }
    public override void Init(List<EquipTable> initData)
    {
        _data_EquipTable = initData;
        _data_EquipTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public EquipTable GetData(int cfgId){  return _dataDic[cfgId];}
}

