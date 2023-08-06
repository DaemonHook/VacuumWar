/*
 * file: GameManager.cs
 * author: D.H.
 * feature: 游戏逻辑控制
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string mapName;

    private void Init()
    {
        DataManager.instance.InitMap(mapName);
        InterfaceManager.instance.Init();
    }

    #region 全局服务

    public bool DoAction(UnitController unit, ActionType type, object param)
    {
        switch (type)
        {
            case ActionType.MOVE:
                Vector2Int target = (Vector2Int)param;
                Debug.Log($"unit at {unit.logicPosition} try move to {target}");
                return TryMoveUnit(unit, target);
            default: break;
        }
        return true;
    }
    
    /// <summary>
    /// 移动单位
    /// </summary>
    /// <param name="unit">单位</param>
    /// <param name="newPos">新位置</param>
    /// <returns>若单位不存在则返回false</returns>
    private bool TryMoveUnit(UnitController unit, Vector2Int newPos)
    {
        var originPos = unit.logicPosition;
        if (!UnitLayer.instance.unitDict.ContainsKey(originPos))
        {
            return false;
        }
        if (TileLayer.instance.RebindUnit(originPos, newPos))
        {
            UnitLayer.instance.MoveUnit(originPos, newPos);
        }
        else
        {
            Debug.LogError($"MoveUnit: Rebind failed!");
            return false;
        }
        return true;
    }

    #endregion

    #region 全局状态

    /// <summary>
    /// 当前玩家（队伍）
    /// </summary>
    public int curTeam { get; private set; }



    /// <summary>
    /// 单位是否可以移动至目标瓦片
    /// </summary>
    /// <param name="unit">单位</param>
    /// <param name="targetTile">目标瓦片</param>
    public bool CanMoveTo(UnitController unit, TileController targetTile)
    {
        Vector2Int target = targetTile.logicPosition;
        if (!unit.CanMoveTo(target))
        {
            return false;
        }

        if (UnitLayer.instance.unitDict.ContainsKey(target))
        {
            return false;
        }
        
        //TODO: 增加地形逻辑
        return true;
    }

    #endregion

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}