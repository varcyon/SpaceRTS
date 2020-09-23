using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ElementClass
{
    PoorMetal,
    NonMetal,
    Metalloid,
    AlakiMetal,
    AlkalineEarth,
    Transition,
    RareEarth,
    Radioactive,
    Liquid,
    Gas
}

[System.Serializable]
public class Element { 
    public string eName;
    public int amount;
    public ElementClass elementClass;

    public Element(ElementClass e, string n, int a )
    {
        elementClass = e;
        eName = n;
        amount = a;
    }

}
