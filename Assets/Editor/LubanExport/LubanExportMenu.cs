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

        [MenuItem("������/һ������")]
        public static void ExecuteExport()
        {
            string configPath = Application.dataPath + "/Editor/LubanExport/" + ExcelConfigFileName;
            string json = File.ReadAllText(configPath);

            ExcelConfig config = JsonUtility.FromJson<ExcelConfig>(json);

            LubanExportConfig exportConfig = new LubanExportConfig(config.ExcelDir);
            exportConfig.Gen();
        }

        [MenuItem("������/Ԥ����������")]
        public static void PreviewCommand()
        {
            string configPath = Application.dataPath + "/Editor/LubanExport/" + ExcelConfigFileName;
            string json = File.ReadAllText(configPath);

            ExcelConfig config = JsonUtility.FromJson<ExcelConfig>(json);

            LubanExportConfig exportConfig = new LubanExportConfig(config.ExcelDir);
            Debug.Log(exportConfig.Preview());
        }

        [MenuItem("������/�������ݱ�Ŀ¼����")]
        public static void GenerateExcelDirConfig()
        {
            string configPath = Application.dataPath + "/Editor/LubanExport/" + ExcelConfigFileName;
            if(File.Exists(configPath))
            {
                EditorUtility.DisplayDialog("������", "���ݱ�Ŀ¼�����ļ��Ѵ���: " + configPath, "ȷ��");
                return;
            }


            ExcelConfig config = new ExcelConfig();
            config.ExcelDir = "�޸Ĵ˴�ΪExcel���ݱ��Ŀ¼";

            string json = JsonUtility.ToJson(config);

            File.WriteAllText(configPath, json);

            Debug.Log("�������ݱ�Ŀ¼���óɹ�");
            AssetDatabase.Refresh();
        }
    }
}
