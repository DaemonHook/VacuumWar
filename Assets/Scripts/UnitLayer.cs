using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

public class UnitLayer : MonoBehaviour
{
    public static UnitLayer instance;

    public List<UnitController> units;

    public Dictionary<Vector2Int, List<UnitController>> unitDict;

    public UnitController AddUnit(string name, Vector2Int position)
    {
        name = name.ToLower();
        if (!unitDict.ContainsKey(position))
        {
            unitDict[position] = new List<UnitController>();
        }
        var go = ResourceManager.instance.GetUnitGO(name);
        var newUnit = Instantiate(go, transform);
        var newUnitController = newUnit.GetComponent<UnitController>();
        newUnitController.Init(position);
        units.Add(newUnitController);
        unitDict[position].Add(newUnitController);
        return newUnitController;
    }

    public bool MoveUnit(Vector2Int oldPos, Vector2Int newPos)
    {
        if (unitDict.ContainsKey(oldPos) && !unitDict.ContainsKey(newPos))
        {
            var unit = unitDict[oldPos];
            unitDict.Remove(oldPos);
            unitDict.Add(newPos, unit);
            
            return true;
        }
        return false;
    }

    public void RemoveUnit(UnitController controller)
    {
        units.Remove(controller);
        unitDict[controller.logicPosition].Remove(controller);
        Destroy(controller.gameObject);
    }

    private void Awake()
    {
        instance = this;
        units = new List<UnitController>();
        unitDict = new Dictionary<Vector2Int, List<UnitController>>();
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
