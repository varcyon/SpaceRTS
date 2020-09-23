using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;
using UnityEngine.VFX;

public enum UnitType { 
    NULL,
    CommandShip,
    Attacker,
    MiningShip,
    ConstructionShip,
    ShipYard,
    Cannon
}

[RequireComponent(typeof(NavMeshAgent), typeof(PhotonView))]
public abstract class Unit : MonoBehaviourPunCallbacks, ISelectable  ,IPunObservable
{
    [SerializeField] protected UnitType unitType;
    [SerializeField] protected bool isSelectable = true;
    [SerializeField] protected bool selected;
    [SerializeField] protected bool canMove;
    [SerializeField] protected GameObject targetObject;

    [SerializeField] private int shieldAmount;
    [SerializeField] private int currentShieldAmount;
    [SerializeField] private int hullAmount;
    [SerializeField] private int currentHullAmount;
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    [SerializeField] private int armor;
    [SerializeField] private int sheildRechargeRate;

    [SerializeField] public GameObject selectionCircle;
    [SerializeField] protected GameObject UI;
   [SerializeField] public int team;

    [SerializeField] GameObject deathEffect;

    public PhotonView PV;
    public NavMeshAgent agent;

    [SerializeField] protected AudioSource unitActionSound;
    [SerializeField] protected AudioSource beamSound;


    public int ShieldAmount { get => shieldAmount; set => shieldAmount = value; }
    public int CurrentShieldAmount { get => currentShieldAmount; set => currentShieldAmount = value; }
    public int HullAmount { get => hullAmount; set => hullAmount = value; }
    public int CurrentHullAmount { get => currentHullAmount; set => currentHullAmount = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Speed { get => speed; set => speed = value; }
    public int SheildRechargeRate { get => sheildRechargeRate; set => sheildRechargeRate = value; }
    public int Armor { get => armor; set => armor = value; }

    public UnitType GetUnitType()
    {
        return unitType;
    }
    public void Select()
    {
        if (isSelectable)
        {
            selected = true;
            selectionCircle.SetActive(true);
            unitActionSound.Play();
            ShowUI();
        }
    }

    public void DeSelect()
    {
        if (isSelectable && selected)
        {
            selected = false;
            selectionCircle.SetActive(false);
            HideUI();

        }
    }
    public void ShowUI()
    {
        if (!UI.activeInHierarchy)
        {
            UI.SetActive(true);
        }
    }
    public void HideUI()
    {
        if (UI.activeInHierarchy && !selected)
        {

            UI.SetActive(false);
        }
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool b)
    {
        canMove = b;
    }

    public void TakeDamage(int damge)
    {
        if(CurrentShieldAmount <= 0)
        {
            if(CurrentShieldAmount < 0)
            {
                CurrentShieldAmount = 0;
            }

            CurrentHullAmount -= damge;
        } else
        {
            CurrentShieldAmount -= damge;
        }

        if(CurrentHullAmount <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        deathEffect.gameObject.SetActive(true);
        deathEffect.gameObject.transform.parent = null;
        GetComponent<LootDrops>().DropLoot();
        UnitManager.UM.selectables.Remove(gameObject);
        if (UnitManager.UM.selectedStructures.Contains(gameObject))
        {
            UnitManager.UM.selectedStructures.Remove(gameObject);
        }
        PhotonNetwork.Destroy(gameObject);
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(x);
        }
        else
        {
            //x = (int)stream.ReceiveNext();
        }
    }
}
