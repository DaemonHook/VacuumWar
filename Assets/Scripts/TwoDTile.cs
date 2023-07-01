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
                        // 在这里执行点击物体触发的事件
                        // 可以调用其他脚本中的方法或在此处添加自定义逻辑
                        Debug.Log("Clicked!");
                    }
                }
            }
        }
    }
}
