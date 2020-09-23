using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum Mode
{
    alert,
    move
}
public class Attacker : Ship, ISelectable, IAttacker
{
    [SerializeField] protected List<Transform> beamPoint = new List<Transform>();
    [SerializeField] protected float scanRadius;
    [SerializeField] protected float attackRange;
    [SerializeField] protected List<GameObject> enemyUnits = new List<GameObject>();
    [SerializeField] protected List<GameObject> scannedObjects = new List<GameObject>();
    [SerializeField] protected LayerMask scanMask;
    [SerializeField] protected bool holdPosition;
    [SerializeField] protected bool canIAttack;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackTimer;
    [SerializeField] protected float beamActiveTime;
    [SerializeField] protected bool beamActivated;

    [SerializeField] protected Mode unitMode;
    protected Vector3 lastPos;
    public void ChangeMode(Mode mode)
    {
        unitMode = mode;
    }
    private void Awake()
    {
        team = PV.OwnerActorNr;
        
    }
    void Start()
    {
        selectionCircle.transform.localScale = transform.localScale;

        CurrentHullAmount = HullAmount;
        CurrentShieldAmount = ShieldAmount;
        foreach (GameObject beam in beams)
        {
            beam.SetActive(false);

        }
        attackTimer = attackRate;
    }
    void Update()
    {//If its not active / contrustucted dont do anything yet this is for the Cannon
        if (GetComponent<Structure>() != null && !GetComponent<Structure>().active) { return; }


        // if we are moving, turn beams off
        if(unitMode == Mode.move)
        {
            foreach (GameObject beam in beams)
            {
                beam.SetActive(false);

            }
        }

        // if we stop moving go into Alert mode
        if (Vector3.Distance(lastPos, transform.position) < .001f)
        {
            unitMode = Mode.alert;
        }
        else
        {
            unitMode = Mode.move;
            lastPos = transform.position;
        }

        FindEnemies();
        FindClosest();
        Chase();
        Beam();

        if (canIAttack)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        Debug.Log("attacking: " + targetObject.name);
        canIAttack = true;
    }

    //Fire beams when able
    public void Beam()
    {
        int beamIndex = 0;
        foreach (Transform point in beamPoint)
        {
            if (unitMode == Mode.alert)
           {
                if (canIAttack && attackTimer <= 0)
                {

                    beams[beamIndex].SetActive(true);
                    Debug.Log(targetObject.name + " damaged for " + Damage + "damage.");
                    if (targetObject != null)
                    {
                        targetObject.GetComponent<Unit>().TakeDamage(Damage);
                    }
                    canIAttack = false;
                    attackTimer = attackRate;
                    StartCoroutine(beamActivation(beamIndex, point));
                }
            }
            beamIndex++;
        }
    }
    //start beams and stop after a time
    IEnumerator beamActivation(int beamIndex, Transform point) {
        beamSound.Play();
        Vector3 start = transform.InverseTransformPoint(point.position);
        Vector3 stop = transform.InverseTransformPoint(targetObject.transform.position);
        stop = new Vector3(stop.x, 2, stop.z);
        beams[beamIndex].GetComponent<LineRenderer>().SetPosition(0, start);
        beams[beamIndex].GetComponent<LineRenderer>().SetPosition(1, stop);
        yield return new WaitForSeconds(beamActiveTime);
        beams[beamIndex].SetActive(false);
        beamSound.Stop();
    }

    // suppose to be go toward the target then attack. I've had problems where i would be able to move them if anything was in range.
    public void Chase()
    {
        if (unitMode == Mode.alert)
        {
            if (targetObject != null)
            {
                transform.LookAt(targetObject.transform.position);

                if (holdPosition)
                {
                    if (Vector3.Distance(this.transform.position, targetObject.transform.position) <= attackRange)
                    {
                        Attack();
                    }
                }
                else
                {
                   // if (Vector3.Distance(this.transform.position, targetObject.transform.position) > attackRange)
                   // {
                   //     Vector3 dir = (transform.position - targetObject.transform.position).normalized;
                   //     Vector3 finalPos = targetObject.transform.position + dir * attackRange;
                   //     agent.SetDestination(finalPos);
                  //  }
                    
                    
                    if (Vector3.Distance(this.transform.position, targetObject.transform.position) <= attackRange)
                    {
                        Attack();
                    }
                }
            }
        }
    }

    //sorts list based on distance
    private void FindClosest()
    {
        if (enemyUnits != null)
        {
            enemyUnits.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position));
            targetObject = enemyUnits[0];
        }
    }
    //finds all enemies / objects
    public void FindEnemies()
    {
        //makes a list for scanned objects
        scannedObjects = new List<GameObject>();
        Collider[] c = Physics.OverlapSphere(transform.position, scanRadius, scanMask);
        // addes the objects into the list for better everything 
        foreach (Collider collider in c)
        {
            if (collider.GetComponent<Unit>() != null && !scannedObjects.Contains(collider.gameObject) && collider.gameObject != this.gameObject)
            {
                scannedObjects.Add(collider.gameObject);
            }
        }

        // checks if the object is no longer in range and removes it
        foreach (GameObject enemy in enemyUnits)
        {
            if (!scannedObjects.Contains(enemy))
            {
                enemyUnits.Remove(enemy);
            }
        }

        // checking for objects in range
        foreach (GameObject scanned in scannedObjects)
        {
            if (scanned.gameObject != this.gameObject)
            {
                if (scanned.gameObject.GetComponentInParent<Unit>().team != this.team)
                {
                    if (!enemyUnits.Contains(scanned.gameObject))
                    {
                        enemyUnits.Add(scanned.gameObject);
                    }
                }
            }
        }
    }


}
