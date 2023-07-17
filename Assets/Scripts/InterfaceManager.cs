/*
 * file: InterfaceManager.cs
 * author: D.H.
 * feature: 交互（interface）管理器
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.UI;
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
        curState.GetEvent(BoardEvent.TileClicked);
    }

    #region 棋盘交互状态机

    public enum BoardEvent
    {
        TileClicked,        // 某个瓦片被点击
        AnimationDone       // 当前动画播放完成
    }

    // 三种状态：空闲，选中单位，忙碌
    DHFSM.State<BoardEvent> idle, selected, busying;
    DHFSM.FinateStateMachine<BoardEvent> fsm;

    DHFSM.State<BoardEvent> curState;

    private void InitFSM()
    {
        idle = new DHFSM.State<BoardEvent>(
            fsm: this.fsm,
            onUpdate: null,
            onEnter: () =>
                {
                    uiCanvas.HideUnitStatus();
                },
            onExit: null,
            eventHandler: (BoardEvent e) =>
            {
                switch (e)
                {
                    case BoardEvent.TileClicked:
                        if (curClickedTile.BindedUnit != null)
                        {
                            //uiCanvas.ShowUnitStatus(curClickedTile.BindedUnit.Status);
                            idle.Yield(selected);
                        }
                        break;
                    default: break;
                }
            }
        );

        // 初始状态为idle
        fsm = new DHFSM.FinateStateMachine<BoardEvent>(idle);

        // 进入时保证curClickedTile已刷新，且curClickedTile.BindedUnit != null
        selected = new DHFSM.State<BoardEvent>(
            this.fsm,
            null,
            () =>
                    {
                        selected.dict["curUnit"] = curClickedTile.BindedUnit;
                        uiCanvas.ShowUnitStatus(curClickedTile.BindedUnit.Status);
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

    public void ShowActions(TileController tile)
    {
        if (tile.BindedUnit == null) { return; }
        UnitController unit = tile.BindedUnit;

        Action<TileController> showAction = (tile) =>
        {
            if (unit.CanMoveTo(tile))
            {
                tile.ShowMoveStatus(MoveStatus.CANMOVE);
            }
            // TODO 增加攻击、建造操作
        };

        TileLayer.instance.ScanRange(tile.logicPosition, unit.InfluceRange, )
    }

    public void HideMoveActions(TileController tile)
    {
        if (tile.BindedUnit == null ) { return; }
        UnitController unit = tile.BindedUnit;

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
