/* 
 * file: TileLayer.cs
 * author: D.H
 * feature: 瓦片层逻辑控制
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLayer : MonoBehaviour
{
    public GameObject tile;

    public static TileLayer instance;

    public TileController[,] tiles;

    public void Init(Vector2Int size)
    {
        //Debug.Log("TileLayer Init!");
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        tiles = new TileController[size.x, size.y];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var go = Instantiate(tile, transform);
                var controller = go.GetComponent<TileController>();
                controller.Init(new Vector2Int(i, j));
                tiles[i, j] = controller;
            }
        }
    }

    #region 服务

    public void BindUnit(Vector2Int position, UnitController controller)
    {
        Debug.Log($"Bind {controller.Status.name} to {position}");
        tiles[position.x, position.y].BindUnit(controller);
    }

    public void UnbindUnit(Vector2Int position)
    {
        tiles[position.x, position.y] = null;
    }

    /// <summary>
    /// 将单位绑定至新位置（不负责移动）
    /// </summary>
    /// <param name="oldPosition">旧位置</param>
    /// <param name="newPosition">新位置</param>
    public bool RebindUnit(Vector2Int oldPosition, Vector2Int newPosition)
    {
        var unit = tiles[oldPosition.x, oldPosition.y].BindedUnit;
        if (unit == null)
        {
            return false;
        }
        tiles[oldPosition.x, oldPosition.y].UnbindUnit();
        tiles[newPosition.x, newPosition.y].BindUnit(unit);
        return true;
    }

    #endregion

    #region 服务接口

    /// <summary>
    /// 扫描一定范围内所有的tile（范围为正方形）
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="hlen">半边长</param>
    /// <param name="callback">回调</param>
    public void ScanRange(Vector2Int center, int hlen, Action<TileController> callback)
    {
        Debug.Log($"ScanRange: center: {center}, hlen: {hlen}");
        for (int i = center.x - hlen; i <= center.x + hlen; i++)
        {
            for (int j = center.y - hlen; j <= center.y + hlen; j++)
            {
                if (0 <= i && i < tiles.GetLength(0) && 0 <= j && j < tiles.GetLength(1))
                {
                    callback(tiles[i, j]);
                }
            }
        }
    }



    #endregion

    private void Awake()
    {
        instance = this;
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
