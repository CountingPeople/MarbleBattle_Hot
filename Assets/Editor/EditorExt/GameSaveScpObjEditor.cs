//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;

//#if UNITY_EDITOR

//[CustomEditor(typeof(GameSaveScpObj))]
//public class GameSaveScpObjEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        // 显示自定义属性
//        //GameSaveScpObj myObject = (GameSaveScpObj)target;
//        //myObject.EquipBagDb = EditorGUILayout.PropertyField<List<EquipDtoJson>>("Custom Property", myObject.EquipBagDb);

//        //serializedObject.Update();

//        //EditorGUILayout.PropertyField(serializedObject.FindProperty("EquipBagDb"), true);

//        //serializedObject.ApplyModifiedProperties();

//        //listName = myObject.cacheStrDic.Keys.ToArray()[1].Name;
//        //for (var i = 0; i < myObject.cacheStrDic.Count; i++)
//        //{
//        //    GUILayout.BeginHorizontal();

//        //    myObject.EquipRoleDb[i] = EditorGUILayout.TextField(myObject.EquipRoleDb[i]);

//        //    if (GUILayout.Button("-"))
//        //    {
//        //        myObject.EquipRoleDb.RemoveAt(i);
//        //        break;
//        //    }

//        //    GUILayout.EndHorizontal();
//        //}


//        if (GUILayout.Button("清空数据")) {
//            PlayerPrefs.SetInt("GameSaveScpObj", 0);
//        }
//    }
//}
//#endif