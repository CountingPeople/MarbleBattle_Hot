using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace WorkTools
{
    public class DependenceAnalysisEditor : EditorWindow
    {
        private static DependenceData _finderData = new DependenceData();   //查找器数据
        private static bool _initFinderData = false;    //初始化查找器数据
        private bool _isUpdateAsset = false;            //刷新资源
        private bool _isDepend = false;                 //引用&依赖模式
        private bool initializedGUIStyle = false;       //初始化样式
                                                        //工具栏按钮样式
        private GUIStyle toolbarButtonGUIStyle;
        //工具栏样式
        private GUIStyle toolbarGUIStyle;
        //选中资源列表
        private List<string> selectedAssetGuid = new List<string>();


        [SerializeField]
        private TreeViewState _treeViewState;
        private DependenceTreeView _finderTreeView;



        //查找资源引用信息
        [MenuItem("Assets/--- 美术工具 ---/依赖分析", false, 2000)]
        static void FindRef()
        {
            AssetDatabase.SaveAssets();
            InitDataIfNeeded();
            OpenWindow();
            DependenceAnalysisEditor window = GetWindow<DependenceAnalysisEditor>();
            window.UpdateSelectedAssets();
        }

        //打开窗口
        [MenuItem("Tools/美术工具/依赖分析")]
        static void OpenWindow()
        {
            DependenceAnalysisEditor window = GetWindow<DependenceAnalysisEditor>();
            window.wantsMouseMove = false;
            window.titleContent = new GUIContent("资源查找工具");
            window.Show();
            window.Focus();
        }

        //初始化数据
        static void InitDataIfNeeded()
        {
            if (!_initFinderData)
            {
                //初始化数据
                if (!_finderData.ReadFromCache())
                {
                    _finderData.CollectDependenciesInfo();
                }
                _initFinderData = true;
            }
        }

        //初始化GUIStyle
        void InitGUIStyleIfNeeded()
        {
            if (!initializedGUIStyle)
            {
                toolbarButtonGUIStyle = new GUIStyle("ToolbarButton");
                toolbarGUIStyle = new GUIStyle("Toolbar");
                initializedGUIStyle = true;
            }
        }

        //更新选中资源列表
        private void UpdateSelectedAssets()
        {
            selectedAssetGuid.Clear();
            foreach (var obj in Selection.objects)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                //如果是文件夹
                if (Directory.Exists(path))
                {
                    string[] folder = new string[] { path };
                    //将文件夹下所有资源作为选择资源
                    string[] guids = AssetDatabase.FindAssets(null, folder);
                    foreach (var guid in guids)
                    {
                        if (!selectedAssetGuid.Contains(guid) && !Directory.Exists(AssetDatabase.GUIDToAssetPath(guid)))
                            selectedAssetGuid.Add(guid);
                    }
                }
                //如果是文件资源
                else
                {
                    string guid = AssetDatabase.AssetPathToGUID(path);
                    selectedAssetGuid.Add(guid);
                }
            }
            _isUpdateAsset = true;
        }

        //通过选中资源列表更新TreeView
        private void UpdateAssetTree()
        {
            if (_isUpdateAsset && selectedAssetGuid.Count != 0)
            {
                var root = SelectedAssetGuidToRootItem(selectedAssetGuid);
                if (_finderTreeView == null)
                {
                    //初始化TreeView
                    if (_treeViewState == null)
                        _treeViewState = new TreeViewState();
                    var headerState = DependenceTreeView.CreateDefaultMultiColumnHeaderState(position.width);
                    var multiColumnHeader = new MultiColumnHeader(headerState);
                    _finderTreeView = new DependenceTreeView(_treeViewState, multiColumnHeader);
                }
                _finderTreeView.assetRoot = root;
                _finderTreeView.CollapseAll();
                _finderTreeView.Reload();
                _isUpdateAsset = false;
            }
        }

        private void OnGUI()
        {
            InitGUIStyleIfNeeded();
            DrawOptionBar();
            UpdateAssetTree();
            if (_finderTreeView != null)
            {
                //绘制Treeview
                _finderTreeView.OnGUI(new Rect(0, toolbarGUIStyle.fixedHeight, position.width, position.height - toolbarGUIStyle.fixedHeight));
            }
        }

        //绘制上条
        public void DrawOptionBar()
        {
            EditorGUILayout.BeginHorizontal(toolbarGUIStyle);
            {
                //刷新数据
                if (GUILayout.Button("跟新数据", toolbarButtonGUIStyle))
                {
                    _finderData.CollectDependenciesInfo();
                    _isUpdateAsset = true;
                    EditorGUIUtility.ExitGUI();
                }
                //修改模式
                bool tempIsDepend = _isDepend;
                _isDepend = GUILayout.Toggle(_isDepend, _isDepend ? "模式(依赖)" : "模式(引用)", toolbarButtonGUIStyle, GUILayout.Width(100));
                if (tempIsDepend != _isDepend)
                    _isUpdateAsset = true;


                //预览模式
                if (GUILayout.Button("展开", toolbarButtonGUIStyle))
                {
                    if (_finderTreeView != null)
                        _finderTreeView.ExpandAll();
                }
                if (GUILayout.Button("折叠", toolbarButtonGUIStyle))
                {
                    if (_finderTreeView != null)
                        _finderTreeView.CollapseAll();
                }

                GUILayout.FlexibleSpace();

                //清理缓存
                if (GUILayout.Button("重置", toolbarButtonGUIStyle))
                {
                    _finderData.Remove();
                    _finderData.CollectDependenciesInfo();
                    _isUpdateAsset = true;
                    EditorGUIUtility.ExitGUI();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        //生成root相关
        private HashSet<string> updatedAssetSet = new HashSet<string>();
        //通过选择资源列表生成TreeView的根节点
        private AssetViewItem SelectedAssetGuidToRootItem(List<string> selectedAssetGuid)
        {
            updatedAssetSet.Clear();
            int elementCount = 0;
            var root = new AssetViewItem { id = elementCount, depth = -1, displayName = "Root", data = null };
            int depth = 0;
            foreach (var childGuid in selectedAssetGuid)
                root.AddChild(CreateTree(childGuid, ref elementCount, depth));
            updatedAssetSet.Clear();
            return root;
        }
        //通过每个节点的数据生成子节点
        private AssetViewItem CreateTree(string guid, ref int elementCount, int _depth)
        {
            if (!updatedAssetSet.Contains(guid))
            {
                _finderData.UpdateAssetState(guid);
                updatedAssetSet.Add(guid);
            }
            ++elementCount;
            var referenceData = _finderData.assetDict[guid];
            var root = new AssetViewItem { id = elementCount, displayName = referenceData.name, data = referenceData, depth = _depth };
            var childGuids = _isDepend ? referenceData.dependencies : referenceData.references;
            foreach (var childGuid in childGuids)
                root.AddChild(CreateTree(childGuid, ref elementCount, _depth + 1));
            return root;
        }
    }
}