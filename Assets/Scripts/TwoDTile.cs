/*
 * file: TwoDTile.cs
 * author: D.H.
 * feature: 2d��Ƭ�Ľ�������
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TwoDTile : MonoBehaviour
{
    LogicTile logicTile;

    // ѡ���
    public GameObject selectedGO;

    float timeCounter;

    private void SelectedModeOn()
    {
        selectedGO.SetActive(true);
    }

    private void Awake()
    {
        logicTile = GetComponent<LogicTile>();
        selectedGO.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(Vector2Int worldPosition, Action actionOnClicked)
    {
        //this.worldPosition = worldPosition;
        //this.actionOnClicked = actionOnClicked;
    }

    private void OnMouseDown()
    {
        //actionOnClicked.Invoke();
        timeCounter = Time.time;
    }

    private void OnMouseUp()
    {
        float timeSpent = Time.time - timeCounter;
        if (timeCounter > 0.0f && timeSpent > IFManager.instance.clickSensitivity
            && IFManager.instance.inDrag == false)
        {
            //Debug.Log($"{this.worldPosition} cell is clicked!");
            SelectedModeOn();
            //actionOnClicked();
        }
    }
}
