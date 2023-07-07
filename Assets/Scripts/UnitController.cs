/*
 * file: UnitController.cs
 * author: D.H.
 * feature: 移动相机
 */

using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    #region 外部设置
    public string unitName;
    public int damage, maxHitpoint, maxMovePoint;
    public int range;
    #endregion

    public UnitStatus status { get; private set; }

    private void Awake()
    {
        status = new UnitStatus(unitName, maxHitpoint, maxMovePoint, damage, range);
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
