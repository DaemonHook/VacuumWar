/*
 * file: UICanvas.cs
 * author: D.H.
 * feature: UI¿ØÖÆ
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICanvas : MonoBehaviour
{
    public GameObject unitInfoCanvas;
    public TextMeshProUGUI unitStatusName, unitStatusHP, unitStatusDamage, unitStatusRange, unitStatusMP;

    public void ShowUnitStatus(UnitStatus unitStatus)
    {
        Debug.Log(unitStatus.ToString());
        unitStatusName.text = unitStatus.name;
        unitStatusHP.text = unitStatus.hitpoint.ToString();
        unitStatusDamage.text = unitStatus.damage.ToString();
        unitStatusRange.text = unitStatus.range.ToString();
        unitStatusMP.text = unitStatus.movePoint.ToString();
        unitInfoCanvas.SetActive(true);
    }

    public void HideUnitStatus()
    {
        unitInfoCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //ShowUnitStatus(new UnitStatus("»ð·ä", 10086, 1024, 9876, 321));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
