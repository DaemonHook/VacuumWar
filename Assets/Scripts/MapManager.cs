﻿/*
 * file: MapManager.cs
 * author: D.H.
 * feature: 地图管理器
 */

using System;
using System.Collections;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }

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
