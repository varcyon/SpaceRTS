using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningShip : Ship
{

    [SerializeField] protected int cargoSpace = 100;
    [SerializeField] int tempCargo;
    [SerializeField] protected bool isMining;
    [SerializeField] protected bool canMine = true;
    [SerializeField] protected float miningTime = 5f;
    [SerializeField] protected float miningTimer;
    [SerializeField] protected int gatheringAmount = 5;
    [SerializeField] protected List<Element> cargo = new List<Element>();
    [SerializeField] protected Transform miningLoc;
    [SerializeField] public GameObject commandShip;

    [SerializeField] private AudioSource unloadCargo;
    [SerializeField] public AudioSource gatherCargo;
    void Start()
    {
        selectionCircle.transform.localScale = transform.localScale;
        team = PV.OwnerActorNr;

        miningTimer = miningTime;
        CurrentHullAmount = HullAmount;
        CurrentShieldAmount = ShieldAmount;
        foreach (GameObject beam in beams)
        {
            beam.SetActive(false);

        }


    }

    void Update()

    {
        if(tempCargo >= cargoSpace) {
            canMine = false;
            agent.SetDestination(commandShip.transform.position);
        }

        if (isMining)
        {
            miningTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

       
        if (other.GetComponent<Resource>())
        {
            Stop();
        }
    }
    

    public void Stop()
    {
        agent.SetDestination(transform.position);
    }

    public void UnloadResources()
    {
        if (!PV.IsMine) return;
       
        Element element;
        foreach (Element e in cargo)
        { if (!unloadCargo.isPlaying)
        {
            unloadCargo.Play();
        }
            switch (e.eName)
            {
                case "Iron":
                    element = PlayerResources.r.resources.Find(x => x.eName == "Iron");
                    element.amount += e.amount;
                    break;

                case "Nickel":
                    element = PlayerResources.r.resources.Find(x => x.eName == "Nickel");
                    element.amount += e.amount;
                    break;

                case "Platnium":
                    element = PlayerResources.r.resources.Find(x => x.eName == "Platnium");
                    element.amount += e.amount;
                    break;

                case "Gold":
                    element = PlayerResources.r.resources.Find(x => x.eName == "Gold");
                    element.amount += e.amount;
                    break;

                case "Rhodium":
                    element = PlayerResources.r.resources.Find(x => x.eName == "Rhodium");
                    element.amount += e.amount;
                    break;
                default:
                    break;
            }
        }
        tempCargo = 0;
        canMine = true;
        cargo.Clear();

    }

    public void BackToResource(Transform t){
        agent.SetDestination(t.position);
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponentInParent<CommandShip>() && Vector3.Distance(transform.position, other.transform.position) < 3)
        {

            Debug.Log("Unload the resources");
            
            UnloadResources();
            if (miningLoc != null)
            {
                BackToResource(miningLoc);
            }
        }
        if (other.GetComponent<Resource>())
        {
            if (canMine)
            {
                isMining = true;
                miningLoc = other.transform;
            }

            if (isMining)
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
                    stop = new Vector3(stop.x, 0,stop.z);
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
            if (miningTimer <= 0)
            {
                tempCargo = 0;
                cargo.Add(other.GetComponent<Resource>().Gather(gatheringAmount));
                gatherCargo.Play();

                foreach (Element e in cargo)
                {
                    tempCargo += e.amount;
                }

                if (tempCargo >= cargoSpace)
                {
                    isMining = false;
                }

                miningTimer = miningTime;
            }
        }
    }
     public  void CollectResourceDrop(Element element)
    {
        if (!PV.IsMine) return;

        cargo.Add(element);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Resource>())
        {
            foreach (GameObject beam in beams)
            {
                beam.SetActive(false);
            }

            isMining = false;
            miningTimer = miningTime;
        }

    }
}
