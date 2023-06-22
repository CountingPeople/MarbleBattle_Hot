using System.Collections.Generic;


    public partial class SkillTable
    {
        //表的ID,有此ID会生成Map结构,ID大小写无关
        public int id;
        //文字描述
        public string name;
        //图标
        public string icon;
        //技能表现
        public int displayId;
        //伤害类型
        public int propType;
        //技能值
        public float rate;
    }
