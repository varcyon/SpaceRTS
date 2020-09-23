using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleSelectionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitName;

    [SerializeField] private TextMeshProUGUI curShield;
    [SerializeField] private TextMeshProUGUI maxShield;

    [SerializeField] private TextMeshProUGUI curHull;
    [SerializeField] private TextMeshProUGUI maxHull;

    [SerializeField] private Image unitImage;

    [SerializeField] private TextMeshProUGUI unitDamage;
    [SerializeField] private TextMeshProUGUI unitDefense;
    [SerializeField] private TextMeshProUGUI unitSpeed;
    [SerializeField] private TextMeshProUGUI unitSheildRegen;
    // Update is called once per frame
    void Update()
    {
        if(UnitManager.UM.selectedStructures[0] != null)
        {
            unitName.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().GetUnitType().ToString();
            curShield.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().CurrentShieldAmount.ToString();
            maxShield.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().ShieldAmount.ToString();
            curHull.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().CurrentHullAmount.ToString();
            maxHull.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().HullAmount.ToString();
            unitImage.sprite = Resources.Load<Sprite>("Sprites/"+unitName.text+"");
            unitDamage.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().Damage.ToString();
            unitDefense.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().Armor.ToString();
            unitSpeed.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().Speed.ToString();
            unitSheildRegen.text = UnitManager.UM.selectedStructures[0].GetComponent<Unit>().SheildRechargeRate.ToString();
        }
    }
}
