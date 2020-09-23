using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidResource : Resource
{

    
    void Start()
    {
        elements.Add(new Element(ElementClass.Transition, "Iron",     800000000));
        elements.Add(new Element(ElementClass.Transition, "Nickel",    50000000));
        elements.Add(new Element(ElementClass.Transition, "Platnium",  50000000));
        elements.Add(new Element(ElementClass.Transition, "Gold",      50000000));
        elements.Add(new Element(ElementClass.Transition, "Rhodium",   50000000));

        foreach (Element e in elements)
        {
            totalAmount += e.amount;
        }


    }


}
