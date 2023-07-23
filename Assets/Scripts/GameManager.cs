/*
 * file: GameManager.cs
 * author: D.H.
 * feature: 游戏逻辑控制
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string mapName;

    public void Init()
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
                if (unit.CanMoveTo(target))
                unit.DoAction(ActionType.MOVE, target);
                break;
            default: break;
        }
        return true;
    }

    /// <summary>
    /// 移动单位
    /// </summary>
    /// <param name="originPos">原位置</param>
    /// <param name="newPos">新位置</param>
    /// <returns>若单位不存在则返回false</returns>
    public bool MoveUnit(Vector2Int originPos, Vector2Int newPos)
    {
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
            Debug.LogError($"MoveUnit: TileLayer no unit at {originPos}");
        }
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