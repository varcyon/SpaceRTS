using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class GameMode : MonoBehaviour
{
    public enum Modes
{
    SinglePlayer, Multiplayer
}
    public static GameMode IS;

    public Modes mode;
    private void Awake()
    {
        if(IS == null)
        {
            IS = this;
        }
    }
    public  bool Multiplayer()
    {
        if (mode == GameMode.Modes.Multiplayer )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
