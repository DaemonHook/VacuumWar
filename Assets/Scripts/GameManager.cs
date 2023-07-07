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

    #region 游戏图层的引用
    public GameObject tilesLayerGO, groundLayerGO, unitLayerGO;

    private TileLayer tileLayer;
    private GroundLayer groundLayer;
    private UnitLayer unitLayer;
    #endregion

    public Vector2Int mapSize;

    public void Init()
    {
        Debug.Log("GameManager Init!");
        tileLayer.Init(mapSize);

        InterfaceManager.instance.Init();
    }



    private void Awake()
    {
        instance = this;
        tileLayer = tilesLayerGO.GetComponent<TileLayer>();
        groundLayer = groundLayerGO.GetComponent<GroundLayer>();
        unitLayer = unitLayerGO.GetComponent<UnitLayer>();
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        //List<UnitData> unitList = new List<UnitData>
        //{
        //    new UnitData() { name = "King", position = new int[] { 1, 2 } },
        //    new UnitData() { name = "Queen", position = new int[] { 3, 4 } },
        //    new UnitData() { name = "Pawn", position = new int[] { 5, 6 } },
        //};
        //List<GroundData> groundList = new List<GroundData>()
        //{
        //    new GroundData() {name = "Water", position = new int[] { 1, 1 } },
        //    new GroundData() {name = "Water", position = new int[] { 1, 2 } },
        //    new GroundData() {name = "Water", position = new int[] { 1, 3 } },
        //    new GroundData() {name = "Water", position = new int[] { 1, 3 } },
        //    new GroundData() {name = "Hill", position = new int[] { 5, 3 } },
        //};
        //MapData data = new MapData()
        //{
        //    name = "水火既济",
        //    version = 1,
        //    size = new int[] { 10, 10 },
        //    ground = groundList,
        //    unit = unitList,
        //};
        //Debug.Log(JsonUtility.ToJson(data));

    }

    // Update is called once per frame
    void Update()
    {

    }
}