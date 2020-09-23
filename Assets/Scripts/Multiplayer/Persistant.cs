using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Persistant : MonoBehaviour
{
    public static int team;
    public int displayTeam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayTeam = team;
    }
}
