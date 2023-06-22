using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 场景枚举
/// </summary>
public enum SceneMode
{
    NONE = 0,
    /// <summary>
    /// 登录场景
    /// </summary>
    LOGIN,
    /// <summary>
    /// 主场景
    /// </summary>
    Battle,
    /// <summary>
    /// 游戏场景pve
    /// </summary>
    PVE,
    /// <summary>
    /// 游戏场景pvp
    /// </summary>
    PVP,
    MAX = 9999999,
}