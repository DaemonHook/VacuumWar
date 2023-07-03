/*
 * file: IFManager.cs
 * author: D.H.
 * feature: 交互（interface）管理器
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFManager : MonoBehaviour
{
    public static IFManager instance;

    #region 实现功能的模块
    public CameraMove cameraMove;
    #endregion

    #region 交互设置
    [Header("点击灵敏度")]
    public float clickSensitivity;
    #endregion

    #region 交互状态
    public bool inDrag { get; private set; }
    #endregion

    public void EnterDrag()
    {
        inDrag = true;
    }

    public void LeaveDrag()
    {
        inDrag = false;
    }

    public void SetMapSize(Vector2Int size)
    {
        cameraMove.leftDown = new Vector2Int(0, 0);
        cameraMove.upRight = size;
    }

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
