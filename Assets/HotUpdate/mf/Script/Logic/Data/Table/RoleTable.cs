using System.Collections.Generic;

public partial class RoleTable: TableBase<RoleTable>
{
    private List<RoleTable> _data_RoleTable;
    private Dictionary<int, RoleTable> _dataDic = new Dictionary<int, RoleTable>();
    public List<RoleTable> data_RoleTable { get { return _data_RoleTable; } }
    public override void Init(List<RoleTable> initData)
    {
        _data_RoleTable = initData;
        _data_RoleTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public RoleTable GetData(int cfgId){  return _dataDic[cfgId];}
}

