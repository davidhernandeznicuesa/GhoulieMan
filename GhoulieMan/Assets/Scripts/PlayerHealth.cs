using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Variable con la cantidad de vida.
    [SerializeField] int startingHealth = 100;
    //Variable para el tiempo entre ataques.
    [SerializeField] float timeSinceLastHit = 2.0f;
    //Cantidad de vida que te queda.
    [SerializeField] int currentHealth;
    //Inicializar el contador de tiempo entre ataques.
    [SerializeField] private float timer = 0f;
    //Animación.
    private Animator anim;
    //Para cargar la animación del player.
    private CharacterMovement characterMovement;
    //Variable para manejar el Slider.
    [SerializeField] Slider healthSlider;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }
    void Update()
    {
        //Captura del tiempo pasado.
        timer += Time.deltaTime;
        //Cargamos el movimiento dl player
        characterMovement = GetComponent<CharacterMovement>();
    }
    //Cuando el jugador choca con un collider de la maza y el enemigo con el isTrigger activado.
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        //Si ha pasado el tiempo de espera y estamos vivos.
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            //Si nos golpea la maza o nos pegamos con el enemigo.
            if(other.tag == "weapon")
            {
                //Llamamos al método de golpeo
                takeHit();
                //Inicializamos el tiempo entre golpes.
                timer = 0f;
            }
        }
    }

    //Método para el control del daño.
    void takeHit()
    {
        //Si tiene vida
        if (currentHealth > 0)
        {
            //Mandamos la vida que le queda.
            GameManager.instance.PlayerHit(currentHealth);
            //Animación de golpeado.
            anim.Play("Hurt");
            //Descontamos 10 puntos
            currentHealth -= 10;
            //Descontar en la barra.
            healthSlider.value = currentHealth;
        }
        //Si ha llegado a cero
        if(currentHealth <= 0)
        {
            //Mandamos la vida que le queda.
            GameManager.instance.PlayerHit(currentHealth);
            //Animación de muerte.
            anim.SetTrigger("isDie");
            //Desacivamos el movimiento del player.
            characterMovement.enabled = false;
        }
    }
}
