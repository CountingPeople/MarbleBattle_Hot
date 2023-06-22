using System.Collections;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;
using Framework;
using UnityEngine;

// Data Table manager
// use Luban for export data table
public static class DataManager
{
    static bool _IsInit = false;
    static string _DataDir = "Assets/Bundles/Res/DataTable/Data";

    static bool IsInit { get { return _IsInit; } }

    static cfg.Tables _dataTable = null;
    public static cfg.Tables DataTable {
        get { return _dataTable; }
    }

    public static void Init()
    {
        if (_IsInit)
            return;

        _dataTable = new cfg.Tables(LoadByteBuf);

        Debug.Log("== DataTable load success==");

        _IsInit = true;
    }

    private static ByteBuf LoadByteBuf(string file)
    {
        string dataFilePath = $"{_DataDir}/{file}";
        //return new ByteBuf(Resources.Load<TextAsset>(dataFilePath).bytes);
        var bytes = ResourcesModule.Instance.Load<TextAsset>($"{dataFilePath}.bytes").bytes;
        return new ByteBuf(bytes);
    }

    static void Destory()
    {
        _IsInit = false;
    }
}
