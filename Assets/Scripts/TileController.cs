/*
 * file: LogicTile.cs
 * author: D.H.
 * feature: 瓦片逻辑控制器
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// 瓦片逻辑控制器
public class TileController : MonoBehaviour
{
    public int height;

    public Vector2Int logicPosition;

    private ITileDisplay tileDisplay;

    public UnitController BindedUnit { get; private set; }

    public void Init(Vector2Int position)
    {
        tileDisplay = GetComponent<ITileDisplay>();
        logicPosition = position;
        tileDisplay.Init(position);
        tileDisplay.BindController(this);
    }

    public void Select()
    {
        string unit = BindedUnit == null ? "null" : BindedUnit.name;
        Debug.Log($"Selected {logicPosition} unit: {unit}");
        tileDisplay.TriggerSelectedMode(true);
    }

    public void UnSelect()
    {
        Debug.Log($"UnSelected {logicPosition}");
        tileDisplay.TriggerSelectedMode(false);
    }

    public void OnClicked()
    {
        InterfaceManager.instance.TileClicked(this);
    }

    public void BindUnit(UnitController unit)
    {
        BindedUnit = unit;
    }

    public void UnbindUnit()
    {
        BindedUnit = null;
    }

    /// <summary>
    /// 显示移动状态（如可移动、被阻挡等）
    /// </summary>
    /// <param name="status"></param>
    public void SwitchStatus(MoveStatus status)
    {
        tileDisplay.SwitchMoveStatus(status);
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
