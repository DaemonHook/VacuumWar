/*
 * file: InterfaceManager.cs
 * author: D.H.
 * feature: 交互（interface）管理器
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    #region 功能模块
    public CameraMove cameraMove;
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
    private TileController curSelected;

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
        if (curSelected != null)
        {
            curSelected.UnSelect();
        }
        clickedTile.Select();
        curSelected = clickedTile;

    }

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

    public void ShowActions(TileController tileController)
    {

    }

    #endregion

    private void Awake()
    {
        instance = this;
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
