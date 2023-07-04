/* 
 * file: Enums.cs
 * author: D.H.
 * feature: 全局枚举
 */

using System;
// 移动状态
public enum MoveStatus
{
    CANCELED,       // 不显示
    ABLE,           // 可移动
    NOT_ABLE,       // 不可移动
    ATTACKABLE,     // 可攻击
    BUILDABLE,      // 可建造
}


public enum UnitType
{
    BUILDING,       // 建筑
    TROOP,          // 部队
}
