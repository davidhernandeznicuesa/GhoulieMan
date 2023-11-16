using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{
    //Empezamos con un valor de vida en el enemigo.
    [SerializeField]
    private int startingHealth = 20;
    //Tiempo entre da�os al enemigo.
    [SerializeField]
    private float timeSinceLastHit;
    //Tiempo de desaparici�n del enemigo;
    [SerializeField]
    private float dissapearSpeed = 2f;
    //El valor del da�o.
    [SerializeField]
    private int currentHealth;

    //Control del tiempo.
    private float timer = 0f;
    //Carga de la animaci�n.
    private Animator anim;
    //Coger el NavMex
    private NavMeshAgent nav;
    //Saber si est� muerto.
    private bool isAlive;
    //Coger el rigidbody.
    private Rigidbody rigidBody;
    //Coger el collider.
    private CapsuleCollider capsuleCollider;
    //Preguntar si ha desaparecido.
    private bool dissapearEnemy = false;

    //Hacemos una propiedad de si est� vivo.
    public bool IsAlive
    {
        //Devolvemos si est� vivo o muerto.
        get { return isAlive; }
    }

    void Start()
    {
        //Inicializamos las variables.
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
    }
    void Update()
    {
        timer += Time.deltaTime;   
    }
    //Cuando colisiona el cuchillo con el enhemigo.
    private void OnTriggerEnter(Collider other)
    {
        //Preguntamos si se le puede golpear y si est� vivo
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            //Si golpea el cuchillo.
            if(other.tag == "PlayeWeapon")
            {
                //Llamamos al m�todo de descontar vida.
                takeHit();
                //Reinicioamos el tiempo entre golpes.
                timer = 0f;
            }
        }
    }
    //M�todo de quitar vida
    private void takeHit()
    {
        //Si tiene puntos.
        if(currentHealth > 0)
        {
            anim.Play("Hurt");
            currentHealth -= 10;
        }
    }
}
