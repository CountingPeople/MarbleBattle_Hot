//using Framework;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameDataDto 
//{
//    public int instId;
//    public string head;
//    public string name;
//    public int level;
//    public float exp;
//    public int keyCount;
//    public int rank;
//    public int mission;
//    public float volume;
//    public bool isPlaySound;
//    public string genTime;
//    public List<string> dateList = new List<string>();
//    public List<EquipDto> bagEquipDtos=new List<EquipDto>();
//    public List<EquipDto> playerEquipDtos=new List<EquipDto>();
//    public Dictionary<AchiveCondType, int> achiveData = new Dictionary<AchiveCondType, int>();
//    public Dictionary<int, AchiveGetState> achiveState = new Dictionary<int, AchiveGetState>();

//    //public Int64 leaveTime;

//    public static implicit operator GameDataDto(GameDataDtoJson jsonRoleDto)
//    {
//        if (jsonRoleDto == null)
//        {
//            return null;
//        }
//        GameDataDto roleDto = new GameDataDto();
//        roleDto.instId = jsonRoleDto.instId;
//        roleDto.keyCount = jsonRoleDto.totalKey;
//        roleDto.level = jsonRoleDto.level;
//        roleDto.name = jsonRoleDto.name;
//        roleDto.head = jsonRoleDto.head;
//        roleDto.volume = jsonRoleDto.volume;
//        roleDto.isPlaySound = jsonRoleDto.isPlaySound;
//        roleDto.genTime = jsonRoleDto.genTime;
//        roleDto.exp = jsonRoleDto.exp;
//        roleDto.mission = jsonRoleDto.mission;
//        roleDto.achiveData = jsonRoleDto.achiveData;
//        roleDto.achiveState = jsonRoleDto.achiveState;
//        foreach (var item in jsonRoleDto.bagEquipDtos)
//        {
//            roleDto.bagEquipDtos.Add(item);
//        }
//        foreach (var item in jsonRoleDto.playerEquipDtos)
//        {
//            roleDto.playerEquipDtos.Add(item);
//        }

//        //GameApp.Instance.Log($"qs  {jsonRoleDto.isPlaySound}  {roleDto.isPlaySound}");

//        return roleDto;
//    }

//    public GameDataDto()
//    {
//        var _genTime = ExtTool.Instance.GetTimeSpanNow();
//        genTime = _genTime.ToString();
//        instId = ExtTool.Instance.GetInstanceId();

//        head = UIUtil.Instance.GetRandHead();
//        name = UIUtil.Instance.GetRandName();
//        level = 1;
//        exp = 0;
//        keyCount = 0;
//        rank = 2000;
//        volume = 0.5f;
//        isPlaySound = true;
//        mission = 1;

//        var all = EquipLogic.Instance.GetRandSuit(1, 1, 1);
//        foreach (var item in all)
//        {
//            playerEquipDtos.Add(item);
//        }

//    }

//    public void LoadData()
//    {
//        //离线时间
//        //Int64 passTime = ExtTool.Instance.GetTimeSpanPass(leaveTime);
//        //DateTime dt = ExtTool.Instance.GetTimeDate(leaveTime);
//        //string leaveStr = dt.ToString("yyyy/MM/dd HH:mm:ss");

//        //出生时间
//        DateTime dt = ExtTool.Instance.GetTimeDate(Convert.ToInt64(genTime));
//        string genStr = dt.ToString("yyyy/MM/dd HH:mm:ss");

//        //登录天数
//        var currentTime = System.DateTime.Now;
//        var tempYear = currentTime.Year;
//        var tempMonth = currentTime.Month;
//        var tempDay = currentTime.Day;
//        string tempDateString = string.Format("{0}_{1}_{2}", tempYear, tempMonth, tempDay);
//        if (!dateList.Contains(tempDateString))
//        {
//            dateList.Add(tempDateString);
//        }
//        AchivmentLogic.Instance.SetAchiveData(AchiveCondType.Cond_TotalPlayDay, dateList.Count);



//        //背包
//        foreach (var item in bagEquipDtos)
//        {
//            item.skillIcon= SkillLogic.Instance.GetSkillIcon(item.skillId);
//            BagLogic.Instance.AddEquip(item);
//        }

//        //玩家数据
//        //PlayerLogic.Instance.LoadData(this);

//        //音效加载
//        AudioModule.Instance.SetSound(volume, isPlaySound);

//        //成就
//        if (achiveData.Count > 0)
//        {
//            //AchivmentLogic.Instance.SetData(this);
//        }
//        AchivmentLogic.Instance.SetAchiveData(AchiveCondType.Cond_UpLevel, level);

//        //Debug.Log($"qs {mission}");

//    }

//    public void SavaData()
//    {


//        //背包
//        bagEquipDtos.Clear();
//        foreach (var item in BagLogic.Instance.GetEquipBind().ToList())
//        {
//            bagEquipDtos.Add(item);
//        }
//        //玩家数据
//        PlayerDto tempPlayer = PlayerLogic.Instance.GetPlayer();
//        head = tempPlayer.iconPath;
//        name = tempPlayer.name;
//        level = tempPlayer.level.Value;
//        exp = tempPlayer.exp;
//        keyCount = tempPlayer.keyCount.Value;
//        rank = tempPlayer.rank.Value;
//        //玩家装备
//        playerEquipDtos.Clear();
//        foreach (var item in PlayerLogic.Instance.GetRole().GetEquipListBind().ToList())
//        {
//            playerEquipDtos.Add(item);
//        }

//        //声音设置
//        var temp = AudioModule.Instance.GetSountCfg();
//        volume = temp.Item1;
//        isPlaySound = temp.Item2;

//        //成就
//        achiveData = AchivmentLogic.Instance.GetData();
//        achiveState = AchivmentLogic.Instance.GetState();
//        //登录天数


//        //Debug.Log($"qs {mission}");


//        ExtTool.Instance.SavePlayerJson(this);
//        Debug.Log("保存成功");
//    }


//}

//public class GameDataDtoJson
//{
//    public int instId;
//    public string head;
//    public string name;
//    public int totalKey;
//    public string genTime;
//    //public Int64 leaveTime;
//    public int level;
//    public float exp;
//    public int mission;
//    public float volume;
//    public bool isPlaySound;
//    public List<EquipDtoJson> bagEquipDtos=new List<EquipDtoJson>();
//    public List<EquipDtoJson> playerEquipDtos = new List<EquipDtoJson>();
//    public Dictionary<AchiveCondType, int> achiveData = new Dictionary<AchiveCondType, int>();
//    public Dictionary<int, AchiveGetState> achiveState = new Dictionary<int, AchiveGetState>();

//    public static implicit operator GameDataDtoJson(GameDataDto roleDto)
//    {
//        if (roleDto == null)
//        {
//            return null;
//        }
//        GameDataDtoJson json = new GameDataDtoJson();
//        json.instId = roleDto.instId;
//        json.totalKey = roleDto.keyCount;
//        json.level = roleDto.level;
//        json.exp = roleDto.exp;
//        json.name = roleDto.name;
//        json.head = roleDto.head;
//        json.mission = roleDto.mission;
//        json.volume = roleDto.volume;
//        json.isPlaySound = roleDto.isPlaySound;
//        json.genTime = roleDto.genTime;
//        json.achiveData = roleDto.achiveData;
//        json.achiveState = roleDto.achiveState;

//        foreach (var item in roleDto.bagEquipDtos) {
//            json.bagEquipDtos.Add(item);
//        }
//        foreach (var item in roleDto.playerEquipDtos)
//        {
//            json.playerEquipDtos.Add(item);
//        }

//        //GameApp.Instance.Log($"qs  {json.isPlaySound}  {roleDto.isPlaySound}");

//        return json;
//    }
//}


