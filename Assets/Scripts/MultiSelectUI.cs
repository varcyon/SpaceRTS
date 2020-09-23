using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectUI : MonoBehaviour
{
    private GridLayoutGroup grid;
    private int children;
    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
    }

    void Update()
    {
        if(this.transform.childCount > 26)
        {
            grid.cellSize = new Vector2(50, 50);
        } else if(transform.childCount > 58)
        {
            grid.cellSize = new Vector2(45, 45);
        }
    }
}
