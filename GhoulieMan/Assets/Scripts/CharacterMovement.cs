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
    //Variable de salto.
    public float jumpSpeed;
    //Variables de comprobar que está tocando el suelo.
    public bool grounded;
    //Variable para coger el transform.
    public Transform groundCheck;
    //Radio de área de contacto con el collider de la base.
    public float groundRadius;
    //Variable para reconocimiento del objeto que tocamos.
    public LayerMask whatIsGround;

    //Variable de velocidad del cuchillo.
    public float knifeSpeed;
    //Variable del transform de donde sale el cuchillo;
    public Transform knifeSpawn;
    //Variable para el prefab del cuchillo.
    public Rigidbody knifePrefab;
    //Variable de el cuchillo creado.
    Rigidbody clone;

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
        //Inicializo la variable de salto.
        jumpSpeed = 600.0f;
        //Inicializamos la variable de tocar el suelo.
        grounded = false;
        //Cargar el transform.
        groundCheck = GameObject.Find("GroundCheck").transform;
        //Cargar el tamaño del radio del area de contacto con el collaider de la base.
        groundRadius = 0.2f;
        //Cargamos la velocidad del cuchillo.
        knifeSpeed = 600.0f;
        //Cargamos el objeto vacío de donde sale el cuchillo.
        knifeSpawn = GameObject.Find("KnifeSpawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Si presionamos la flecha derecha se mueva.
        moveDirection = Input.GetAxis("Horizontal");
        //Preguntamos por el botón de salto.
        if (grounded && Input.GetButtonDown("Jump"))
        {
            //Cargamos la animación de salto.
            anim.SetTrigger("IsJumping");
            //Le damos la fuerza para que salte.
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
        }
    }

    //Método especial para manejar las físicas.
    private void FixedUpdate()
    {
        //Le damos la velocidad a x y mantenemos la velocidad de y.
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
        //Cargamos el área de la base que va a tocar el collider del suelo.
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //Preguntamos por el valor de horizontal para aplicar el giro.
        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }else if(moveDirection < 0.0f && facingRight)
        {
            Flip();
        }
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));
        //Preguntamos si se ha presionado el botón izquierdo del ratón.
        if (Input.GetButtonDown("Fire1"))
        {
            //Llamamos al método Attack para disparar.
            Attack();
        }
    }
    void Flip()
    {
        //Se invierte el sentido del movimiento
        facingRight = !facingRight;
        //Le damos el valor al vector para que rote 180 grados.
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
    //Método de ataque creando el cuchillo y lanzándolo.
    void Attack()
    {
        //Cargar la animación de ataque.
        anim.SetTrigger("attacking");
        
    }
    public void CallFireProjectile()
    {
        //Creamos el objeto clone asignándole un rigidbody del prefab.
        clone = Instantiate(knifePrefab, knifeSpawn.position, knifeSpawn.rotation) as Rigidbody;
        //Le damos movimiento a clone.
        clone.AddForce(knifeSpawn.transform.right * knifeSpeed);
    }
}
