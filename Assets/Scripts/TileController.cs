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

    public void Select()
    {
        Debug.Log($"Selected {worldPosition}");
        tileDisplay.TriggerSelectedMode(true);
    }

    public void UnSelect()
    {
        Debug.Log($"UnSelected {worldPosition}");
        tileDisplay.TriggerSelectedMode(false);
    }

    public void OnClicked()
    {
        InterfaceManager.instance.TileClicked(this);
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
