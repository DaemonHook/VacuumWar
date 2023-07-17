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

    public GameObject maskGO;
    #region 瓦片颜色定义
    public Color[] groundColors;

    public Color canMoveColor, cannotMoveColor, attackableColor, buildAbleColor;

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
        GameObject go = null;
        GroundLayer.instance.groundGODict.TryGetValue(controller.logicPosition, out go);
        int h = go ? go.GetComponent<GroundController>().height : 0;
        if (h < 0)
        {
            tileColorGo.GetComponent<SpriteRenderer>().color = groundColors[0];
        }
        else if (h == 0)
        {
            tileColorGo.GetComponent<SpriteRenderer>().color = groundColors[1];
        }
        else
        {
            tileColorGo.GetComponent<SpriteRenderer>().color = groundColors[2];
        }
    }

    // Update is called once per frame
    void Update()
    {
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

    public void SwitchMoveStatus(MoveStatus moveStatus)
    {
        maskGO.SetActive(true);
        switch (moveStatus)
        {
            case MoveStatus.None:
                maskGO.SetActive(false); break;
            case MoveStatus.CANMOVE:
                maskGO.GetComponent<SpriteRenderer>().color = canMoveColor; break;
            case MoveStatus.CANNOTMOVE:
                maskGO.GetComponent<SpriteRenderer>().color = cannotMoveColor; break;
            case MoveStatus.ATTACKABLE:
                maskGO.GetComponent<SpriteRenderer>().color = attackableColor; break;
            case MoveStatus.BUILDABLE:
                maskGO.GetComponent<SpriteRenderer>().color = buildAbleColor; break;
            default: break;

        }
    }

    public void BindController(TileController controller)
    {
        this.controller = controller;
    }

    public void Init(Vector2Int position)
    {
        InterfaceManager.instance.SetGOPosition(gameObject, position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timeCounter = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float timeSpent = Time.time - timeCounter;
        if (timeCounter > 0.0f && timeSpent > InterfaceManager.instance.clickSensitivity
            && InterfaceManager.instance.inDrag == false)
        {
            controller.OnClicked();
        }
    }
}
