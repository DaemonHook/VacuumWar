/*
 * file: CameraMove.cs
 * author: D.H.
 * feature: 移动镜头
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class CameraMove : MonoBehaviour
{
    private Camera thisCamera;

    [Header("游戏区域大小")]
    public Vector2 leftDown, upRight;

    [Header("镜头移动边界的容差")]
    public float tolerance;

    [Header("镜头移动的灵敏度")]
    public float sensitivity;

    [Header("移动持续时间")]
    public float duration;

    private Vector2 mapSize;

    private bool inDrag = false;

    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!IFManager.instance.inDrag && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DragMoveCoroutine());
        }
    }

    Vector3 GetMousePositionInWorld()
    {
        Vector3 screenPosition = Input.mousePosition;
        return thisCamera.ScreenToWorldPoint(screenPosition);
    }

    private IEnumerator DragMoveCoroutine()
    {
        Vector3 initialMousePosition = GetMousePositionInWorld();
        Debug.Log($"originPosition: {initialMousePosition}");
        while (Input.GetMouseButton(0))
        {
            //Debug.Log("Check Drag!");
            Vector3 currentMousePosition = GetMousePositionInWorld();
            Vector3 travel = currentMousePosition - initialMousePosition;
            Debug.Log($"target: {transform.position - travel}");
            travel.z = 0;
            if (travel.magnitude / transform.GetComponent<Camera>().orthographicSize > sensitivity)
            {
                IFManager.instance.inDrag = true;
            }
            CameraMoveWithTolerance(transform.position - travel);
            yield return null;
        }
        IFManager.instance.inDrag = false;
    }

    public void CameraMoveWithTolerance(Vector3 target)
    {
        target.x = Mathf.Clamp(target.x, leftDown.x + tolerance, upRight.x - tolerance);
        target.y = Mathf.Clamp(target.y, leftDown.y + tolerance, upRight.y - tolerance);
        Debug.Log($"Camera move to {target}");
        transform.DOMove(target, duration);
    }

    public void Init(Vector2 mapSize, Vector2 originPos)
    {
        this.mapSize = mapSize;
        transform.position = new Vector3(originPos.x, originPos.y, -10.0f);
    }
}
