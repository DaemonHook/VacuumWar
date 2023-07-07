using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLayer : MonoBehaviour
{
    public static GroundLayer instance;

    public GameObject waterGO, hillGO, mineGO;

    public Dictionary<Vector2Int, GameObject> groundGODict = new Dictionary<Vector2Int, GameObject>();

    public void SetGround(string name, Vector2Int position)
    {
        //Debug.Log($"SetGround: {name}, {position}");
        if (groundGODict.ContainsKey(position))
        {
            Destroy(groundGODict[position]);
        }
        name = name.ToLower();
        GameObject go = null;
        switch (name)
        {
            case "water":
                go = Instantiate(waterGO, transform);
                go.GetComponent<GroundController>().Init(position);
                break;
            case "mine":
                go = Instantiate(mineGO, transform);
                go.GetComponent<GroundController>().Init(position);
                break;
            case "hill":
                go = Instantiate(hillGO, transform);
                go.GetComponent<GroundController>().Init(position);
                break;
            default:
                Debug.LogError($"invalid groundname: {name}");
                break;
        }
        if (go != null)
        {
            go.GetComponent<GroundController>().Init(position);
            groundGODict[position] = go;
        }
    }

    private void Awake()
    {
        instance = this;
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
