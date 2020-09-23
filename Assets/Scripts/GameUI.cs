using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI ui;
    [SerializeField] private List<GameObject> selectedUnitsUI = new List<GameObject>();
    [SerializeField] private List<GameObject> selectedButtonsUI = new List<GameObject>();

    [SerializeField] private GameObject selectedUIButton;
    [SerializeField] private Transform multipleSelectionUI;
    [SerializeField] private Transform singleSelectionUI;

    [SerializeField] public TextMeshProUGUI notEnoughResources;
    [SerializeField] public AudioSource notEnoughError;

    private void Start()
    {
        if (ui == null)
        {
            ui = this;
        }
    }

    // all of this is for the multi select. bugged
    //TODO: fix only 2 are being shown.
        void Update()
    {
        //selectedUnitsUI = UnitManager.UM.selectedStructures;


        if (UnitManager.UM.selectedStructures.Count > 1)
        {
            multipleSelectionUI.gameObject.SetActive(true);
            singleSelectionUI.gameObject.SetActive(false);
        }
        else if(UnitManager.UM.selectedStructures.Count == 1)
        {
            multipleSelectionUI.gameObject.SetActive(false);
            singleSelectionUI.gameObject.SetActive(true);
        } else
        {
            multipleSelectionUI.gameObject.SetActive(false);
            singleSelectionUI.gameObject.SetActive(false);
        }

        if (UnitManager.UM.selectedStructures.Count <= 1)
        {
            ClearAndDestroy();
        }
    }

    public void ClearAndDestroy()
    {
            foreach (GameObject gameObject in selectedButtonsUI)
            {
                Destroy(gameObject);
            }
            selectedButtonsUI.Clear();
    }
    public void PopulateSelection()
    {
        foreach (GameObject unit in UnitManager.UM.selectedStructures)
            {
                if(selectedButtonsUI.Count > 1)
                {
                    foreach (GameObject button in selectedButtonsUI)
                    {
                        if (!selectedButtonsUI.Contains(button))
                        {
                            GameObject UIbutton = Instantiate(selectedUIButton, multipleSelectionUI);
                            UIbutton.GetComponent<SelectionButton>().unit = unit;
                            selectedButtonsUI.Add(UIbutton);

                        }
                    }

                } 
                else {
                
                GameObject UIbutton = Instantiate(selectedUIButton, multipleSelectionUI);
                UIbutton.GetComponent<SelectionButton>().unit = unit;
                selectedButtonsUI.Add(UIbutton);

            }
        }
    }
}
