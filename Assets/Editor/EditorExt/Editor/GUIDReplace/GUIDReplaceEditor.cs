using UnityEditor;
using UnityEngine;

namespace WorkTools
{
    public class GUIDReplaceEditor : EditorWindow
    {
        private static GUIDReplaceEditor m_GUED;
        private Object m_old;
        private Object m_replace;
        private int m_state = 0;

        [MenuItem("Tools/美术工具/GUED替换", false)]
        private static void ShowWindow()
        {
            if (m_GUED == null)
                m_GUED = GetWindow<GUIDReplaceEditor>(false, "GUIDReplace");
            else
                m_GUED.Close();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Label("GUED替换");


                EditorGUI.BeginChangeCheck();  //开始检测面板变化·初始化获取设置
                GUILayout.BeginHorizontal();
                {
                    EditorGUIUtility.labelWidth = 60;
                    m_old = (Object)EditorGUILayout.ObjectField("旧文件:", m_old, typeof(Object), false);
                }
                GUILayout.EndHorizontal();

                if (EditorGUI.EndChangeCheck())  //检测结束 如果源目标跟换则需要重新检索
                    m_state = 0;

                GUILayout.BeginHorizontal();
                {
                    EditorGUIUtility.labelWidth = 60;
                    m_replace = (Object)EditorGUILayout.ObjectField("新文件:", m_replace, typeof(Object), false);
                }
                GUILayout.EndHorizontal();



                GUILayout.BeginHorizontal();
                {
                    if (m_state == 1) { GUI.color = Color.green; }
                    else { GUI.color = Color.white; }
                    if (GUILayout.Button("查找", GUILayout.Height(20)))
                    {
                        m_state = 1;
                        UtilEditor.ClearConsole();
                        GUIDReplace.Find(m_old);
                    }
                    GUI.color = Color.white;


                    if (GUILayout.Button("替换", GUILayout.Height(20)))
                    {
                        if (m_state == 1)
                        {
                            m_state = 0;
                            GUIDReplace.Replace(m_old, m_replace);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
    }
}