/*
 * file: UnitController.cs
 * author: D.H.
 * feature: 移动相机
 */

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private IUnitDisplay display;

    #region 外部设置

    public string unitName;
    public int damage, maxHitpoint, maxMovePoint;
    public int range;

    #endregion

    public int team;

    public UnitStatus Status { get; private set; }

    public Vector2Int logicPosition;

    public bool Idle
    {
        get { return display.IsIdle(); }
    }

    // 这回合能否再次进行移动
    public bool CanMove { get; private set; }

    // 本回合所能影响到的最远范围（目前版本为移动点数+射程）
    public int InfluceRange
    {
        get { return Status.maxMovePoint + Status.range; }
    }

    public void Init(Vector2Int position)
    {
        display.SetPosition(position);
        display.RefreshStatus(Status);
        CanMove = true;
        logicPosition = position;
    }

    #region 外部状态接口

    /// <summary>
    /// 是否可以移动至瓦片（这里只考虑本身因素，如MP）
    /// </summary>
    /// <param name="target">目标位置</param>
    public bool CanMoveTo(Vector2Int target)
    {
        if (CanMove &&
            Util.ManhattanDistance(target, logicPosition) <= Status.movePoint
           )
        {
            return true;
        }

        return false;
    }

    #endregion

    #region 单位动作

    public void MoveSelf(Vector2Int newPos)
    {
        Debug.Log($"unit {unitName} moving to {newPos}");
        logicPosition = newPos;
        display.MoveTo(newPos);
        CanMove = false;
        display.TriggerAvailability(false);
    }

    #endregion

    private void Awake()
    {
        Status = new UnitStatus(unitName, maxHitpoint, maxMovePoint, damage, range);
        display = GetComponent<IUnitDisplay>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}