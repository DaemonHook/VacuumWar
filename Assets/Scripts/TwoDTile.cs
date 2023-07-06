/*
 * file: TwoDTile.cs
 * author: D.H.
 * feature: 2d瓦片交互逻辑
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TwoDTile : MonoBehaviour, ITileDisplay, IPointerDownHandler, IPointerUpHandler
{
    private TileController controller;

    // 选择框
    public GameObject selectedGO;
    
    public GameObject tileColorGo;

    #region 瓦片颜色定义
    public Color[] groundColors;

    public Color canceledColor, canMoveColor, cannotMoveColor, attackable, buildAble;

    private static int tolCount = 0;
    #endregion

    float timeCounter;

    private void SelectedModeOn()
    {
        selectedGO.SetActive(true);
    }

    private void SelectedModeOff()
    {
        selectedGO.SetActive(false);
    }

    private void Awake()
    {
        selectedGO.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (groundColors.Length != 0)
        {
            //int curCount = tolCount % groundColors.Length;
            //tileColorGo.GetComponent<SpriteRenderer>().color = groundColors[UnityEngine.Random.Range(0, groundColors.Length)];
            ////Debug.Log($"color is {groundColors[curCount]}");
            //tolCount++;
        }
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


    public void TriggerSelectedMode(bool status)
    {
        if (status)
        {
            SelectedModeOn();
        }
        else
        {
            SelectedModeOff();
        }
    }

    public void TriggerMoveStatusMode(MoveStatus moveStatus)
    {
        throw new NotImplementedException();
    }

    public void BindController(TileController controller)
    {
        this.controller = controller;
        //Debug.Log($"controller is {upperController}");
    }

    public void Init(Vector2Int position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log($"Pointer down: {eventData}");
        timeCounter = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log($"Pointer up: {eventData}");
        float timeSpent = Time.time - timeCounter;
        if (timeCounter > 0.0f && timeSpent > InterfaceManager.instance.clickSensitivity
            && InterfaceManager.instance.inDrag == false)
        {
            controller.OnClicked();
            //actionOnClicked();
        }
    }
}
