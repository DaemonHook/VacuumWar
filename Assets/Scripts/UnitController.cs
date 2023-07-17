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

    public Vector2Int Position { get; private set; }

    // 这回合能否再次进行移动
    public bool CanMove { get; private set; }

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
        CanMove = true;
    }

    #region 

    /// <summary>
    /// 是否可以移动至瓦片
    /// </summary>
    /// <param name="tile">目标瓦片</param>
    public bool CanMoveTo(TileController tile)
    {
        if (CanMove &&
            tile.BindedUnit == null &&
            tile.logicPosition != Position &&
            Util.ManhattanDistance(tile.logicPosition, Position) < Status.movePoint)
        {
            return true;
        }
        return false;
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
