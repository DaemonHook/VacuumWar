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
    public bool InDrag { get; private set; }

    // 当前点击的tile
    private TileController curClickedTile;
    
    // 当前选中的tile
    private TileController curSelectedTile;

    #endregion

    public void Init()
    {
        InitCamera();
    }

    public void InitCamera()
    {
        cameraMove.Init(DataManager.instance.mapSize,
            new Vector2Int(DataManager.instance.mapSize.x / 2, DataManager.instance.mapSize.y / 2));
    }

    #region 设置交互状态

    /// <summary>
    /// 进入拖动状态
    /// </summary>
    public void EnterDrag()
    {
        InDrag = true;
    }

    /// <summary>
    /// 离开拖动状态
    /// </summary>
    public void LeaveDrag()
    {
        InDrag = false;
    }

    public void SetMapSize(Vector2Int size)
    {
        cameraMove.leftDown = new Vector2Int(0, 0);
        cameraMove.upRight = size;
    }

    #endregion

    #region 交互功能

    public void ClickTile(TileController clickedTile)
    {
        curClickedTile = clickedTile;
        fsm.GetEvent(BoardEvent.TileClicked);
    }

    public void SelectTile(TileController selectedTile)
    {
        if (curSelectedTile != null)
        {
            curSelectedTile.UnSelect();
        }

        curSelectedTile = selectedTile;
        curSelectedTile.Select();
    }
    
    #region 棋盘交互状态机

    private enum BoardEvent
    {
        TileClicked,        // 某个瓦片被点击
        ActionDone,         // 当前动作开始
        ActionStart         // 当前动作完成
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
                // 空闲时，若选中了单位则切换至选中模式
                switch (e)
                {
                    case BoardEvent.TileClicked:
                        Debug.Log("idle: TileClicked!");
                        SelectTile(curClickedTile);
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
        
        // 选中模式
        // 进入时保证curClickedTile已刷新，且curClickedTile.BindedUnit != null
        selected.Init(
            fsm,
            () =>
                    {
                        // 进入选中模式时，显示单位信息、可行动作
                        // 并且绑定当前单位
                        selected.dict["curUnit"] = curSelectedTile.BindedUnit;
                        uiCanvas.ShowUnitStatus(curSelectedTile.BindedUnit.Status);
                        RefreshActiveActions(curSelectedTile);
                        ShowActiveActions();
                    },
            
            () =>
                    {
                        // 退出选中模式时，隐藏单位信息、可行动作，解绑当前单位
                        uiCanvas.HideUnitStatus();
                        HideActiveActions();
                        selected.dict["curUnit"] = null;
                    },
            
            (BoardEvent e) =>
                    {
                        switch (e)
                        {
                            case BoardEvent.TileClicked:
                                // 发生点击事件时
                                if (activeTiles.ContainsKey(curClickedTile.logicPosition))
                                {
                                    // 若点击了当前活跃瓦片
                                    
                                    //TODO: 添加可行的动作
                                    switch (activeTiles[curClickedTile.logicPosition])
                                    {
                                        
                                        case MoveStatus.CANMOVE:
                                            if (GameManager.instance.DoAction((UnitController)selected.dict["curUnit"],
                                                    ActionType.MOVE, (object)curClickedTile.logicPosition))
                                            {
                                                // 确保单位已经重新绑定
                                                SelectTile(curClickedTile);
                                                selected.Yield(selected);
                                            }
                                            break;
                                        default: break;
                                    }
                                }
                                else if (curClickedTile.BindedUnit != null)
                                {
                                    // 若点击到单位
                                    if (curClickedTile.BindedUnit == (UnitController)selected.dict["curUnit"])
                                    {
                                        // 若所点击的是当前已经选中的单位，则退出选中状态
                                        selected.Yield(idle);
                                    }
                                    else if (curClickedTile.BindedUnit.Idle)
                                    {
                                        // 若点击了其他单位，则选中该单位
                                        SelectTile(curClickedTile);
                                        selected.Yield(selected);
                                    }
                                }
                                else
                                {
                                    // 若点击了空地，退出选中状态
                                    SelectTile(curClickedTile);
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
    /// <param name="curTile"></param>
    public void RefreshActiveActions(TileController curTile)
    {
        if (curTile.BindedUnit == null)
        {
            Debug.LogError($"RefreshActiveActions: null unit on curTile");
            return;
        }
        UnitController unit = curTile.BindedUnit;
        activeTiles.Clear();
        Action<TileController> refreshAction = (tile) =>
        {
            if (GameManager.instance.CanMoveTo(unit, tile))
            {
                // Debug.Log($"Unit at {unit.logicPosition} can move to {tile.logicPosition}");
                activeTiles.Add(tile.logicPosition, MoveStatus.CANMOVE);
            }
            else
            {
                //TODO 增加更多操作
            }
        };

        TileLayer.instance.ScanRange(curTile.logicPosition, unit.InfluceRange, refreshAction);
    }

    public void ShowActiveActions()
    {
        foreach (var kvp in activeTiles)
        {
            // Debug.Log($"{kvp.Key} is {kvp.Value}");
            TileLayer.instance.tiles[kvp.Key.x, kvp.Key.y].SwitchStatus(kvp.Value);
        }
    }

    public void HideActiveActions()
    {
        foreach (var kvp in activeTiles)
        {
            TileLayer.instance.tiles[kvp.Key.x, kvp.Key.y].SwitchStatus(MoveStatus.NONE);
        }
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
        if (!InDrag && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(cameraMove.DragMoveCoroutine());
        }
    }
}
