using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
//using static TSFrame.Editor.Properties.Resources;

namespace Framework.Editor
{
    internal static class ViewEditor
    {
        private const string SCRIPT_ROOT = "LuaScript";
        private const string ASSETS_ROOT = "Assets";
        private const string RESOURCE_ROOT = "Resource";
        private static string _assetPath = Application.dataPath + "/";
        private static List<Type> typesList = new List<Type>();
        [MenuItem("Framework/UI/生成View #V", false, 0)]
        internal static void GenerateView()
        {
            try
            {
                _assetPath = Application.dataPath + "/";
                if (!FrameworkEditorUtils.CheckConfig())
                {
                    EditorUtility.DisplayDialog("错误", "配置表路径没有配置，请配置与Asset同级的config文件", "OK");
                    return;
                }
                CheckPath();
                List<ViewDto> viewList = new List<ViewDto>();
                LoadPanel(ref viewList);
                LoadItem(ref viewList);

                foreach (var item in viewList)
                {
                    GenComponent(item);
                    File.WriteAllText(item.ViewGenFilePath, item.ViewGenCode, new UTF8Encoding());
                    if (!File.Exists(item.ViewFilePath))
                    {
                        File.WriteAllText(item.ViewFilePath, item.ViewCode, new UTF8Encoding());
                    }
                }

                EditorUtility.DisplayDialog("Successful", "生成成功succeed", "确定");
                AssetDatabase.Refresh();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
            finally
            {
                //EditorUtility.ClearProgressBar();
            }

        }

        private static void GenComponent(ViewDto item)
        {
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(item.PrefabPath);
            List<TranDto> trans = new List<TranDto>();
            FrameworkEditorUtils.GetTrans(obj.transform, "", trans);
            CreateComponent(trans, item);
        }

        private static void CreateComponent(List<TranDto> trans, ViewDto viewDto)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder varSb = new StringBuilder();
            trans.Reverse();
            foreach (var item in trans)
            {
                string tranName = item.Tran.name;
                string[] typeNames = FrameworkEditorUtils.GetExportType(tranName);
                for (int i = 0; i < typeNames.Length; i++)
                {
                    string typeName = typeNames[i];
                    string comName = item.ComOriginalName;
                    if (typeNames.Length == 1)
                    {
                        comName = tranName;
                    }
                    else
                    {
                        comName = FrameworkEditorUtils.FRAMEWORK_CONFIG.UIExportDic.FirstOrDefault(a => a.Value == typeName).Key + comName;
                    }
                    string p_comName = comName.Substring(0, 1).ToUpper() + comName.Substring(1);
                    if (viewDto.Components.Contains(tranName))
                    {
                        throw new Exception($"界面:{viewDto.Name},存在重复命名组件:{tranName}");
                    }
                    if (typeName == typeof(Transform).Name)
                    {
                        varSb.AppendLine($"    private {typeName} {comName} = null;");
                        varSb.AppendLine($"    public {typeName} {p_comName} => this.{comName};");
                        sb.AppendLine($"        this.{comName} = this.transform.Find(\"{item.ParentPath}{tranName}\");");
                        sb.AppendLine($"#if UNITY_EDITOR");
                        sb.AppendLine($"        this.NullAssert(this.{comName}, \"{item.ParentPath}{tranName}\");");
                        sb.AppendLine($"#endif");
                    }
                    else if (typeName == typeof(GameObject).Name)
                    {
                        varSb.AppendLine($"    private {typeName} {comName} = null;");
                        varSb.AppendLine($"    public {typeName} {p_comName} => this.{comName};");
                        sb.AppendLine($"        this.{comName} = this.transform.Find(\"{item.ParentPath}{tranName}\").gameObject;");
                        sb.AppendLine($"#if UNITY_EDITOR");
                        sb.AppendLine($"        this.NullAssert(this.{comName}, \"{item.ParentPath}{tranName}\");");
                        sb.AppendLine($"#endif");
                    }
                    else
                    {
                        varSb.AppendLine($"    private {typeName} {comName} = null;");
                        varSb.AppendLine($"    public {typeName} {p_comName} => this.{comName};");
                        sb.AppendLine($"        this.{comName} = this.transform.Find(\"{item.ParentPath}{tranName}\").GetComponent<{typeName}>();");
                        sb.AppendLine($"#if UNITY_EDITOR");
                        sb.AppendLine($"        this.NullAssert(this.{comName}, \"{item.ParentPath}{tranName}\");");
                        sb.AppendLine($"#endif");
                    }
                }
                viewDto.Components.Add(tranName);
            }
            viewDto.ViewGenCode = viewDto.ViewGenCode.Replace("{BindComponent}", sb.ToString());
            viewDto.ViewGenCode = viewDto.ViewGenCode.Replace("{Variable}", varSb.ToString());
            //uiCreateorSb.AppendLine();
        }

        private static void LoadPanel(ref List<ViewDto> viewList)
        {
            string panelPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelPath;
            string panelGenScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelGenPath;
            string panelScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelScriptPath;
            string[] panels = Directory.GetFiles(panelPath, "*.prefab", SearchOption.AllDirectories);
            string[] allScripts = Directory.GetFiles(panelScriptPath, "*.cs", SearchOption.AllDirectories);
            foreach (var item in panels)
            {
                ViewDto viewDto = new ViewDto();
                viewDto.BaseName = "UIPanel";
                viewDto.Name = Path.GetFileNameWithoutExtension(item);
                viewDto.ViewName = viewDto.Name + "View";
                string viewTemp = FrameworkEditorUtils.FRAMEWORK_CONFIG.ViewTemplate.text;
                viewTemp = viewTemp.Replace("{ClassName}", viewDto.ViewName);
                viewTemp = viewTemp.Replace("{Base}", viewDto.BaseName);
                //viewTemp = viewTemp.Replace("{GeneratedTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                viewDto.ViewGenCode = viewTemp;
                viewDto.ViewGenFilePath = panelGenScriptPath + "/" + viewDto.ViewName + ".gen.cs";
                foreach (var script in allScripts)
                {
                    string scriptName = Path.GetFileName(script);
                    if (scriptName == viewDto.ViewName + ".cs")
                    {
                        viewDto.ViewFilePath = script;
                        break;
                    }
                }
                if (string.IsNullOrWhiteSpace(viewDto.ViewFilePath))
                {
                    viewDto.ViewFilePath = panelScriptPath + "/" + viewDto.ViewName + ".cs";
                }
                CreateViewCode(viewDto);

                int num = item.LastIndexOf(ASSETS_ROOT);
                string path = item.Substring(num, item.Length - num);
                viewDto.PrefabPath = path;

                path = Path.GetDirectoryName(item);
                path = path.Substring(num, path.Length - num).Replace("\\", "/");
                viewDto.ResourcePath = path + "/" + viewDto.Name+ ".prefab";
                viewDto.ViewGenCode = viewDto.ViewGenCode.Replace("{UIPath}", viewDto.ResourcePath);
                viewList.Add(viewDto);
            }
        }
        private static void LoadItem(ref List<ViewDto> viewList)
        {
            string itemPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemPath;
            string itemGenScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemGenPath;
            string itemScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemScriptPath;
            string[] items = Directory.GetFiles(itemPath, "*.prefab", SearchOption.AllDirectories);
            string[] allScripts = Directory.GetFiles(itemScriptPath, "*.cs", SearchOption.AllDirectories);
            foreach (var item in items)
            {
                ViewDto viewDto = new ViewDto();
                viewDto.BaseName = "UIItem";
                viewDto.Name = Path.GetFileNameWithoutExtension(item);
                viewDto.ViewName = viewDto.Name + "View";
                string viewTemp = FrameworkEditorUtils.FRAMEWORK_CONFIG.ViewTemplate.text;
                viewTemp = viewTemp.Replace("{ClassName}", viewDto.ViewName);
                viewTemp = viewTemp.Replace("{Base}", viewDto.BaseName);
                viewTemp = viewTemp.Replace("{GeneratedTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                viewDto.ViewGenCode = viewTemp;
                viewDto.ViewGenFilePath = itemGenScriptPath + "/" + viewDto.ViewName + ".gen.cs";
                foreach (var script in allScripts)
                {
                    string scriptName = Path.GetFileName(script);
                    if (scriptName == viewDto.ViewName + ".cs")
                    {
                        viewDto.ViewFilePath = script;
                        break;
                    }
                }
                if (string.IsNullOrWhiteSpace(viewDto.ViewFilePath))
                {
                    viewDto.ViewFilePath = itemScriptPath + "/" + viewDto.ViewName + ".cs";
                }
                CreateViewCode(viewDto);

                int num = item.LastIndexOf(ASSETS_ROOT);
                string path = item.Substring(num, item.Length - num);
                viewDto.PrefabPath = path;

                path = Path.GetDirectoryName(item);
                //num = path.LastIndexOf(RESOURCE_ROOT) + RESOURCE_ROOT.Length + 2;
                path = path.Substring(num, path.Length - num).Replace("\\", "/");
                viewDto.ResourcePath = path + "/" + viewDto.Name + ".prefab";
                viewDto.ViewGenCode = viewDto.ViewGenCode.Replace("{UIPath}", viewDto.ResourcePath);
                viewList.Add(viewDto);
            }
        }

        private static void CreateViewCode(ViewDto viewDto)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Framework;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine($"internal partial class {viewDto.ViewName}");
            sb.AppendLine("{");
            sb.AppendLine();
            sb.AppendLine("    protected override void OnCreate()");
            sb.AppendLine("    {");
            sb.AppendLine("        //code");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    protected override void OnDestroy()");
            sb.AppendLine("    {");
            sb.AppendLine("        //code");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("}");
            viewDto.ViewCode = sb.ToString();
        }

        private static void CheckPath()
        {
            string panelPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelPath;
            string panelGenScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelGenPath;
            string panelScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelScriptPath;
            string itemPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemPath;
            string itemGenScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemGenPath;
            string itemScriptPath = _assetPath + FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemScriptPath;
            if (!Directory.Exists(panelPath))
            {
                Directory.CreateDirectory(panelPath);
            }
            if (!Directory.Exists(itemPath))
            {
                Directory.CreateDirectory(itemPath);
            }
            if (!Directory.Exists(panelGenScriptPath))
            {
                Directory.CreateDirectory(panelGenScriptPath);
            }
            if (!Directory.Exists(itemGenScriptPath))
            {
                Directory.CreateDirectory(itemGenScriptPath);
            }
            if (!Directory.Exists(panelScriptPath))
            {
                Directory.CreateDirectory(panelScriptPath);
            }
            if (!Directory.Exists(itemScriptPath))
            {
                Directory.CreateDirectory(itemScriptPath);
            }
        }

        private class ViewDto
        {
            public string Name;
            public string ViewName;
            public string BaseName;
            public string PrefabPath;
            public string ResourcePath;

            public string ViewCode;
            public string ViewGenCode;
            public string ViewFilePath;
            public string ViewGenFilePath;

            //public string ModelCode;
            //public string ModelFilePath;

            public List<string> Components;

            public ViewDto()
            {
                Components = new List<string>();
            }
        }
    }
}

//internal static class PanelEditor
//{
//    /// <summary>
//    /// Resources读取路径
//    /// </summary>
//    internal static string _panelUIResPath = null;
//    /// <summary>
//    /// UI在Asset的路径
//    /// </summary>
//    internal static string _panelUIPath = null;
//    /// <summary>
//    /// 代码所在路径
//    /// </summary>
//    internal static string _panelCodePath = null;

//    internal static IniTool _iniTool = null;

//    //internal static FieldInfo _injectModelField = null;

//    [MenuItem("TSFrame/UI/生成Panel #P", false, 0)]
//    internal static void GeneratePanel()
//    {
//        try
//        {
//            if (!UIEditorUtils.CheckUIConfig())
//            {
//                EditorUtility.DisplayDialog("错误", "配置表路径没有配置，请配置与Asset同级的config文件", "OK");
//                return;
//            }
//            InitData();

//            if (!Directory.Exists(_panelCodePath))
//            {
//                Directory.CreateDirectory(_panelCodePath);
//            }
//            EditorUtility.DisplayProgressBar("生成Panel", "正在生成Panel", 0);
//            UIEditorUtils.ErrorList = new List<string>();
//            Thread.Sleep(200);
//            string[] strs = Directory.GetFiles(Path.Combine(Application.dataPath, _panelUIPath), "*.prefab");
//            float length = strs.Length;
//            float index = 0;
//            foreach (var item in strs)
//            {
//                index++;
//                //_injectModelField = null;
//                EditorUtility.DisplayProgressBar("生成Panel", $"正在生成{Path.GetFileNameWithoutExtension(item)}", index / length);
//                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine("Assets", _panelUIPath, Path.GetFileName(item)));
//                List<TranDto> trans = new List<TranDto>();
//                UIEditorUtils.GetTrans(obj.transform, "", trans);
//                trans.Reverse();
//                GenerateUserCode(obj.name);
//                GenerateCode(obj.name + ".gen.cs", PanelTemplate, trans, _panelCodePath, Path.Combine(_panelUIResPath, Path.GetFileNameWithoutExtension(item)));
//                Thread.Sleep(200);
//            }
//            EditorUtility.DisplayProgressBar("生成Panel", "生成Panel完成", 1);
//            Thread.Sleep(200);
//            EditorUtility.ClearProgressBar();
//            if (UIEditorUtils.ErrorList.Count > 0)
//            {
//                foreach (var item in UIEditorUtils.ErrorList)
//                {
//                    EditorUtility.DisplayDialog("警告", item, "OK");
//                }
//            }
//            AssetDatabase.Refresh();
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError(ex);
//        }
//        finally
//        {
//            if (_iniTool != null)
//            {
//                _iniTool.Close();
//            }
//            _iniTool = null;
//            //_injectModelField = null;
//            EditorUtility.ClearProgressBar();
//        }
//    }

//    private static void GenerateUserCode(string name)
//    {
//        string panelPath = _iniTool.ReadValue("UIScript", "PanelScriptPath", "");
//        string panelModelPath = _iniTool.ReadValue("UIScript", "PanelDataModelPath", "");
//        string modelName = "";
//        if (!string.IsNullOrWhiteSpace(panelModelPath))
//        {
//            modelName = name + "Data";
//            panelModelPath = Path.Combine(Application.dataPath, panelModelPath, modelName + ".cs");
//            if (!File.Exists(panelModelPath))
//            {
//                string temp = ModelTemplate;
//                temp = temp.Replace("{ClassName}", modelName);
//                File.WriteAllText(panelModelPath, temp, new UTF8Encoding());
//            }
//        }
//        if (!string.IsNullOrWhiteSpace(panelPath))
//        {
//            panelPath = Path.Combine(Application.dataPath, panelPath, name + ".cs");
//            if (!File.Exists(panelPath))
//            {
//                if (string.IsNullOrWhiteSpace(modelName))
//                {
//                    modelName = "NullModel";
//                }
//                string modelFieldName = modelName;
//                string tempChar = modelFieldName[0].ToString();
//                tempChar = tempChar.ToLower();
//                modelFieldName = modelFieldName.Remove(0, 1);
//                modelFieldName = modelFieldName.Insert(0, tempChar);
//                string temp = UITemplate;
//                temp = temp.Replace("{ClassName}", name);
//                temp = temp.Replace("{ModelClass}", modelName);
//                temp = temp.Replace("{ModelClassName}", modelFieldName);
//                File.WriteAllText(panelPath, temp, new UTF8Encoding());
//            }
//        }
//    }

//    private static void GenerateCode(string scriptName, string templateText, List<TranDto> trans, string panelCodePath, string uiPath)
//    {
//        uiPath = uiPath.Replace(@"\", "/");
//        string temp = templateText;
//        string varCode = UIEditorUtils.GetVarCode(trans, _iniTool);
//        string componentCode = UIEditorUtils.GetComponentCode(trans, _iniTool);
//        FieldInfo injectField = null;
//        string injectData = UIEditorUtils.GetInjectModelField(UIEditorUtils.GetTypeForPanel(Path.GetFileName(uiPath)), ref injectField);
//        string bindingData = UIEditorUtils.GetBindingData(UIEditorUtils.GetTypeForPanel(Path.GetFileName(uiPath)), injectField);

//        temp = temp.Replace("{GeneratedTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//        temp = temp.Replace("{UIPath}", uiPath);
//        temp = temp.Replace("{ClassName}", Path.GetFileName(uiPath));
//        temp = temp.Replace("{InjectData}", injectData);
//        temp = temp.Replace("{BindData}", bindingData);
//        temp = temp.Replace("{Variable}", varCode);
//        temp = temp.Replace("{BindComponent}", componentCode);

//        File.WriteAllText(Path.Combine(panelCodePath, scriptName), temp, new UTF8Encoding());
//    }

//    private static void InitData()
//    {
//        _iniTool = new IniTool();
//        _iniTool.Open(UIEditorUtils._configFilePath);
//        _panelUIPath = _iniTool.ReadValue("UI", "PanelPath", "");
//        _panelCodePath = _iniTool.ReadValue("UIScript", "PanelGeneratedScriptPath", "");
//        _panelCodePath = Path.Combine(Application.dataPath, _panelCodePath);
//        int index = _panelUIPath.IndexOf("Resources");
//        _panelUIResPath = _panelUIPath.Substring(index + UIEditorUtils.RESOURCES_LENGTH, _panelUIPath.Length - index - UIEditorUtils.RESOURCES_LENGTH);
//    }
//}
