using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 场景事件
/// 跟随场景销毁注册的事件会被销毁
/// </summary>
public enum SceneEvent
{
    SCENE_LOADED = 1,        //场景加载完毕
    SCENE_ENTER,            //进入场景
    GAME_START,         //玩家可操作
    GAME_END,           //游戏结束玩家不可操作
    GAME_PAUSE,         //游戏暂停
    GAME_CONTINUE,      //游戏继续
    SCENE_EXIT,         //卸载场景


    //联机----------------------------------------
    NEW_PLAYER_ENTER = 1000, //新玩家进入
    PLAYER_EXIT,            //玩家离开


}