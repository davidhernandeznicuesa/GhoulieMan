using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Attack : MonoBehaviour
{
    //Variables para el ataque para manejar en Unity.
    //Variable de la distancia entre el player y el enemigo.
    [SerializeField] private float range = 3f;
    //Variable de tiempo entre ataques.
    [SerializeField] private float timeBetweenAttacks = 1f;
    //Variables para manejar con propiedades.
    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider weaponCollider;
    //Variable para acceder a la clase enemy01Health
    private Enemy01Health enemyHealth;
    

    void Start()
    {
        //Inicializar las variables.
        anim = GetComponent<Animator>();
        player = GameManager.instance.Player;
        weaponCollider = GetComponentInChildren<BoxCollider>();
        StartCoroutine(attack());
        //Cargamos la clase
        enemyHealth = GetComponent<Enemy01Health>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position)< range && enemyHealth.IsAlive)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    IEnumerator attack()
    {
        if (playerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("Attacking");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        StartCoroutine(attack());
    }
}
