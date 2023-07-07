using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLayer : MonoBehaviour
{
    public static UnitLayer instance;

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
