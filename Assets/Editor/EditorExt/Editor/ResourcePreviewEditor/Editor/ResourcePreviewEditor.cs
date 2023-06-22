using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WorkTools
{
    [CustomEditor(typeof(ResObject))]
    public class ResourcePreviewEditor : Editor
    {
        ResObject _Root;
        static string _Path;
        static int _ViewSize = 50;
        static string _keyword;
        static Vector2 _ViewLocation;


        public void OnEnable()
        {
            _Root = (ResObject)target;
            _Path = _Root.path;
            _Root.ClearNull();
            CollectPrefab();
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            GUILayout.Box("预览面板", new GUIStyle("FrameBox"));

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUIUtility.labelWidth = 55;
                _keyword = EditorGUILayout.TextField("搜索", _keyword, new GUIStyle("SearchTextField"));
                _ViewSize = EditorGUILayout.IntSlider("预览尺寸", _ViewSize, 50, 100, GUILayout.Width(200));
            }
            EditorGUILayout.EndHorizontal();

            _ViewLocation = EditorGUILayout.BeginScrollView(_ViewLocation, new GUIStyle("GroupBox"), GUILayout.MinHeight(UnityEngine.Screen.height - 260));
            {
                foreach (var asset in _Root.Assets)
                {
                    var initial_letter = SpellHelper.GetSpellCode(asset.tips);
                    if (_keyword != "" && !string.IsNullOrEmpty(_keyword))
                    {
                        if (asset.tips.ToLower().Contains(_keyword.ToLower()))
                            ElementGUI(asset);

                        var array = initial_letter.ToCharArray();
                        var count = _keyword.Length;
                        if (initial_letter.Length >= count)
                        {
                            var temp_a = initial_letter.Substring(0, count).ToLower();
                            var temp_b = _keyword.ToLower();

                            if (temp_b.Contains(temp_a))
                                ElementGUI(asset);
                        }
                    }
                    else
                    { ElementGUI(asset); }
                }
            }
            EditorGUILayout.EndScrollView();

            GUILayout.Box("设置面板", new GUIStyle("FrameBox"));
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUIUtility.labelWidth = 80;
                _Path = EditorGUILayout.TextField("预览路径", _Path);
                EditorGUILayout.Space(30);

                if (GUILayout.Button("刷新", GUILayout.Width(100)))
                    CollectPrefab(true);
            }
            EditorGUILayout.EndHorizontal();
        }

        //元素GUI
        static void ElementGUI(ResData asset)
        {
            EditorGUILayout.BeginHorizontal(new GUIStyle("box"));
            {
                EditorGUIUtility.labelWidth = 120;
                Texture texture = AssetPreview.GetAssetPreview(asset.obj);
                if (GUILayout.Button(texture, GUILayout.Width(_ViewSize), GUILayout.Height(_ViewSize)))
                {
                    EditorGUIUtility.PingObject(asset.obj);
                    Selection.activeGameObject = asset.obj as GameObject;
                }
                EditorGUILayout.BeginVertical();
                {
                    GUILayout.Space(10);

                    var gui_color = GUI.color;
                    if (asset.tips == "remarks")
                        GUI.color = Color.grey;

                    EditorGUI.BeginChangeCheck();
                    asset.tips = EditorGUILayout.TextField(asset.tips, new GUIStyle("ExposablePopupItem"));
                    if (EditorGUI.EndChangeCheck()) { EditorUtility.SetDirty(asset); }

                    GUI.color = gui_color;

                    GUILayout.Label(asset.obj.name, new GUIStyle("MeTimeLabel"), GUILayout.Height(_ViewSize * 0.5f));
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
        private void OnDisable()
        {
            AssetDatabase.SaveAssets();
        }

        //收集预设
        void CollectPrefab(bool update = false)
        {
            if (Directory.Exists(_Path) == false)
                return;
            else
                _Root.path = _Path;

            var filelist = new List<string>();
            UtilEditor.SearchAllFiles(_Root.path, filelist, new List<string> { ".prefab" });
            if (_Root.path != _Root.lastPath)
                _Root.Assets.Clear();

            if (update)
            {
                for (int i = 0; i < _Root.Assets.Count; i++)
                    EditorUtility.SetDirty(_Root.Assets[i].obj);
            }

            foreach (var file in filelist)
            {
                var res_data = ScriptableObject.CreateInstance<ResData>();
                res_data.obj = AssetDatabase.LoadAssetAtPath<GameObject>(file);
                res_data.path = file;
                _Root.AddData(res_data);
            }
            _Root.lastPath = _Root.path;
        }
    }
}