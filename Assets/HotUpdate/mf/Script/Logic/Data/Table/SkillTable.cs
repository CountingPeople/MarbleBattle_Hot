using System.Collections.Generic;

public partial class SkillTable: TableBase<SkillTable>
{
    private List<SkillTable> _data_SkillTable;
    private Dictionary<int, SkillTable> _dataDic = new Dictionary<int, SkillTable>();
    public List<SkillTable> data_SkillTable { get { return _data_SkillTable; } }
    public override void Init(List<SkillTable> initData)
    {
        _data_SkillTable = initData;
        _data_SkillTable.ForEach(value =>{ _dataDic.Add(value.id, value); });
    }
    public SkillTable GetData(int cfgId){  return _dataDic[cfgId];}
}

