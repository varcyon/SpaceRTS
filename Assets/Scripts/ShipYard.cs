using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipYard : Structure
{
    [SerializeField] public GameObject attacker;
    private void Awake()
    {
        team = PV.OwnerActorNr;
        
    }
    void Start()
    {
        selectionCircle.transform.localScale = transform.localScale;
        active = false;
        isConstructing = false;
        CurrentHullAmount = HullAmount;
        CurrentShieldAmount = ShieldAmount;

        //what the shipyard can build
        buildTimes.Add("Attacker", 5f);
        resourcesNeeded.Add("Attacker", new List<Element>() { new Element(ElementClass.Transition, "Iron", 300), new Element(ElementClass.Transition, "Platnium",5) });

    }



}
