using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Librer�a de inteligencia artificial.
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
        //Preguntamos si la distancia est� dentro del radio de acci�n del enemigo.
        if(Vector3.Distance(player.position, this.transform.position) < 12)
        {
            //Le decimos al enemigo que siga al player.
            nav.SetDestination(player.position);
            //Activo la animaci�n de pasear.
            anim.SetBool("isWalking", true);
            //Desactivo la de esperar.
            anim.SetBool("isIdle", false);
        }
        else //Cuando tu te separas
        {
            //Le decimos al enemigo que se quede donde est�.
            nav.SetDestination(this.transform.position);
            //Activo la animaci�n de pasear.
            anim.SetBool("isWalking", false);
            //Desactivo la de esperar.
            anim.SetBool("isIdle", true);
        }
        //Cuando te mueres.
        if (GameManager.instance.GameOver)
        {
            //Le decimos al enemigo que se quede donde est�.
            nav.SetDestination(this.transform.position);
            //Activo la animaci�n de pasear.
            anim.SetBool("isWalking", false);
            //Desactivo la de esperar.
            anim.SetBool("isIdle", true);
        }
    }
}
