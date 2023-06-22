using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    sealed class FrameworkWindow : EditorWindow
    {
        [MenuItem("Framework/设置", false, 1000)]
        public static void OpenWindow()
        {
            FrameworkEditorUtils.CheckConfig();
            EditorWindow.GetWindowWithRect<FrameworkWindow>(new Rect(100, 100, 550, 400), true, "设置");

        }

        private void OnGUI()
        {

            EditorGUILayout.BeginVertical("box");

            {
                EditorGUILayout.BeginHorizontal();
                FrameworkEditorUtils.FRAMEWORK_CONFIG.ViewTemplate = EditorGUILayout.ObjectField("视图模板", FrameworkEditorUtils.FRAMEWORK_CONFIG.ViewTemplate, typeof(TextAsset), false) as TextAsset;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            {
                EditorGUILayout.BeginHorizontal();
                FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelPath = EditorGUILayout.TextField("Panel路径", FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelPath);
                if (GUILayout.Button("选择路径", GUILayout.MaxWidth(80)))
                {
                    FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelPath = EditorUtility.OpenFolderPanel("选择文件夹", string.Empty, string.Empty);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            {
                EditorGUILayout.BeginHorizontal();
                FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemPath = EditorGUILayout.TextField("Item路径", FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemPath);
                if (GUILayout.Button("选择路径", GUILayout.MaxWidth(80)))
                {
                    FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemPath = EditorUtility.OpenFolderPanel("选择文件夹", string.Empty, string.Empty);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            {
                EditorGUILayout.BeginHorizontal();
                FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelGenPath = EditorGUILayout.TextField("Panel生成路径", FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelGenPath);
                if (GUILayout.Button("选择路径", GUILayout.MaxWidth(80)))
                {
                    FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelGenPath = EditorUtility.OpenFolderPanel("选择文件夹", string.Empty, string.Empty);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            {
                EditorGUILayout.BeginHorizontal();
                FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemGenPath = EditorGUILayout.TextField("Item生成路径", FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemGenPath);
                if (GUILayout.Button("选择路径", GUILayout.MaxWidth(80)))
                {
                    FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemGenPath = EditorUtility.OpenFolderPanel("选择文件夹", string.Empty, string.Empty);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            {
                EditorGUILayout.BeginHorizontal();
                FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelScriptPath = EditorGUILayout.TextField("Panel脚本路径", FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelScriptPath);
                if (GUILayout.Button("选择路径", GUILayout.MaxWidth(80)))
                {
                    FrameworkEditorUtils.FRAMEWORK_CONFIG.PanelScriptPath = EditorUtility.OpenFolderPanel("选择文件夹", string.Empty, string.Empty);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            {
                EditorGUILayout.BeginHorizontal();
                FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemScriptPath = EditorGUILayout.TextField("Item脚本路径", FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemScriptPath);
                if (GUILayout.Button("选择路径", GUILayout.MaxWidth(80)))
                {
                    FrameworkEditorUtils.FRAMEWORK_CONFIG.ItemScriptPath = EditorUtility.OpenFolderPanel("选择文件夹", string.Empty, string.Empty);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
        }

        private void OnDestroy()
        {
            if (FrameworkEditorUtils.FRAMEWORK_CONFIG != null)
            {
                EditorUtility.SetDirty(FrameworkEditorUtils.FRAMEWORK_CONFIG);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
