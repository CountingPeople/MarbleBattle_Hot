using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.Collections.Generic;
using UnityEditor.Animations;

public class CreateScriptUtil : Editor
{

    [MenuItem("Tools/GenActionName", false, 81)]
    public static void CreatNewCSharp()
    {
        string actionStr = GetActions();
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
        GetSelectedPathOrFallback() + "/ActionNameData.cs",
        null,
        actionStr);
    }

    private static string GetActions() {
        var obj = (GameObject)Selection.activeObject;

        var mAnim = obj.gameObject.GetComponent<Animator>();
        List<string> mStateNameList = new List<string>();
        AnimatorController ac = mAnim.runtimeAnimatorController as AnimatorController;
        ChildAnimatorState[] stList = ac.layers[0].stateMachine.states;
        for (int i = 0; i < stList.Length; ++i)
        {
            mStateNameList.Add(stList[i].state.name);
        }
        string str = string.Join("|", mStateNameList);
        return str.Trim();
    }

    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}


class MyDoCreateScriptAsset : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(o);
    }

    internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
    {
        string fullPath = Path.GetFullPath(pathName);

        string[] actions = resourceFile.Split('|');
        StringBuilder text = new StringBuilder();
        text.AppendLine("public enum ActionName");
        text.AppendLine("{");

        foreach (var item in actions) 
        {
            text.AppendLine("    " + item.Replace(" ", "").Trim()+ ",");
        }
        text.AppendLine("}");

        text.AppendLine("public static class ActionNameData");
        text.AppendLine("{");
        text.AppendLine("    public static string[] actionArr = new string[]");
        text.AppendLine("    {");
        foreach (var item in actions)
        {
            text.AppendLine("        " + '"'+ item +'"'+ ",");
        }
        text.AppendLine("    };");


        text.AppendLine("    public static string GetActionName(ActionName actionName)");
        text.AppendLine("    {");
        text.AppendLine("        return actionArr[(int)actionName];");
        text.AppendLine("    }");

        text.AppendLine("}");

        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(text);
        streamWriter.Close();
        AssetDatabase.ImportAsset(pathName);
        return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
    }
}