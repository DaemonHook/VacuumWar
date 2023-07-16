using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TwoDUnit : MonoBehaviour, IUnitDisplay
{
    private bool idle = true;

    public Slider HPSlider;

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

    public void SetPosition(Vector2Int position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
    }

    public void ShowAttackEffect()
    {
        throw new System.NotImplementedException();
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
