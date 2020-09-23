using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
   [SerializeField] private bool doNotDestroyOnLoad = true;
    void Start()
    {
        if(doNotDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    
}
