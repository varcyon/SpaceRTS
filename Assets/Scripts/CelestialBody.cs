using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CelestialType
{
    Star,Planet
}
public abstract class CelestialBody : MonoBehaviour {
    [SerializeField] protected bool isSelectable = true;
    [SerializeField] protected bool selected;
    [SerializeField] protected GameObject selectionCircle;
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] public string designation;
    [SerializeField]protected float diameter;
    [SerializeField] protected float elementAmount;

    private List<string> letters = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "L", "M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
    private List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

    [SerializeField] protected List<Element> elements = new List<Element>();
    [SerializeField] protected GameObject dataDisplay;
    [SerializeField] protected GameObject elementListing;
    [SerializeField] private List<GameObject> listedElements = new List<GameObject>();
    [SerializeField] protected CelestialType celestialType;



    private void AddElements()
    {
        elements.Add(new Element(ElementClass.PoorMetal, "Aluminum", 0));
        elements.Add(new Element(ElementClass.PoorMetal, "Gallium", 0));
        elements.Add(new Element(ElementClass.PoorMetal, "Indium", 0));
        elements.Add(new Element(ElementClass.PoorMetal, "Thallium", 0));
        elements.Add(new Element(ElementClass.PoorMetal, "Tin", 0));
        elements.Add(new Element(ElementClass.PoorMetal, "Lead", 0));
        elements.Add(new Element(ElementClass.PoorMetal, "Bismuth", 0));

        elements.Add(new Element(ElementClass.NonMetal, "Carbon", 0));
        elements.Add(new Element(ElementClass.NonMetal, "Phosphorus", 0));
        elements.Add(new Element(ElementClass.NonMetal, "Sulfur", 0));
        elements.Add(new Element(ElementClass.NonMetal, "Selenium", 0));

        elements.Add(new Element(ElementClass.Metalloid, "Boron", 0));
        elements.Add(new Element(ElementClass.Metalloid, "Silicon", 0));
        elements.Add(new Element(ElementClass.Metalloid, "Germanium", 0));
        elements.Add(new Element(ElementClass.Metalloid, "Arsenic", 0));
        elements.Add(new Element(ElementClass.Metalloid, "Antimony", 0));
        elements.Add(new Element(ElementClass.Metalloid, "Tellurium", 0));
        elements.Add(new Element(ElementClass.Metalloid, "Polonium", 0));
        elements.Add(new Element(ElementClass.Metalloid, "Astatine", 0));

        elements.Add(new Element(ElementClass.AlakiMetal, "Lithium", 0));
        elements.Add(new Element(ElementClass.AlakiMetal, "Sodium", 0));
        elements.Add(new Element(ElementClass.AlakiMetal, "Potassium", 0));
        elements.Add(new Element(ElementClass.AlakiMetal, "Rubidium", 0));
        elements.Add(new Element(ElementClass.AlakiMetal, "Cesium", 0));
        elements.Add(new Element(ElementClass.AlakiMetal, "Francium", 0));

        elements.Add(new Element(ElementClass.AlkalineEarth, "Beryllium", 0));
        elements.Add(new Element(ElementClass.AlkalineEarth, "Magnesium", 0));
        elements.Add(new Element(ElementClass.AlkalineEarth, "Calcium", 0));
        elements.Add(new Element(ElementClass.AlkalineEarth, "Strontium", 0));
        elements.Add(new Element(ElementClass.AlkalineEarth, "Barium", 0));
        elements.Add(new Element(ElementClass.AlkalineEarth, "Radium", 0));

        elements.Add(new Element(ElementClass.Transition, "Scandium", 0));
        elements.Add(new Element(ElementClass.Transition, "Yttrium", 0));
        elements.Add(new Element(ElementClass.Transition, "Titanium", 0));
        elements.Add(new Element(ElementClass.Transition, "Zirconium", 0));
        elements.Add(new Element(ElementClass.Transition, "Hafnium", 0));
        elements.Add(new Element(ElementClass.Transition, "Vanadium", 0));
        elements.Add(new Element(ElementClass.Transition, "Niobium", 0));
        elements.Add(new Element(ElementClass.Transition, "Tantalum", 0));
        elements.Add(new Element(ElementClass.Transition, "Chromium", 0));
        elements.Add(new Element(ElementClass.Transition, "Molybdenum", 0));
        elements.Add(new Element(ElementClass.Transition, "Tungsten", 0));
        elements.Add(new Element(ElementClass.Transition, "Manganese", 0));
        elements.Add(new Element(ElementClass.Transition, "Rhenium", 0));
        elements.Add(new Element(ElementClass.Transition, "Iron", 0));
        elements.Add(new Element(ElementClass.Transition, "Ruthenium", 0));
        elements.Add(new Element(ElementClass.Transition, "Osmium", 0));
        elements.Add(new Element(ElementClass.Transition, "Cobalt", 0));
        elements.Add(new Element(ElementClass.Transition, "Rhodium", 0));
        elements.Add(new Element(ElementClass.Transition, "Iridium", 0));
        elements.Add(new Element(ElementClass.Transition, "Nickel", 0));
        elements.Add(new Element(ElementClass.Transition, "Palladium", 0));
        elements.Add(new Element(ElementClass.Transition, "Platinum", 0));
        elements.Add(new Element(ElementClass.Transition, "Copper", 0));
        elements.Add(new Element(ElementClass.Transition, "Silver", 0));
        elements.Add(new Element(ElementClass.Transition, "Gold", 0));
        elements.Add(new Element(ElementClass.Transition, "Zinc", 0));
        elements.Add(new Element(ElementClass.Transition, "Cadmium", 0));

        elements.Add(new Element(ElementClass.RareEarth, "Lanthanum", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Cerium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Praseodymium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Neodymium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Samarium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Europium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Gadolinium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Terbium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Dysprosium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Holmium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Erbium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Thulium", 0));
        elements.Add(new Element(ElementClass.RareEarth, "Lutetium", 0));

        elements.Add(new Element(ElementClass.Radioactive, "Actinium", 0));
        elements.Add(new Element(ElementClass.Radioactive, "Thorium", 0));
        elements.Add(new Element(ElementClass.Radioactive, "Protactinium", 0));
        elements.Add(new Element(ElementClass.Radioactive, "Uranium", 0));

        elements.Add(new Element(ElementClass.Liquid, "Mercury", 0));
        elements.Add(new Element(ElementClass.Liquid, "Bromine", 0));
        elements.Add(new Element(ElementClass.Liquid, "Water", 0));
        elements.Add(new Element(ElementClass.Liquid, "IceWater", 0));

        elements.Add(new Element(ElementClass.Gas, "Nitrogen", 0));
        elements.Add(new Element(ElementClass.Gas, "Oxygen", 0));
        elements.Add(new Element(ElementClass.Gas, "Fluorine", 0));
        elements.Add(new Element(ElementClass.Gas, "Chlorine", 0));
        elements.Add(new Element(ElementClass.Gas, "Helium", 0));
        elements.Add(new Element(ElementClass.Gas, "Neon", 0));
        elements.Add(new Element(ElementClass.Gas, "Argon", 0));
        elements.Add(new Element(ElementClass.Gas, "Krypton", 0));
        elements.Add(new Element(ElementClass.Gas, "Xenon", 0));
        elements.Add(new Element(ElementClass.Gas, "Radon", 0));
        elements.Add(new Element(ElementClass.Gas, "Hydrogen", 0));

    }

 

    /*
    public virtual void Generate()
    {
        dataDisplay.GetComponent<RectTransform>().position = new Vector3(-800f, -800f, 0f);
        dataDisplay.SetActive(true);
        AddElements();
        if(GetComponent<Star>())
          designation = letters[Random.Range(0, letters.Count)]+ 
                      letters[Random.Range(0, letters.Count)]+ 
                      letters[Random.Range(0, letters.Count)]+ 
                        
                      numbers[Random.Range(0, numbers.Count)]+ 
                      numbers[Random.Range(0, numbers.Count)]+ 
                      numbers[Random.Range(0, numbers.Count)];
    }
    */
    public float GetDiameter()
    {
        return diameter;
    }

    public void Select()
    {
        if (isSelectable)
        {
            selected = true;
            selectionCircle.SetActive(true);
        }
    }

    public void DeSelect()
    {
        if (isSelectable && selected)
        {
            selected = false;
            selectionCircle.SetActive(false);

        }
    }
/*
    public void OnMouseEnter()
    {
        if (!dataDisplay.activeInHierarchy && !CameraControl.focused)
        {
            dataDisplay.SetActive(true);
            dataDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = designation;
            dataDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = celestialType.ToString();
            if (gameObject.GetComponent<Star>())
            {
                Star s = gameObject.GetComponent<Star>();
                if(s != null)
                {
                    dataDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Type " + s.starClass;
                    dataDisplay.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Tempature: ";
                    dataDisplay.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = s.tempature + " K";
                    foreach (Element element in elements)
                    {
                        if(element.amount > elementAmount * 0.20f)
                        {
                           GameObject EL = Instantiate(elementListing, dataDisplay.transform.GetChild(5).GetChild(0));
                           EL.GetComponent<Initialize>().eToolTipListingName.text = element.eName;
                           listedElements.Add(EL);
                        }
                    }
                }
            }
            else if (gameObject.GetComponent<PlanetaryBody>())
            {
                PlanetaryBody p = gameObject.GetComponent<PlanetaryBody>();
                if(p != null)
                {
                    dataDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Type " + p.planetClass;
                    dataDisplay.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Atmosphere: ";
                    dataDisplay.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = p.atmosphere.ToString();
                }
            }

        }
    }
    public void OnMouseOver()
    {
        if (dataDisplay.activeInHierarchy)
        {
            dataDisplay.transform.position = Input.mousePosition;
        }
    }

    private void OnMouseExit()
    {
        if (dataDisplay.activeInHierarchy)
        {
            foreach (GameObject g in listedElements)
            {
                Destroy(g);
            }
            listedElements.Clear();
            dataDisplay.SetActive(false);
        }
    }
    */
}