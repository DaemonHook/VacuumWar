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

    public TileController[,] tileControllers;

    public void Init(Vector2Int size)
    {
        //Debug.Log("TileLayer Init!");
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        tileControllers = new TileController[size.x, size.y];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                //Debug.Log($"Init Tile at {i}, {j}");
                var go = Instantiate(tile, transform);
                go.GetComponent<TileController>().Init(new Vector2Int(i, j));
            }
        }
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
