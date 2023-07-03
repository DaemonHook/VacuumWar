/*
 * file: TwoDTile.cs
 * author: D.H.
 * feature: 2d瓦片交互逻辑
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TwoDTile : MonoBehaviour, ITileDisplay
{
    private TileController upperController;

    // 选择框
    public GameObject selectedGO;

    float timeCounter;

    private void SelectedModeOn()
    {
        selectedGO.SetActive(true);
    }

    private void SelectedModeOff()
    {
        selectedGO.SetActive(true);
    }

    private void Awake()
    {
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
        Debug.Log($"mouse down!");
        //actionOnClicked.Invoke();
        Debug.Log($"Mouse down onto {upperController.worldPosition}");
        timeCounter = Time.time;
    }

    private void OnMouseUp()
    {
        float timeSpent = Time.time - timeCounter;
        if (timeCounter > 0.0f && timeSpent > IFManager.instance.clickSensitivity
            && IFManager.instance.inDrag == false)
        {
            //Debug.Log($"{this.worldPosition} cell is clicked!");
            upperController.OnClicked();
            //actionOnClicked();
        }
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
        upperController = controller;
        Debug.Log($"controller is {upperController}");
    }

    public void Init(Vector2Int position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
    }
}
