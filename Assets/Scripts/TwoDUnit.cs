using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TwoDUnit : MonoBehaviour, IUnitDisplay
{
    private bool idle = true;
    
    // 目前通过更改透明度显示可用状态
    private static float normalAlpha = 1.0f,
        unavailableAlpha = 0.7f;

    public Slider HPSlider;

    public SpriteRenderer sprite;

    public bool IsIdle()
    {
        return idle;
    }

    public void MoveTo(Vector2Int position)
    {
        idle = false;
        transform.DOMove(
            InterfaceManager.instance.GetActualPosition(position),
            InterfaceManager.instance.moveDuration).OnComplete(() => { idle = true; });
    }

    public void RefreshStatus(UnitStatus status)
    {
        HPSlider.value = (float)status.hitpoint / status.maxHitpoint;
    }

    public void TriggerAvailability(bool availability)
    {
        var sr = transform.Find("sprite").GetComponent<SpriteRenderer>();
        var c = sr.color;
        if (availability)
        {
            c.a = normalAlpha;
            transform.Find("sprite").GetComponent<SpriteRenderer>().color = c;
        }
        else
        {
            c.a = unavailableAlpha;
            transform.Find("sprite").GetComponent<SpriteRenderer>().color = c;
        }
    }

    public void SetPosition(Vector2Int position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
    }

    public void ShowAttackEffect()
    {
        throw new NotImplementedException();
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
