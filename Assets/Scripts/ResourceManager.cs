/*
 * file: ResourceManager.cs
 * author: D.H.
 * feature: 动态资源加载器
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    private Dictionary<string, MapData> mapCache = new Dictionary<string, MapData>();

    private Dictionary<string, GameObject> unitCache = new Dictionary<string, GameObject>();

    public MapData GetMapData(string name)
    {
        name = name.ToLower();
        if (mapCache.ContainsKey(name))
        {
            return mapCache[name];
        }
        else return null;
    }

    public GameObject GetUnitGO(string name)
    {
        name = name.ToLower();
        if (unitCache.ContainsKey(name))
        {
            return unitCache[name];
        }
        else return null;
    }

    private void Awake()
    {
        instance = this;
        var allUnits = Resources.LoadAll<GameObject>("Units");
        foreach (var unit in allUnits)
        {
            if (unit.GetComponent<UnitController>() != null)
            {
                var unitName = unit.GetComponent<UnitController>().unitName.ToLower();
                unitCache[unitName] = unit;
            }
        }

        var allMaps = Resources.LoadAll<TextAsset>("Maps");
        foreach (var map in allMaps)
        {
            var mapData = JsonUtility.FromJson<MapData>(map.text);

            if (mapData.name != default(string))
            {
                Debug.Log($"MapName: {mapData.name}");
                mapCache[mapData.name.ToLower()] = mapData;
            }
        }
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
