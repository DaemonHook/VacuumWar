﻿/*
 * file: UnitStatus.cs
 * author: D.H.
 * feature: 单位状态
 */

using System;
public struct UnitStatus
{
    // 单位名称
    public string name;
    // hp, mp, 攻击，射程
    public int hitpoint, movePoint, damage, range;
    // 最大hp，最大mp
    public int maxHitpoint, maxMovePoint;

    public UnitStatus(string name, int maxHitPoint, int maxMovePoint, int damage, int range)
    {
        this.name = name;
        hitpoint = maxHitPoint;
        movePoint = maxMovePoint;
        this.damage = damage;
        this.range = range;
        this.maxHitpoint = maxHitPoint;
        this.maxMovePoint = maxMovePoint;
    }
}
