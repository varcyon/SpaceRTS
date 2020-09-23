using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class SelectionButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject unit;
    public TextMeshProUGUI unitName;
    void Start()
    {
        unitName.text = unit.name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl) && eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("de-selecting " + unit.name);
            unit.GetComponent<ISelectable>().DeSelect();
            UnitManager.UM.selectedStructures.Remove(unit);
            return;

        }


        if (eventData.button == PointerEventData.InputButton.Left)
        {
            foreach (GameObject unit in UnitManager.UM.selectedStructures)
            {
                unit.GetComponent<ISelectable>().DeSelect();
            }
            UnitManager.UM.selectedStructures.Clear();
            unit.GetComponent<ISelectable>().Select();
            UnitManager.UM.selectedStructures.Add(unit);    
        }

      if(eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("clicked with right click");
            CameraControl.i.FocusPos(unit.transform.position);
        }
    }
}
