/*
 * file: LogicTile.cs
 * author: D.H.
 * feature: 瓦片逻辑控制器
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// 瓦片逻辑控制器
public class TileController : MonoBehaviour
{
    public int height;

    public Vector2Int worldPosition;

    private ITileDisplay tileDisplay;

    public void Init(Vector2Int position)
    {
        tileDisplay = GetComponent<ITileDisplay>();
        worldPosition = position;
        tileDisplay.Init(position);
        tileDisplay.BindController(this);
    }

    public void OnClicked()
    {
        tileDisplay.TriggerSelectedMode(true);
        Debug.Log($"Tile on {worldPosition} is clicked!");
    }


    private void Awake()
    {
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
