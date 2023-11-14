using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }
    void Update()
    {
        //Captura del tiempo pasado.
        timer += Time.deltaTime;    
    }
    //Cuando el jugador choca con un collider de la maza y el enemigo con el isTrigger activado.
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        //Si ha pasado el tiempo de espera y estamos vivos.
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            //Si nos golpea la maza.
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
        GameManager.instance.PlayerHit(currentHealth);
        anim.Play("Hurt");
        currentHealth -= 10;
    }

}
