using System.Collections;
using UnityEngine;

public class TwoDGround : MonoBehaviour, IGroundDisplay
{
    public void SetPosition(Vector2Int logicPosition)
    {
        InterfaceManager.instance.SetGOPosition(gameObject, logicPosition);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}