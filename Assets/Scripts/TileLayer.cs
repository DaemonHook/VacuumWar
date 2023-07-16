/* 
 * file: TileLayer.cs
 * author: D.H
 * feature: 瓦片层逻辑控制
 */

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

    public void BindUnit(Vector2Int position, UnitController controller)
    {
        Debug.Log($"Bind {controller.status.name} to {position}");
        tiles[position.x, position.y].BindUnit(controller);
    }

    public void UnbindUnit(Vector2Int position)
    {
        tiles[position.x, position.y] = null;
    }

    public void MoveUnit(Vector2Int oldPosition, Vector2Int newPosition)
    {
        var unit = tiles[oldPosition.x, oldPosition.y].BindedUnit;
        tiles[oldPosition.x, oldPosition.y].UnbindUnit();
        tiles[newPosition.x, newPosition.y].BindUnit(unit);
    }

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
