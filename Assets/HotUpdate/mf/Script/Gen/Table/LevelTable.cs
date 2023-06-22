using System.Collections.Generic;


    public partial class LevelTable
    {
        //表的ID,有此ID会生成Map结构,ID大小写无关
        public int id;
        //关卡名字
        public string lvName;
        //章节id
        public int chapter;
        //章节名字
        public string cpName;
        //怪物组
        public List<int> monsters;
        //奖励组
        public List<int> rewards;
    }
