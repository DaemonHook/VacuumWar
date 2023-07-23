/*
 * file: MapManager.cs
 * author: D.H.
 * feature: 地图管理器
 */

using System;
using System.Collections;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }

    public MapData curMapData { get; private set; }

    public Vector2Int mapSize;

    public void InitMap(string name)
    {
        curMapData = ResourceManager.instance.GetMapData(name);
        mapSize = new Vector2Int(curMapData.size[0], curMapData.size[1]);

        //Debug.Log($"Initmap: {name}");
        //print($"ground: {curMapData.ground[0].name}");
        foreach (var groundData in curMapData.ground)
        {
            //Debug.Log(groundData);
            //Debug.Log($"groundData.name:{groundData.name}, groundData.position: {groundData.position[0]}, {groundData.position[1]}");
            //Debug.Log(GroundLayer.instance);

            GroundLayer.instance.SetGround(groundData.name,
                new Vector2Int(groundData.position[0], groundData.position[1]));
        }
        TileLayer.instance.Init(mapSize);

        foreach (var unitData in curMapData.unit)
        {
            var pos = new Vector2Int(unitData.position[0], unitData.position[1]);
            var controller = UnitLayer.instance.AddUnit(unitData.name, pos);
            TileLayer.instance.BindUnit(pos, controller);
        }
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
