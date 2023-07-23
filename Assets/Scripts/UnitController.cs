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
        get
        {
            return display.IsIdle();
        }
    }

    // 这回合能否再次进行移动
    public bool NotMoved { get; private set; }

    // 本回合所能影响到的最远范围（目前版本为移动点数+射程）
    public int InfluceRange
    {
        get
        {
            return Status.maxMovePoint + Status.range;
        }
    }

    public void Init(Vector2Int position)
    {
        display.SetPosition(position);
        display.RefreshStatus(Status);
        NotMoved = true;
        logicPosition = position;
    }

    #region 

    /// <summary>
    /// 是否可以移动至瓦片
    /// </summary>
    /// <param name="tile">目标瓦片</param>
    public bool CanMoveTo(TileController tile)
    {
        if (NotMoved &&
            tile.BindedUnit == null &&
            tile.logicPosition != logicPosition &&
            Util.ManhattanDistance(tile.logicPosition, logicPosition) <= Status.movePoint)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region 单位动作

    public void MoveSelf(Vector2Int newPos)
    {
        logicPosition = newPos;
        display.MoveTo(newPos);
    }
    
    /// <summary>
    /// 进行动作（不做合法性检查）
    /// </summary>
    /// <param name="type">动作类型</param>
    /// <param name="param">参数</param>
    public void DoAction(ActionType type, object param)
    {
        switch (type)
        {
            case ActionType.MOVE:
                MoveSelf((Vector2Int)param);
                break;
            default: break;
        }
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
