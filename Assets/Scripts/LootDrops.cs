using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class LootDrops : MonoBehaviour
{
    [SerializeField] private List<GameObject> lootDrops = new List<GameObject>();
    [SerializeField] private int numOfDrops;

    public void DropLoot()
    {
        int lootNum = Random.Range(0, lootDrops.Count - 1);
        for (int i = 0; i < numOfDrops; i++)
        {
            PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", lootDrops[lootNum].name.ToString()), transform.position, Quaternion.identity);
        }
    }
}
