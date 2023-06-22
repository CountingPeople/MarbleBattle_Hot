using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class CacheLogic : GetInstance<CacheLogic>
{
    //GameDataDto2 dataDto;

    //public void InitData()
    //{
    //    //dataDto = ExtTool.Instance.LoadPlayerJson();
    //}

    //public void LoadData()
    //{
    //    if (dataDto == null)
    //    {
    //        dataDto = new GameDataDto2();
    //    }
    //    dataDto.LoadData();
    //}

    //public void SaveData()
    //{
    //    dataDto.SavaData();
    //}
    //private GameDataDto2 dataDto;
    private GameSaveScpObjTool _connection;

    public void InitData()
    {
        //dataDto = ExtTool.Instance.LoadPlayerJson();
        //InitConnect("GameData.db");
        _connection = new GameSaveScpObjTool();
        _connection.InitData();

        //GameDtoService gameDto = new GameDtoService();
        //EquipDtoService equipDto = new EquipDtoService();
        //RoleEquipDtoService roleEquipDto = new RoleEquipDtoService();
        //AchiveDayService achive1 = new AchiveDayService();
        //AchiveDataService achive2 = new AchiveDataService();
        //AchiveStateService achive3 = new AchiveStateService();

        //GameDtoService.Instance.Init(_connection);
        //EquipDtoService.Instance.Init(_connection);
        //RoleEquipDtoService.Instance.Init(_connection);
        //AchiveDayService.Instance.Init(_connection);
        //AchiveDataService.Instance.Init(_connection);
        //AchiveStateService.Instance.Init(_connection);
    }

    public void LoadData()
    {
        //if (dataDto == null)
        //{
        //    dataDto = new GameDataDto2();
        //}
        //dataDto.LoadData();
    }

    public void SaveData()
    {
        //dataDto.SavaData();
        //_connection.Close();
    }

    private void InitConnect(string DatabaseName)
    {

        //#if UNITY_EDITOR
        //        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
        //#else
        //        // check if file exists in Application.persistentDataPath
        //        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        //        if (!File.Exists(filepath))
        //        {
        //            Debug.Log("Database not in Persistent path");
        //            // if it doesn't ->
        //            // open StreamingAssets directory and load the db ->

        //#if UNITY_ANDROID
        //            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
        //            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
        //            // then save to Application.persistentDataPath
        //            File.WriteAllBytes(filepath, loadDb.bytes);
        //#elif UNITY_IOS
        //                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
        //                // then save to Application.persistentDataPath
        //                File.Copy(loadDb, filepath);
        //#elif UNITY_WP8
        //                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
        //                // then save to Application.persistentDataPath
        //                File.Copy(loadDb, filepath);

        //#elif UNITY_WINRT
        //		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
        //		// then save to Application.persistentDataPath
        //		File.Copy(loadDb, filepath);

        //#elif UNITY_STANDALONE_OSX
        //		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
        //		// then save to Application.persistentDataPath
        //		File.Copy(loadDb, filepath);
        //#else
        //	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
        //	// then save to Application.persistentDataPath
        //	File.Copy(loadDb, filepath);

        //#endif

        //            Debug.Log("Database written");
        //        }

        //        var dbPath = filepath;
        //#endif
        //Debug.Log("Final PATH: " + Application.persistentDataPath + "/" + DatabaseName);
        ////_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        //_connection = new SQLiteConnection(Application.persistentDataPath + "/" + DatabaseName, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

    }


    public void SaveRank(int count)
    {
        //PlayerLogic.Instance.GetPlayer().rank = count;
        //GameDtoService.Instance.Update();
    }

    public void SaveMission(int count)
    {
        //PlayerLogic.Instance.GetPlayer().mission = count;
        //GameDtoService.Instance.Update();
    }

    public void SaveVolume(float count)
    {
        //PlayerLogic.Instance.GetPlayer().volume = count;
        //GameDtoService.Instance.Update();
    }

    public void SaveIsPlayerSound(bool isPlay)
    {
        //PlayerLogic.Instance.GetPlayer().isPlaySound = isPlay;
        //GameDtoService.Instance.Update();
    }

}
