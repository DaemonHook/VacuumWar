/*
 * file: IFManager.cs
 * author: D.H.
 * feature: 界面（interface）管理器
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFManager : MonoBehaviour
{
    public static IFManager instance;

    #region 全局设置
    [Header("点击灵敏度")]
    public float clickSensitivity;
    #endregion

    #region 显示状态
    public bool inDrag = false;
    #endregion

    public Action actionOnClicked;

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

    }
}
