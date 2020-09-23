using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerPrefab : MonoBehaviour
{
    public GameObject CommandShip;
    public PhotonView PV;
    public GameObject ship;
    public GameObject cam;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        
    }
    void Start()
    {
        if (PV.IsMine)
        {
           SpawnShip(); 
        }
    }

    void Update()
    {

    }

    public void SpawnShip()
    {
        
        ship =  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", CommandShip.name.ToString()), GameSetupController.startPosistions[PV.OwnerActorNr -1].transform.position, Quaternion.identity);
        ship.GetComponent<Structure>().isConstructing = false;
        ship.GetComponent<Structure>().active = true;
        ship.GetComponentInChildren<MeshRenderer>().material.shader = GetComponent<Structure>().normalMat;
        Persistant.team = PV.OwnerActorNr;
        UnitManager.UM.selectables.Add(ship);
    }

}
