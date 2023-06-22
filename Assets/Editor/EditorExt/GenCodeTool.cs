using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GenCodeTool : EditorWindow
{


    [MenuItem("Framework/GenCode ")]
    public static void GenCode()
    {
        string fullPath = Application.dataPath + "/Bundles/table/";
        StringBuilder sb = new StringBuilder();

        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }

                string str = files[i].Name.Replace(".json", "");

                sb.AppendLine($"TableBase<{str}>.Initialize();");
            }

        }

        CreateOrOPenFile(Application.dataPath, "GenFile.txt", sb.ToString());
        AssetDatabase.Refresh();
    }

    [MenuItem("Framework/GenFile ")]
    public static void GenFile()
    {
        string fullPath = Application.dataPath + "/Bundles/table/";


        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }

                string str = files[i].Name.Replace(".json", "");

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("");
                sb.AppendLine($"public partial class {str}: TableBase<{str}>");
                sb.AppendLine("{");
                sb.AppendLine($"    private List<{str}> _data_{str};");
                sb.AppendLine($"    private Dictionary<int, {str}> _dataDic = new Dictionary<int, {str}>();");
                sb.AppendLine($"    public List<{str}> data_{str} {{ get {{ return _data_{str}; }} }}");
                sb.AppendLine($"    public override void Init(List<{str}> initData)");
                sb.AppendLine("    {");
                sb.AppendLine($"        _data_{str} = initData;");
                sb.AppendLine($"        _data_{str}.ForEach(value =>{{ _dataDic.Add(value.id, value); }});");
                sb.AppendLine("    }");
                sb.AppendLine($"    public {str} GetData(int cfgId){{  return _dataDic[cfgId];}}");
                sb.AppendLine("}");
                CreateOrOPenFile($"{Application.dataPath}/HotUpdate/mf/Script/Logic/Data/Table/", $"{str}.cs", sb.ToString());
            }
        }
        AssetDatabase.Refresh();
    }


    //路径、文件名、写入内容
    static void CreateOrOPenFile(string path, string name, string info)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
}
