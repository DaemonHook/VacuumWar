/*
 * file: GameManager.cs
 * author: D.H.
 * feature: 游戏逻辑控制
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public Vector2Int mapSize;
    public string mapName;

    public void Init()
    {
        MapManager.instance.InitMap(mapName);
        InterfaceManager.instance.Init();
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}