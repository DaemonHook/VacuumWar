using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

public class UnitLayer : MonoBehaviour
{
    public static UnitLayer instance;

    [Header("∂”ŒÈ—’…´")]
    public Color[] teamColors;

    public List<UnitController> units;

    public Dictionary<Vector2Int, UnitController> unitDict;

    public UnitController AddUnit(string unitName, Vector2Int position)
    {
        unitName = unitName.ToLower();
        var go = ResourceManager.instance.GetUnitGO(unitName);
        var newUnit = Instantiate(go, transform);
        var newUnitController = newUnit.GetComponent<UnitController>();
        newUnitController.Init(position);
        units.Add(newUnitController);
        if (!unitDict.ContainsKey(position))
        {
            unitDict[position] = newUnitController;
        }
        return newUnitController;
    }

    public void MoveUnit(Vector2Int oldPos, Vector2Int newPos)
    {
        if (unitDict.ContainsKey(oldPos) && !unitDict.ContainsKey(newPos))
        {
            UnitController unit = unitDict[oldPos];
            unitDict.Remove(oldPos);
            unitDict.Add(newPos, unit);
            unit.MoveSelf(newPos);
        }
        else
        {
            Debug.LogError($"MoveUnit: Invalid move {oldPos} to {newPos}");
        }
    }

    public void RemoveUnit(UnitController controller)
    {
        units.Remove(controller);
        unitDict.Remove(controller.logicPosition);
        Destroy(controller.gameObject);
    }

    private void Awake()
    {
        instance = this;
        units = new List<UnitController>();
        unitDict = new Dictionary<Vector2Int, UnitController>();
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
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
