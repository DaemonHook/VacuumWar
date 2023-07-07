using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDUnit : MonoBehaviour, IUnitDisplay
{
    private bool idle = true;

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
