using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossAwake = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossAwake)
        {
            print("Boss is awake");
        }    
    }
}
