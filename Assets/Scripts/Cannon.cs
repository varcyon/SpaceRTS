using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Structure, ISelectable
{
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
    }


}
