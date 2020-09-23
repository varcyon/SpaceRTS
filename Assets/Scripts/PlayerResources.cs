using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources r;
    [SerializeField] public List<Element> resources = new List<Element>();
    int resourceIndex;
    void Start()
    {
        if (r == null)
        {
            r = this;
        }
        else if (r != this)
        {
            Destroy(this);
        }


        resources.Add(new Element(ElementClass.Transition, "Iron", 100));
        resources.Add(new Element(ElementClass.Transition, "Nickel", 50));
        resources.Add(new Element(ElementClass.Transition, "Platnium", 50));
        resources.Add(new Element(ElementClass.Transition, "Gold", 50));
        resources.Add(new Element(ElementClass.Transition, "Rhodium", 50));

    }
}
