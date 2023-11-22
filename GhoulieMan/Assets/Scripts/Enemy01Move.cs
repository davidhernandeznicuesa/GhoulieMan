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
    //Variable para acceder a la clase enemy01Health
    private Enemy01Health enemyHealth;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        //Cargamos la clase
        enemyHealth = GetComponent<Enemy01Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //Preguntamos si la distancia est� dentro del radio de acci�n del enemigo.
        if(Vector3.Distance(player.position, this.transform.position) < 12)
        {
            //Preguntamos si est� vivo.
            if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
            {
                //Le decimos al enemigo que siga al player.
                nav.SetDestination(player.position);
                //Activo la animaci�n de pasear.
                anim.SetBool("isWalking", true);
                //Desactivo la de esperar.
                anim.SetBool("isIdle", false);
            }else if (GameManager.instance.GameOver || !enemyHealth.IsAlive)
            {
                //Activo la animaci�n de pasear.
                anim.SetBool("isWalking", false);
                //Desactivo la de esperar.
                anim.SetBool("isIdle", true);
                //Desactivo el camino navMesh
                nav.enabled = false;
            }
        }
        ////else //Cuando tu te separas
        ////{
        ////    //Le decimos al enemigo que se quede donde est�.
        ////    nav.SetDestination(this.transform.position);
        ////    //Activo la animaci�n de pasear.
        ////    anim.SetBool("isWalking", false);
        ////    //Desactivo la de esperar.
        ////    anim.SetBool("isIdle", true);
        ////}
        ////Cuando te mueres.
        //if (GameManager.instance.GameOver)
        //{
        //    //Le decimos al enemigo que se quede donde est�.
        //    nav.SetDestination(this.transform.position);
        //    //Activo la animaci�n de pasear.
        //    //anim.SetBool("isWalking", false);
        //    //Desactivo la de esperar.
        //    //anim.SetBool("isIdle", true);
        //}
    }
}
