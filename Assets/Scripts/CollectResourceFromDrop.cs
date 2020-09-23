using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Security.Cryptography;

public class CollectResourceFromDrop : MonoBehaviour
{
    [SerializeField] private int amount;
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.GetComponentInParent<MiningShip>())
        {
            Element element = new Element(ElementClass.Transition, "Iron", amount);
            other.GetComponent<MiningShip>().CollectResourceDrop(element);
            other.GetComponent<MiningShip>().gatherCargo.PlayOneShot(other.GetComponent<MiningShip>().gatherCargo.clip);
            PV.RPC("DestroyObject", RpcTarget.All);
        }

    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
