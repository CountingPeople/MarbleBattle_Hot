using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace MarbleBattleEditor
{
    public class LubanExportMenu : MonoBehaviour
    {

        const string ExcelConfigFileName = "ExcelConfig.json";

        [MenuItem("导表工具/一键导出")]
        public static void ExecuteExport()
        {
            string configPath = Application.dataPath + "/Editor/LubanExport/" + ExcelConfigFileName;
            string json = File.ReadAllText(configPath);

            ExcelConfig config = JsonUtility.FromJson<ExcelConfig>(json);

            LubanExportConfig exportConfig = new LubanExportConfig(config.ExcelDir);
            exportConfig.Gen();
        }

        [MenuItem("导表工具/预览导表命令")]
        public static void PreviewCommand()
        {
            string configPath = Application.dataPath + "/Editor/LubanExport/" + ExcelConfigFileName;
            string json = File.ReadAllText(configPath);

            ExcelConfig config = JsonUtility.FromJson<ExcelConfig>(json);

            LubanExportConfig exportConfig = new LubanExportConfig(config.ExcelDir);
            Debug.Log(exportConfig.Preview());
        }

        [MenuItem("导表工具/生成数据表目录配置")]
        public static void GenerateExcelDirConfig()
        {
            string configPath = Application.dataPath + "/Editor/LubanExport/" + ExcelConfigFileName;
            if(File.Exists(configPath))
            {
                EditorUtility.DisplayDialog("导表工具", "数据表目录配置文件已存在: " + configPath, "确定");
                return;
            }


            ExcelConfig config = new ExcelConfig();
            config.ExcelDir = "修改此处为Excel数据表根目录";

            string json = JsonUtility.ToJson(config);

            File.WriteAllText(configPath, json);

            Debug.Log("生成数据表目录配置成功");
            AssetDatabase.Refresh();
        }
    }
}
