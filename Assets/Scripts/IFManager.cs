/*
 * file: IFManager.cs
 * author: D.H.
 * feature: ���棨interface��������
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFManager : MonoBehaviour
{
    public static IFManager instance;

    #region ȫ������
    [Header("���������")]
    public float clickSensitivity;
    #endregion

    #region ��ʾ״̬
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
