using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TwoDTile : MonoBehaviour
{
    LogicTile logicTile;

    private void Awake()
    {
        logicTile = GetComponent<LogicTile>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        // ������ִ�е�����崥�����¼�
                        // ���Ե��������ű��еķ������ڴ˴�����Զ����߼�
                        Debug.Log("Clicked!");
                    }
                }
            }
        }
    }
}
