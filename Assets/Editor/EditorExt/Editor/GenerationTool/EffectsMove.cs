using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WorkTools
{
    public class EffectsMove : EditorWindow
    {

        static bool m_pass = true;
        //递交位置
        static string m_GeneratePaht = "Assets/AssetBundle/prefab/effect/skill";

        [MenuItem("Assets/--- 美术工具 ---/特效工具/递交到AB文件夹", false, 2000)]
        public static void Generate()
        {
            m_pass = true;
            UtilEditor.ClearConsole();
            //------ 容错处理 ------
            var selecte = UtilEditor.GetSelectedAssets(new List<string>() { ".prefab" });
            if (selecte == null || selecte.Count < 1)
                Debug.LogWarning("Tools：没有对应的递交文件");

            //------ 检查预设名称 ------
            foreach (var path in selecte)
            {
                var prefab_name = Path.GetFileName(path);
                if (prefab_name.StartsWith("fx_atk_") || prefab_name.StartsWith("fx_skill_") || prefab_name.StartsWith("fx_buff_"))
                    continue;
                else
                {
                    Debug.Log("<color=red>" + Path.GetFileName(path) + "</color> 预设不符合命名规则", AssetDatabase.LoadAssetAtPath<Object>(path));
                    m_pass = false;
                }
            }


            if (m_pass == false)
                return;


            //------ 检查依赖文件 ------
            foreach (var path in selecte)
            {
                var dependencies = AssetDatabase.GetDependencies(path);
                foreach (var dependent_path in dependencies)
                {
                    //------ 排除文件 ------
                    if (dependent_path.EndsWith(".cs") || dependent_path.EndsWith(".shader") || dependent_path.EndsWith("Lit.mat") || dependent_path.EndsWith("ParticlesUnlit.mat"))
                        continue;
                    else
                    {
                        //预设父路径
                        var preafb_parent_path = Directory.GetParent(path).ToString().Replace("\\", "/");

                        //依赖父路径
                        var dependent_parent_path = "";
                        var path_sp = dependent_path.Split('/');
                        for (int i = 0; i < path_sp.Length; i++)
                        {
                            var item = path_sp[i];
                            dependent_parent_path += item + "/";
                            if (item == "effect")
                            {
                                dependent_parent_path += path_sp[i + 1];
                                break;
                            }
                        }
                        if (dependent_parent_path.StartsWith("Assets/Art/effect/command") || dependent_parent_path.StartsWith(preafb_parent_path))
                        {
                            continue;
                        }
                        else
                        {
                            Debug.Log("<color=red>" + Path.GetFileName(path) + "</color>" + " 依赖文件不在规则内：" + "<color=yellow>" + Path.GetFileName(dependent_path) + "</color>", AssetDatabase.LoadAssetAtPath<Object>(dependent_path));
                            m_pass = false;
                        }
                    }

                }
            }

            //------ 移动文件 ------
            if (m_pass)
            {


                foreach (var item in selecte)
                {
                    var preafb = Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>(item));

                    //设置层级
                    var allNode = preafb.GetComponentsInChildren<Transform>();
                    foreach (var node in allNode)
                        node.gameObject.layer = LayerMask.NameToLayer("TransparentFX");


                    //创建预设
                    var prefab_path = m_GeneratePaht + "/" + Path.GetFileName(item);
                    PrefabUtility.SaveAsPrefabAsset(preafb, prefab_path);
                    Debug.Log("<color=#00ff00>递交成功！</color>");

                    GameObject.DestroyImmediate(preafb);
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            else
            {
                m_pass = true;
                Debug.Log("<color=red>递交失败</color>");
            }
        }
    }
}
