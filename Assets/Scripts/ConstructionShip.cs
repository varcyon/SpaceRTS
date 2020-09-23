using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class ConstructionShip : Ship
{
    [SerializeField] public GameObject shipYard;
    [SerializeField] public GameObject cannon;

    private GameObject constructioning;

    [SerializeField] GameObject currentPlacingStructure;
    [SerializeField] MeshRenderer structureMat;
    [SerializeField] private bool amIConstructing;
    [SerializeField] private bool placed;
    [SerializeField] private float constructionDist;
    GameObject placedObject;

    [SerializeField] protected Dictionary<string, float> constructionTimes = new Dictionary<string, float>();
    [SerializeField] protected Dictionary<string, List<Element>> resourcesNeeded = new Dictionary<string, List<Element>>();

    void Start()
    {
        team = PV.OwnerActorNr;
        constructionTimes.Add("ShipYard", 5f);
        resourcesNeeded.Add("ShipYard", new List<Element>() { new Element(ElementClass.Transition, "Iron", 100), new Element(ElementClass.Transition, "Platnium", 5) });
        constructionTimes.Add("Cannon", 5f);
        resourcesNeeded.Add("Cannon", new List<Element>() { new Element(ElementClass.Transition, "Iron", 50), new Element(ElementClass.Transition, "Gold", 5) });
        CurrentHullAmount = HullAmount;
        CurrentShieldAmount = ShieldAmount;
        foreach (GameObject beam in beams)
        {
            beam.SetActive(false);

        }
        selectionCircle.transform.localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        PlaceConstruction();
    }

    private void PlaceConstruction()
    {
        if (currentPlacingStructure != null)
        {
            MovePlaceableToMouse();
            if (Input.GetMouseButtonDown(0))
            {
                //directs ship to place location

                Vector3 dir = (transform.position - currentPlacingStructure.transform.position).normalized;
                Vector3 finalPos = currentPlacingStructure.transform.position + dir * constructionDist;
                agent.SetDestination(finalPos);

                UnitManager.UM.isPlaceingBuilding = false;
                placed = true;
                Vector3 placePos = currentPlacingStructure.transform.position;
                // destoryTemp object
                Destroy(currentPlacingStructure);
                //create placed object
                placedObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", constructioning.name.ToString()), placePos, Quaternion.identity);
                //sets the contruction time 
                placedObject.GetComponent<Structure>().constructionTime = constructionTimes[constructioning.name.ToString()];
                placedObject.GetComponent<Unit>().selectionCircle.SetActive(false);
                this.Select();
                //adds to selectables ( for multiselect
                UnitManager.UM.selectables.Add(placedObject);
            }
        }
    }

    //if you dont have enough resources
    public IEnumerator NotEnoughError()
    {
        GameUI.ui.notEnoughResources.gameObject.SetActive(true);
        GameUI.ui.notEnoughError.PlayOneShot(GameUI.ui.notEnoughError.clip);
        yield return new WaitForSeconds(1f);
        GameUI.ui.notEnoughResources.gameObject.SetActive(false);

    }

    public void ContructShipYard()
    {
        bool canBuild = false;
        foreach (Element element in resourcesNeeded[shipYard.name])
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
                StartCoroutine(NotEnoughError());
                break;
            }
        }
        if (canBuild)
        {
            foreach (Element element in resourcesNeeded[shipYard.name])
            {
                Element e = PlayerResources.r.resources.Find(x => x.eName == element.eName);
                e.amount -= element.amount;
            }

            if (currentPlacingStructure == null)
            {
                currentPlacingStructure = Instantiate(shipYard);

                constructioning = shipYard;
                placed = false;
                UnitManager.UM.isPlaceingBuilding = true;
                currentPlacingStructure.layer = 2;
                currentPlacingStructure.GetComponentInChildren<MeshRenderer>().material.shader = currentPlacingStructure.GetComponent<Structure>().constructionGhost;
                currentPlacingStructure.GetComponentInChildren<Collider>().enabled = false;
                currentPlacingStructure.GetComponentInChildren<NavMeshAgent>().enabled = false;

            }
            else
            {
                Destroy(currentPlacingStructure);
            }
        }
    }
    public void ContructCannon()
    {

        bool canBuild = false;
        foreach (Element element in resourcesNeeded[cannon.name])
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
                StartCoroutine(NotEnoughError());
                break;
            }
        }
        if (canBuild)
        {
            foreach (Element element in resourcesNeeded[cannon.name])
            {
                Element e = PlayerResources.r.resources.Find(x => x.eName == element.eName);
                e.amount -= element.amount;
            }


            if (currentPlacingStructure == null)
            {
                currentPlacingStructure = Instantiate(cannon);
                constructioning = cannon;
                placed = false;
                UnitManager.UM.isPlaceingBuilding = true;
                currentPlacingStructure.layer = 2;
                currentPlacingStructure.GetComponentInChildren<MeshRenderer>().material.shader = currentPlacingStructure.GetComponent<Structure>().constructionGhost;
                currentPlacingStructure.GetComponentInChildren<Collider>().enabled = false;
                currentPlacingStructure.GetComponentInChildren<NavMeshAgent>().enabled = false;
            }
            else
            {
                Destroy(currentPlacingStructure);
            }
        }
    }

    //moves construction to mouse to be placed
    void MovePlaceableToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {
            currentPlacingStructure.transform.position = hitinfo.point;
        }
    }


    //sets to constructing while in range
    private void OnTriggerStay(Collider other)
    {
        if (!other.GetComponentInParent<Structure>().active && Vector3.Distance(transform.position, other.transform.position) < constructionDist && placed)
        {
            other.GetComponentInParent<Structure>().isConstructing = true;
            amIConstructing = true;

        }
        else
        {
            amIConstructing = false;
        }
        StartCoroutine(AmIConstructing(other));

    }
    public IEnumerator AmIConstructing(Collider other)
    {
        if (amIConstructing)
        {
            foreach (GameObject beam in beams)
            {
                beam.SetActive(true);
                if (!beamSound.isPlaying)
                {
                    beamSound.Play();
                }

                Vector3 start = transform.InverseTransformPoint(beam.transform.position);
                start = new Vector3(start.x, 0, start.z);
                Vector3 stop = transform.InverseTransformPoint(other.transform.position);
                stop = new Vector3(stop.x, 0, stop.z);
                beam.GetComponent<LineRenderer>().SetPosition(0, start);
                beam.GetComponent<LineRenderer>().SetPosition(1, stop);
            }
        }
        else
        {
            foreach (GameObject beam in beams)
            {
                beam.SetActive(false);
                beamSound.Stop();

            }
        }
        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("left construction Area");
        foreach (GameObject beam in beams)
        {
            beam.SetActive(false);
            beamSound.Stop();

        }

        other.GetComponentInParent<Structure>().isConstructing = false;
        amIConstructing = false;


    }
    public void Stop()
    {
        agent.SetDestination(transform.position);
    }

    //for Multiplayer
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(amIConstructing);
        }
        else
        {
            amIConstructing = (bool)stream.ReceiveNext();
        }
    }
}
