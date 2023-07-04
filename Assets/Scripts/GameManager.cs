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

    // 当前选择的Tile
    private TileController curSelected;

    public void Init()
    {
        Debug.Log("GameManager Init!");
        tileLayer.Init(mapSize);

        IFManager.instance.Init();
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}