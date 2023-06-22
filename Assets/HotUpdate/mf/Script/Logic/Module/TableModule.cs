using Framework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[Priority(1)]
internal sealed class TableModule : BaseModule<TableModule>
{
    public override void Init()
    {
        base.Init();

        Debug.Log("表数据");
        //TableBase<AchivmentTable>.Initialize();
        //TableBase<EquipTable>.Initialize();
        //TableBase<ItemTable>.Initialize();
        //TableBase<LevelTable>.Initialize();
        //TableBase<PropTable>.Initialize();
        //TableBase<RoleTable>.Initialize();
        //TableBase<SkillTable>.Initialize();
        //TableBase<StageTable>.Initialize();
    }
}
