using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UnitManager : MonoBehaviourPunCallbacks
{
    public static UnitManager UM;
    public List<GameObject> selectedStructures = new List<GameObject>();
    [SerializeField] public List<GameObject> selectables = new List<GameObject>();
    [SerializeField] public bool isPlaceingBuilding;
    private void Start()
    {
        if(UM == null)
        {
            UM = this;
        } 
        else if ( UM != this)
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        if(selectedStructures.Count != 0)
        {
            selectedStructures[0].GetComponent<Unit>().ShowUI();
        } 
    }
}
