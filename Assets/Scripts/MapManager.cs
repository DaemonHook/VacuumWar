/*
 * file: MapManager.cs
 * author: D.H.
 * feature: 地图管理器
 */

using System.Collections;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }

    public MapData curMapData;

    public Vector2Int mapSize;

    public void InitMap(string name)
    {
        curMapData = ResourceManager.instance.GetMapData(name);
        
    }

    private void Awake()
    {
        instance = this;
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
