using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSpawnSafe : MonoBehaviour
{
    public bool occupided;
    public string oName;
    private void OnTriggerStay(Collider other)
    {
        occupided = true;
        oName = other.name;
    }

    private void OnTriggerExit(Collider other)
    {
        occupided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        occupided = true;
        oName = other.name;
    }
}
