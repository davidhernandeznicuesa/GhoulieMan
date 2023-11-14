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
        //Preguntamos si la distancia está dentro del radio de acción del enemigo.
        if(Vector3.Distance(player.position, this.transform.position) < 6)
        {
            //Le decimos al enemigo que siga al player.
            nav.SetDestination(player.position);
            //Activo la animación de pasear.
            anim.SetBool("isWalking", true);
            //Desactivo la de esperar.
            anim.SetBool("isIdle", false);
        }
        //else
        //{
        //    //Activo la animación de pasear.
        //    anim.SetBool("isWalking", false);
        //    //Desactivo la de esperar.
        //    anim.SetBool("isIdle", true);
        //}
        
    }
}
