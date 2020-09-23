using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    [SerializeField] protected int totalAmount;
    [SerializeField] protected List<Element> elements = new List<Element>();

    //sends element amount to the miner
    public Element Gather(int gatherAmount)
    {
        int i = Random.Range(0, elements.Count);
        Element elementToBeGathered = new Element( elements[i].elementClass, elements[i].eName, elements[i].amount);
        elements[i].amount -= gatherAmount;
        if(elements[i].amount <= 0)
        {
            elements.RemoveAt(i);
        }
        totalAmount -= gatherAmount;
        elementToBeGathered.amount = gatherAmount;
        return elementToBeGathered;
    }

    private void Update()
    {
        if (totalAmount <= 0)
        {
 //           Destroy(gameObject);
        }
    }
    
}
