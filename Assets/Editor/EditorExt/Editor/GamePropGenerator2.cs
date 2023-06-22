using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using System.Linq;

/// <summary>
/// 游戏属性生成
/// </summary>
public class GamePropGenerator2
{
    private class PropData
    {
        public int PropId { get; set; }
        public string PropName { get; set; }
        public string PropIcon { get; set; }
        public string PropField { get; set; }

        public List<string> Contents { get; set; }
        public PropData()
        {
            Contents = new List<string>();
        }
    }

    /// <summary>
    /// 属性生成文件
    /// </summary>
    private const string PROP_TARGET_FILE = "_Script/GameProp/PropAdapters.cs";
    private const string PROP_ENUM_FILE = "_Script/GameProp/GamePropEnum.cs";
    /// <summary>
    /// 生成游戏属性
    /// </summary>
    [MenuItem("Tools/生成测试游戏属性")]
    public static void GenProps()
    {
        TableBase<PropTable>.Initialize();
        List<PropData> list = new List<PropData>();

        PropTable.Instance.data_PropTable.ForEach(data =>
        {
            PropData prop = new PropData();
            prop.PropId = data.id;
            prop.PropName = data.name;
            prop.PropIcon = data.icon;
            prop.PropField = data.field;
            list.Add(prop);
        });

        StringBuilder sb = new StringBuilder();
        //m_genFileTitle(sb);
        m_checkFileContent(list);
        m_genCode(sb, list);
        //m_genFileTail(sb);
        File.WriteAllText(Path.Combine(Application.dataPath, PROP_TARGET_FILE), sb.ToString(), Encoding.Default);

        StringBuilder propEnumSb = new StringBuilder();
        m_genEnum(propEnumSb, list);
        File.WriteAllText(Path.Combine(Application.dataPath, PROP_ENUM_FILE), propEnumSb.ToString(), Encoding.Default);

        AssetDatabase.Refresh();

        Debug.Log("游戏属性生成成功");
    }

    private static void m_genEnum(StringBuilder sb, List<PropData> list)
    {
        sb.AppendLine($"using System;");
        sb.AppendLine($"public enum GamePropEnum");
        sb.AppendLine("{");
        foreach (PropData propData in list)
        {
            sb.AppendLine($"    {propData.PropField}={propData.PropId}, //{propData.PropName}");
        }
        sb.AppendLine("}");
    }


    private static void m_genFileTail(StringBuilder sb)
    {
        sb.AppendLine("");
        sb.AppendLine("return this");
        sb.AppendLine("");
    }

    private static void m_genCode(StringBuilder sb, List<PropData> list)
    {
        foreach (PropData propData in list)
        {
            if (propData.Contents.Count == 0)
            {
                propData.Contents.Add($"public class adapter_{propData.PropId}:BasePropAdapter");
                propData.Contents.Add("{");
                propData.Contents.Add($"    //属性Id");
                propData.Contents.Add($"    public override int GetPropId()");
                propData.Contents.Add("    {");
                propData.Contents.Add($"        return {propData.PropId};");
                propData.Contents.Add("    }");
                propData.Contents.Add("");
                propData.Contents.Add($"    //读取一个属性(计算后的值)");
                propData.Contents.Add($"    public override int OnReadValue(PropContainer propContainer, int rawValue)");
                propData.Contents.Add("    {");
                propData.Contents.Add($"        return rawValue;");
                propData.Contents.Add("    }");
                propData.Contents.Add("}");
                propData.Contents.Add($"");
            }
            sb.AppendLine($"//Begin:{propData.PropName}@{propData.PropId}");
            foreach (var str in propData.Contents)
            {
                sb.AppendLine(str);
            }
            sb.AppendLine($"//End:{propData.PropName}@{propData.PropId}");
            sb.AppendLine("");
        }
    }

    private static void m_checkFileContent(List<PropData> list)
    {
        string[] lines = File.ReadAllLines(Path.Combine(Application.dataPath, PROP_TARGET_FILE));
        bool isBegin = false;
        PropData curData = null;
        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                string str = lines[i];
                if (!isBegin)
                {
                    if (str.StartsWith("//Begin:"))
                    {
                        string[] begins = str.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                        int propId = int.Parse(begins[1]);
                        curData = list.FirstOrDefault(a => a.PropId == propId);
                        isBegin = curData != null;
                    }
                }
                else
                {
                    if (str.StartsWith("//End:"))
                    {
                        curData = null;
                        isBegin = curData != null;
                    }
                    else
                    {
                        curData.Contents.Add(str);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"属性第:{i}行格式有问题");
                throw ex;
            }

        }
    }

    private static void m_genFileTitle(StringBuilder sb)
    {
        sb.AppendLine("//所有适配器");
        sb.AppendLine("//@type table<BasePropAdapter>");
        sb.AppendLine("local this = {}");
        sb.AppendLine("//@type BasePropAdapter");
        sb.AppendLine("local tempAdapters = nil");
        sb.AppendLine("");
        sb.AppendLine("//计算属性比率系数");
        sb.AppendLine("local calcRatio = 0.0001");
        sb.AppendLine("");
    }
}
