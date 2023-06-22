using System.Collections.Generic;

public partial class ItemTable: TableBase<ItemTable>
{
    private List<ItemTable> _data_ItemTable;
    private Dictionary<int, ItemTable> _dataDic = new Dictionary<int, ItemTable>();
    public List<ItemTable> data_ItemTable { get { return _data_ItemTable; } }
    public override void Init(List<ItemTable> initData)
    {
        _data_ItemTable = initData;
        _data_ItemTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public ItemTable GetData(int cfgId){  return _dataDic[cfgId];}
}

