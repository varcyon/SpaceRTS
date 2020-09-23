using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Structure : Unit
{
    [SerializeField] protected List<GameObject> buildQUE = new List<GameObject>();
    [SerializeField] protected float buildTime;
    [SerializeField] protected float buildTimer;

    [SerializeField] protected bool buildInProgress;

    [SerializeField] protected Dictionary<string, float> buildTimes = new Dictionary<string, float>();
    [SerializeField] protected Dictionary<string, List<Element>> resourcesNeeded = new Dictionary<string, List<Element>>();
    [SerializeField] public List<Transform> buildDoneSpawn = new List<Transform>();
    [SerializeField] private LayerMask layerMask;

    /// For Structure Placement
    [SerializeField] public bool isConstructing = false;
    [SerializeField] public bool active = false;
    [SerializeField] public float constructionTime;
    [SerializeField] protected float constructionTimer;

    [SerializeField] public Collider constructingArea;

    [SerializeField] public Shader constructionGhost;
    [SerializeField] public Shader dissolve;
    [SerializeField] public Shader normalMat;
    [SerializeField] private bool spawnDissolve;
    [SerializeField] private float dissolveAmount;

    [SerializeField] TextMeshProUGUI buildProgress;
    [SerializeField] TextMeshProUGUI whatIsBeingProduced;

    void Start()
    {

        buildTimer = 0;
        CurrentHullAmount = HullAmount;
        CurrentShieldAmount = ShieldAmount;
        canMove = false;

    }
    private void Update()
    {
        Constructing();
        if (!active)
            return;
        Building();
    }

    void Constructing()
    {
        if (isConstructing)
        {
            constructionTimer += Time.deltaTime;
            float percent = constructionTimer / constructionTime;
            buildProgress.text = (Mathf.Clamp(percent, 0, 1) * 100).ToString("#");
        }
        if (constructionTimer >= constructionTime && isConstructing)
        {
            PV.RPC("RPC_ChangeToDissolve", RpcTarget.All);
            spawnDissolve = true;
        }

        if (spawnDissolve)
        {
            dissolveAmount += Time.deltaTime;
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_DissolveTime", dissolveAmount);
            if (dissolveAmount >= 1)
            {
                isConstructing = false;
                spawnDissolve = false;
                PV.RPC("RPC_ChangeToNormal", RpcTarget.All);

                active = true;
            }
            buildProgress.gameObject.SetActive(false);
        }


    }

    [PunRPC]
    public void RPC_ChangeToNormal()
    {

        GetComponentInChildren<MeshRenderer>().material.shader = normalMat;
    }

    [PunRPC]
    public void RPC_ChangeToDissolve()
    {
        GetComponentInChildren<MeshRenderer>().material.shader = dissolve;

    }


    //adds unit to the build queque
    public void AddBuildQue(GameObject unit)
    {
        bool canBuild = false;
        foreach (Element element in resourcesNeeded[unit.name])
        {
            Element e = PlayerResources.r.resources.Find(x => x.eName == element.eName);
            if (e.amount >= element.amount)
            {
                Debug.Log("enough");
                canBuild = true;
            }
            else
            {
                Debug.Log("Not Enough");
                canBuild = false;
                StartCoroutine(NotEnoughFade());
                break;
            }
        }
        if (canBuild)
        {
            foreach (Element element in resourcesNeeded[unit.name])
            {
                Element e = PlayerResources.r.resources.Find(x => x.eName == element.eName);
                e.amount -= element.amount;
            }

            buildQUE.Add(unit);
        }
    }
    // checks if there is anything in the queque, if there is , count down 

    public IEnumerator NotEnoughFade()
    {
        GameUI.ui.notEnoughResources.gameObject.SetActive(true);
        GameUI.ui.notEnoughError.PlayOneShot(GameUI.ui.notEnoughError.clip);
        yield return new WaitForSeconds(1f);
        GameUI.ui.notEnoughResources.gameObject.SetActive(false);



    }
    protected void Building()
    {
        Debug.Log("in building");
        if (buildInProgress)
        {
            buildProgress.gameObject.SetActive(true);
            whatIsBeingProduced.gameObject.SetActive(true);

            buildTimer += Time.deltaTime;

            whatIsBeingProduced.text = buildQUE[0].name.ToString();
            float percent = buildTimer / buildTime;
            buildProgress.text = (Mathf.Clamp(percent, 0, 1) * 100).ToString("#");

        }
        else
        {
            buildProgress.gameObject.SetActive(false);
            whatIsBeingProduced.gameObject.SetActive(false);

        }

        if (buildQUE.Count > 0 && !buildInProgress)
        {
            buildTime = buildTimes[buildQUE[0].name.ToString()];
            buildInProgress = true;
        }

        if (buildQUE.Count > 0 && buildTimer >= buildTime)
        {
            BuildComplete(buildQUE[0]);
            buildInProgress = false;
            buildTimer = 0;
            buildQUE.RemoveAt(0);
        }
    }
    // instantiates unit after building
    protected void BuildComplete(GameObject buit)
    {
        foreach (Transform transform in buildDoneSpawn)
        {
            Collider[] c = Physics.OverlapSphere(transform.position, 0.75f, layerMask);
            if (c.Length > 0)
            {
                Debug.Log("Occupied by: " + c[0].name);
            }
            else
            {
                GameObject go = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", buit.name.ToString()), transform.position, Quaternion.identity);
                if (go.GetComponent<MiningShip>())
                {
                    go.GetComponent<MiningShip>().commandShip = this.gameObject;
                }
                UnitManager.UM.selectables.Add(go);
                break;
            }
        }
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isConstructing);
            stream.SendNext(spawnDissolve);
        }
        else
        {
            isConstructing = (bool)stream.ReceiveNext();
            spawnDissolve = (bool)stream.ReceiveNext();
        }
    }


}
