using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class UnitLayer : MonoBehaviour
{
    public static UnitLayer instance;

    public List<UnitController> units;

    public Dictionary<Vector2Int, List<UnitController>> pos2unit;

    public UnitController AddUnit(string name, Vector2Int position)
    {
        name = name.ToLower();
        if (!pos2unit.ContainsKey(position))
        {
            pos2unit[position] = new List<UnitController>();
        }
        var go = ResourceManager.instance.GetUnitGO(name);
        var newUnit = Instantiate(go, transform);
        var newUnitController = newUnit.GetComponent<UnitController>();
        newUnitController.Init(position);
        units.Add(newUnitController);
        pos2unit[position].Add(newUnitController);
        return newUnitController;
    }

    public void RemoveUnit(UnitController controller)
    {
        units.Remove(controller);
        pos2unit[controller.Position].Remove(controller);
        Destroy(controller.gameObject);
    }

    private void Awake()
    {
        instance = this;
        units = new List<UnitController>();
        pos2unit = new Dictionary<Vector2Int, List<UnitController>>();
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
