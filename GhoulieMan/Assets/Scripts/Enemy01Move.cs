using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Librería de inteligencia artificial.
using UnityEngine.AI;

public class Enemy01Move : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, this.transform.position) < 12)
        {
            print("Player demasiado cerca.");
        }
    }
}
