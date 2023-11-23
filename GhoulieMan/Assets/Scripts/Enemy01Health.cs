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
    //Tiempo entre daños al enemigo.
    [SerializeField]
    private float timeSinceLastHit;
    //Tiempo de desaparición del enemigo;
    [SerializeField]
    private float dissapearSpeed = 2f;
    //El valor del daño.
    [SerializeField]
    private int currentHealth;

    //Control del tiempo.
    private float timer = 0f;
    //Carga de la animación.
    private Animator anim;
    //Coger el NavMex
    private NavMeshAgent nav;
    //Saber si está muerto.
    private bool isAlive;
    //Coger el rigidbody.
    private Rigidbody rigidBody;
    //Coger el collider.
    private CapsuleCollider capsuleCollider;
    //Preguntar si ha desaparecido.
    private bool dissapearEnemy;
    //BoxCollider del arma
    private BoxCollider weaponCollider;
    //Variable para que emita sonido al ser herido.
    private AudioSource audio;
    //Para reproducir el audio .
    [SerializeField]
    private AudioClip hurtAudio;
    //Para reproducir el audio .
    [SerializeField]
    private AudioClip dieAudio;
    //Hacemos una propiedad de si está vivo.
    public bool IsAlive
    {
        //Devolvemos si está vivo o muerto.
        get { return isAlive; }
    }
    void Start()
    {
        //Cogemos el boxCollider Hijo
        weaponCollider = GetComponentInChildren<BoxCollider>();
        //Inicializamos las variables.
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        dissapearEnemy = false;
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Cogemos el tiempo real.
        timer += Time.deltaTime;
        //Hundimos el personaje.
        if (dissapearEnemy)
        {
            //Movemos el cadaver al interior de la plataforma.
            transform.Translate(dissapearSpeed * Time.deltaTime * Vector3.down);
        }
    }
    //Cuando colisiona el cuchillo con el enhemigo.
    private void OnTriggerEnter(Collider other)
    {
        //Preguntamos si se le puede golpear y si está vivo
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            //Si golpea el cuchillo.
            if(other.tag == "PlayerWeapon")
            {
                //Llamamos al método de descontar vida.
                takeHit();
                //Reinicioamos el tiempo entre golpes.
                timer = 0f;
            }
        }
    }
    //Método de quitar vida
    private void takeHit()
    {
        //Si tiene puntos.
        if(currentHealth > 0)
        {
            //Emite el sonido de herido.
            audio.PlayOneShot(hurtAudio);
            anim.Play("Hurt");
            currentHealth -= 10;
        }
        //Si está el enemigo muerto.
        if (currentHealth <= 0)
        {
            isAlive = false;
            //Llamamos al método de animación de morirse.
            KillEnemy();
        }
    }
    //Método de activar animación de muerte de enemigo.
    private void KillEnemy()
    {
        //Desactivar el collider
        capsuleCollider.enabled = false;
        //Emite el sonido de muerte.
        audio.PlayOneShot(dieAudio);
        //Activar la animación de morirse.
        anim.SetTrigger("EnemyDie");
        //Activar la función manual del rigidbody.
        rigidBody.isKinematic = true;
        //Activar la coroutine
        StartCoroutine(removeEnemy());
        //desactivamos el collider del arma
        weaponCollider.enabled = false;
        //Desactivar el camino(navMesh).
        nav.enabled = false;
    }
    //Tiempo que tarda en desaparecer el personaje.
    IEnumerator removeEnemy()
    {
        //Hacer desaparecer el enemigo a los 2 segundos.
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        //Destruye el objeto al cabo de 1 segundo.
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
