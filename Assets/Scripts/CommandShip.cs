using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class CommandShip : Structure
{
    [SerializeField] public  GameObject constructionShip;
    [SerializeField] public GameObject miningShip;




    void Start()
    {
        PV = GetComponent<PhotonView>();
        // dictionary of build times for the ships
        buildTimes.Add("ConstructionShip", 10f);
        buildTimes.Add("MiningShip", 5f);
        resourcesNeeded.Add("ConstructionShip", new List<Element>() { new Element(ElementClass.Transition, "Iron", 75) });
        resourcesNeeded.Add("MiningShip", new List<Element>() { new Element(ElementClass.Transition, "Iron", 50) });
        team = PV.OwnerActorNr;

        if (PV.IsMine)
        {
        //initial unit spawns
        BuildComplete(miningShip);
        BuildComplete(miningShip);
        BuildComplete(constructionShip);

        }


        CurrentHullAmount = HullAmount;
        CurrentShieldAmount = ShieldAmount;
    }


}
