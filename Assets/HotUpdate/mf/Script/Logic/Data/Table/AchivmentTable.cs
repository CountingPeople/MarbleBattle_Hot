using System.Collections.Generic;

public partial class AchivmentTable: TableBase<AchivmentTable>
{
    private List<AchivmentTable> _data_AchivmentTable;
    private Dictionary<int, AchivmentTable> _dataDic = new Dictionary<int, AchivmentTable>();
    public List<AchivmentTable> data_AchivmentTable { get { return _data_AchivmentTable; } }
    public override void Init(List<AchivmentTable> initData)
    {
        _data_AchivmentTable = initData;
        _data_AchivmentTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public AchivmentTable GetData(int cfgId){  return _dataDic[cfgId];}
}

