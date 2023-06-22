
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WorkTools
{
    public class GenerateShip : EditorWindow
    {
        //通用动作机位置
        static string m_Controller = "Assets/Art/model/ship/common/common@animator.controller";

        //生成位置
        static string m_GeneratePaht = "Assets/AssetBundle/prefab/ship";

        static bool pass = false;

        [MenuItem("Assets/--- 美术工具 ---/模型工具/生成船只预设", false, 2000)]
        public static void Generate()
        {
            //容错判断
            var selecte = UtilEditor.GetSelectedAssets(new List<string>() { ".FBX" });
            if (selecte == null || selecte.Count < 1)
            {
                Debug.LogWarning("Tools：没有找到对象！");
                return;
            }
            if (!Path.GetFileName(selecte[0]).StartsWith("ship"))
            {
                Debug.LogWarning("Tools：请选择对应的船只 FBX模型");
            }

            var parent_path = Directory.GetParent(selecte[0]).ToString();
            List<string> all_flie = new List<string>();
            UtilEditor.SearchAllFiles(parent_path, all_flie);

            var file_fbx_name = Path.GetFileNameWithoutExtension(selecte[0]);
            var file_mat_path = "";
            var file_tex_path = "";
            var file_controller_path = "";

            foreach (var file in all_flie)
            {
                if (Path.GetFileName(file) == "mat_" + file_fbx_name + ".mat")
                    file_mat_path = file;

                if (Path.GetFileNameWithoutExtension(file) == "tex_" + file_fbx_name)
                    file_tex_path = file;

                if (Path.GetFileName(file).EndsWith(".controller"))
                    file_controller_path = file;
            }

            //创建层级
            var rootName = Path.GetFileNameWithoutExtension(selecte[0]);
            var root = new GameObject(rootName);
            var go = Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>(selecte[0]));
            go.name = "model";//设置go对象名称。
            go.transform.SetParent(root.transform);//设置go变换的父级。

            //创建功能节点
            var tran_root = new GameObject("tran_root");    //功能节点
            var head_ui = new GameObject("head_ui");        //UI节点
            var gun = new GameObject("gun");                //开火节点
            var trailing = new GameObject("trailing");      //拖尾节点
            var effect01 = new GameObject("effect01");      //默认添加的特效自身节点

            //设置层级
            tran_root.transform.SetParent(root.transform);
            tran_root.transform.position = Vector3.zero;//节点坐标

            effect01.transform.SetParent(go.transform);
            effect01.transform.position = Vector3.zero;

            head_ui.transform.SetParent(tran_root.transform);
            head_ui.transform.position = new Vector3(0, 3.25f, 0);

            gun.transform.SetParent(tran_root.transform);
            gun.transform.position = new Vector3(0, 1.25f, 1.25f);

            trailing.transform.SetParent(tran_root.transform);
            trailing.transform.position = Vector3.zero;

            //添加功能节点
            //var dummyLuaEntity = root.AddComponent<Framework.Lua.DummyLuaEntity>();
            //var node = new List<Transform>();
            //node.Add(effect01.transform);
            //node.Add(head_ui.transform);
            //node.Add(gun.transform);
            //node.Add(trailing.transform);
            //dummyLuaEntity.Dummys = node;

            //创建材质
            var mat = AssetDatabase.LoadAssetAtPath<Material>(file_mat_path);
            var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(file_tex_path);
            mat.shader = Shader.Find("temp/WorkURP_NEW/Modle/Modle_Ship");
            mat.SetTexture("_BaseMap", tex);
            mat.EnableKeyword("ENABLED_LIGHTING");


            //设置动作机
            var animator = go.AddComponent<Animator>();
            RuntimeAnimatorController controller = null;

            if (file_controller_path != "")
                controller = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(file_controller_path);
            else
                controller = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(m_Controller);
            animator.runtimeAnimatorController = controller;

            //设置层级
            var allNode = root.GetComponentsInChildren<Transform>();
            foreach (var item in allNode)
            {
                if (item.gameObject.name == "sail")
                    item.gameObject.layer = LayerMask.NameToLayer("TransparentFX");
                else
                    item.gameObject.layer = LayerMask.NameToLayer("Ship");
            }


            //创建预设
            var prefab_path = m_GeneratePaht + "/model_" + rootName + ".prefab";

            if (File.Exists(prefab_path))
            {
                var selectiveType = EditorUtility.DisplayDialogComplex("Tools：", "预设已存在，是否替换预设？", "替换", "取消", "");
                if (selectiveType == 0)
                {
                    //创建预设
                    PrefabUtility.SaveAsPrefabAsset(root, prefab_path);
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                //创建预设
                PrefabUtility.SaveAsPrefabAsset(root, prefab_path);
                AssetDatabase.Refresh();
            }
            GameObject.DestroyImmediate(root);
        }
    }
}