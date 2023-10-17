using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Variable de velocidad.
    public float maxSpeed;
    //Variable de dirección del player.
    public float moveDirection;
    //Variable para ver si mira a la derecha.
    public bool facingRight;
    // Variable para coger el objeto rigidbody.
    private Rigidbody rigidbody;
    //Variable para manejar animaciones.
    private Animator anim;


    void Start()
    {
        //Inicializamos la velocidad.
        maxSpeed = 6.0f;
        //Inicializamos hacia donde mira.
        facingRight = true;
        //Coger el rigidbody.
        rigidbody = GetComponent<Rigidbody>();
        //Cargamos el anim con el componente Animator.
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si presionamos la flecha derecha se mueva.
        moveDirection = Input.GetAxis("Horizontal");
    }

    //Método especial para manejar las físicas.
    private void FixedUpdate()
    {
        //Le damos la velocidad a x y mantenemos la velocidad de y.
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
       
        //Preguntamos por el valor de horizontal para aplicar el giro.
        if (moveDirection>0.0f && !facingRight)
        {
            Flip();
        }else if(moveDirection < 0.0f && facingRight)
        {
            Flip();
        }
        anim.SetFloat("Speed", Mathf.Abs(0.5f));
    }
    void Flip()
    {
        //Se invierte el sentido del movimiento
        facingRight = !facingRight;
        //Le damos el valor al vector para que rote 180 grados.
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
}
