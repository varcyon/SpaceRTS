using Photon.Pun;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum Commands
{
    Open_Research,
    Open_Build,
    Build_BasicFigher,
    Build_MiningShip,
    Build_ConstructionShip,
    Build_ShipYard,
    Build_Cannon
}

//sets up buttons and icons for all buttons.
public class ButtonCommand : MonoBehaviour
{
    public UnitType unitType;
    public Commands commands;
    public Image image;
    public Dictionary<string, Sprite> icons = new Dictionary<string, Sprite>();
    private PhotonView PV;
    void Awake()
    {
        unitType = GetComponentInParent<Unit>().GetUnitType();

    }
    private void Start()
    {
        PV = GetComponentInParent<PhotonView>();
        SetIcon();
        
    }
    //sets icons
    public void SetIcon()
    {
        switch (commands)
        {
            case Commands.Open_Research:
                break;
            case Commands.Open_Build:
                break;
            case Commands.Build_BasicFigher:
                image.sprite = Resources.Load<Sprite>("Sprites/Attacker");
                break;
            case Commands.Build_MiningShip:
                image.sprite = Resources.Load<Sprite>("Sprites/MiningShip");
                break;
            case Commands.Build_ConstructionShip:
                image.sprite = Resources.Load<Sprite>("Sprites/ConstructionShip");
                break;
            case Commands.Build_ShipYard:
                image.sprite = Resources.Load<Sprite>("Sprites/ShipYard");
                break;
            case Commands.Build_Cannon:
                image.sprite = Resources.Load<Sprite>("Sprites/Cannon");
                break;
            default:
                break;
        }
    }
    //what the button does
    public void OnCOmmand()
    {
        if (!PV.IsMine)
        {
            return;
        }
        switch (commands)
        {
            case Commands.Open_Research:
                break;
            case Commands.Open_Build:
                break;
            case Commands.Build_BasicFigher:
                BuildBasicFighter();
                break;
            case Commands.Build_MiningShip:
                BuildMiningShip();
                break;
            case Commands.Build_ConstructionShip:
                BuildConstructionShip();
                break;
            case Commands.Build_ShipYard:
                ConstructShipYard();
                break;
            case Commands.Build_Cannon:
                ConstructCannon();
                break;
            default:
                break;
        }
    }

    //methods for produciton / construction
    void BuildBasicFighter()
    {
        ShipYard cmd = GetComponentInParent<ShipYard>();
        cmd.AddBuildQue(cmd.attacker);
    }

    void BuildConstructionShip()
    {
        CommandShip cmd = GetComponentInParent<CommandShip>();
        cmd.AddBuildQue(cmd.constructionShip);
    }

    void BuildMiningShip()
    {
        CommandShip cmd = GetComponentInParent<CommandShip>();
        cmd.AddBuildQue(cmd.miningShip);
    }

    void ConstructShipYard()
    {
        ConstructionShip cmd = GetComponentInParent<ConstructionShip>();
        cmd.ContructShipYard();
    }

    void ConstructCannon()
    {
        ConstructionShip cmd = GetComponentInParent<ConstructionShip>();
        cmd.ContructCannon();
    }

}
