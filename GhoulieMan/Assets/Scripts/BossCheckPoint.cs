using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPoint : MonoBehaviour
{
    //Variable para coger el collider
    public BoxCollider collider;
    //Variable llamamos al BossController
    private BossController bossController;
    //Variable para coger el movimiento del player
    private CharacterMovement characterMovement;
    //Variable para cargar animación del player.
    private Animator playerAnimator;
    void Start()
    {
        //Cogemos el collider
        collider = GetComponent<BoxCollider>();
        //Cargamos el bossController
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        //Cargamos el movimiento del player
        characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        //Cargar el animator del player
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {    }
    private void OnTriggerExit(Collider other)
    {
        //Si es el jugador
        if (other.tag =="Player")
        {
            //Poner el istrigger a falso para que no se pueda activar.
            collider.isTrigger = false;
            //Despertamos al Boss.
            bossController.bossAwake = true;
            //desactivamos el movimiento del player.
            characterMovement.enabled = false;
            //Activar la animación del player
            playerAnimator.Play("Player_Idle");
        }
    }
}
