/*
 * file: InterfaceManager.cs
 * author: D.H.
 * feature: 交互（interface）管理器
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Security;
using Unity.VisualScripting;
//using UnityEditor.Tilemaps;
//using UnityEditor.UI;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    #region 功能模块
    public CameraMove cameraMove;

    public UICanvas uiCanvas;

    #endregion

    #region 交互设置
    [Header("点击灵敏度")]
    public float clickSensitivity;

    [Header("单位移动时间")]
    public float moveDuration;
    #endregion

    #region 交互状态
    public bool inDrag { get; private set; }

    // 当前选中的tile
    private TileController curClickedTile;

    #endregion

    public void Init()
    {
        InitCamera();
    }

    public void InitCamera()
    {
        cameraMove.Init(MapManager.instance.mapSize,
            new Vector2Int(MapManager.instance.mapSize.x / 2, MapManager.instance.mapSize.y / 2));
    }

    #region 设置交互状态

    /// <summary>
    /// 进入拖动状态
    /// </summary>
    public void EnterDrag()
    {
        inDrag = true;
    }

    /// <summary>
    /// 离开拖动状态
    /// </summary>
    public void LeaveDrag()
    {
        inDrag = false;
    }

    public void SetMapSize(Vector2Int size)
    {
        cameraMove.leftDown = new Vector2Int(0, 0);
        cameraMove.upRight = size;
    }

    #endregion

    #region 交互功能

    public void TileClicked(TileController clickedTile)
    {
        if (curClickedTile != null)
        {
            curClickedTile.UnSelect();
        }
        clickedTile.Select();
        curClickedTile = clickedTile;
        fsm.GetEvent(BoardEvent.TileClicked);
    }

    #region 棋盘交互状态机

    public enum BoardEvent
    {
        TileClicked,        // 某个瓦片被点击
        AnimationDone       // 当前动画播放完成
    }

    // 三种状态：空闲，选中单位，忙碌
    DHFSM.State<BoardEvent>
        idle = new DHFSM.State<BoardEvent>(),
        selected = new DHFSM.State<BoardEvent>(),
        busying = new DHFSM.State<BoardEvent>();
    DHFSM.FinateStateMachine<BoardEvent> fsm = new DHFSM.FinateStateMachine<BoardEvent>();

    //HashSet<Vector2Int> activeTiles = new HashSet<Vector2Int>();
    Dictionary<Vector2Int, MoveStatus> activeTiles = new Dictionary<Vector2Int, MoveStatus>();
    //Dictionary<Vector2Int, Action> activeActions = new Dictionary<Vector2Int, Action>();

    private void InitFSM()
    {
        idle.Init(
            fsm: fsm,
            onEnter: () =>
                {
                    //Debug.Log("uicanvas hid!");
                    uiCanvas.HideUnitStatus();
                },
            onExit: null,
            onEvent: (BoardEvent e) =>
            {
                switch (e)
                {
                    case BoardEvent.TileClicked:
                        Debug.Log("idle: TileClicked!");
                        if (curClickedTile.BindedUnit != null)
                        {
                            idle.Yield(selected);
                        }
                        break;
                    default: break;
                }
            }
        );

        // 初始状态为idle
        fsm.SetInitialState(idle);
        // 进入时保证curClickedTile已刷新，且curClickedTile.BindedUnit != null
        selected.Init(
            fsm,
            () =>
                    {
                        //Debug.Log("selected Entered!");
                        selected.dict["curUnit"] = curClickedTile.BindedUnit;
                        uiCanvas.ShowUnitStatus(curClickedTile.BindedUnit.Status);
                        RefreshActiveActions(curClickedTile);

                        foreach (var pos in activeTiles.Keys)
                        {
                            Debug.Log($"activeTile: {pos}");
                        }
                    },
            () =>
                    {
                        uiCanvas.HideUnitStatus();
                        selected.dict["curUnit"] = null;
                    },
            (BoardEvent e) =>
                    {
                        switch (e)
                        {
                            case BoardEvent.TileClicked:
                                if (curClickedTile.BindedUnit != null)
                                {
                                    selected.Yield(selected);
                                }
                                else
                                {
                                    selected.Yield(idle);
                                }
                                break;
                            default: break;
                        }
                    }
            );


    }

    #endregion

    /// <summary>
    /// 单位的逻辑位置转化为物体实际位置
    /// </summary>
    /// <param name="logicPosition">逻辑位置</param>
    /// <returns></returns>
    public Vector3 GetActualPosition(Vector2Int logicPosition)
    {
        return new Vector3(logicPosition.x, logicPosition.y, 0);
    }

    /// <summary>
    /// 根据逻辑位置设置物体的实际位置
    /// </summary>
    /// <param name="go"></param>
    /// <param name="logicPosition"></param>
    public void SetGOPosition(GameObject go, Vector2Int logicPosition)
    {
        go.transform.position = new Vector3(logicPosition.x, logicPosition.y, 0);
    }

    /// <summary>
    /// 刷新当前单位的行动
    /// </summary>
    /// <param name="tile"></param>
    public void RefreshActiveActions(TileController tile)
    {
        if (tile.BindedUnit == null) { return; }
        UnitController unit = tile.BindedUnit;
        activeTiles.Clear();
        Action<TileController> refreshAction = (tile) =>
        {
            if (unit.CanMoveTo(tile))
            {
                activeTiles.Add(tile.logicPosition, MoveStatus.CANMOVE);
            }
            // TODO 增加攻击、建造操作
            else
            {

            }
        };
        TileLayer.instance.ScanRange(tile.logicPosition, unit.InfluceRange, refreshAction);
    }

    public void ShowActiveActions()
    {
        foreach (var kvp in activeTiles)
        {

        }
    }

    public void HideMoveActions(TileController tile)
    {
    }

    #endregion

    private void Awake()
    {
        instance = this;
        InitFSM();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inDrag && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(cameraMove.DragMoveCoroutine());
        }
    }
}
