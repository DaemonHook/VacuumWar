/*
 * file: UnitController.cs
 * author: D.H.
 * feature: 移动相机
 */

using System.Collections;
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

    public UnitStatus status { get; private set; }

    public Vector2Int position { get; private set; }

    public void Init(Vector2Int position)
    {
        display.SetPosition(position);
        display.RefreshStatus(status);
    }

    private void Awake()
    {
        status = new UnitStatus(unitName, maxHitpoint, maxMovePoint, damage, range);
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
