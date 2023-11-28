using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    //Variables de m�rgenes.
    public float xMargin = 1.0f;
    public float yMargin = 1.0f;

    public float xSmooth = 10.0f;
    public float ySmooth = 10.0f;

    public Vector2 maxXandY;
    public Vector2 minXandY;

    public Transform cameraTarget;

    //Variables camara del Boss
    public Transform bossCameraTarget;
    public bool bossCameraActive = false;
    public float cameraSpeed = 30.0f;


    //M�todo de carga anterior al Start.
    private void Awake()
    {
        //Cargamos la c�mara del player
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").transform;
        //Cargamos la c�mara del Boss.
        bossCameraTarget = GameObject.FindGameObjectWithTag("BossCameraTarget").transform;
    }
    //M�todos que comprueban que estamos dentro de la pantalla.
    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - cameraTarget.position.x) > xMargin;
    }
    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - cameraTarget.position.y) > yMargin;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        TrackPlayer();
    }

    //M�todo del movimiento de la c�mara.
    private void TrackPlayer()
    {
        //Variables de las posiciones de X e Y.
        float targetX = transform.position.x;
        float targetY = transform.position.y;
        //Preguntamos si estamos dentro de los m�rgenes.
        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, cameraTarget.position.x, xSmooth * Time.deltaTime);
        }
        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, cameraTarget.position.y, ySmooth * Time.deltaTime);
        }
        //Clamp sirve para darle un valor m�nimo y un valor m�ximo en X.
        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        //Clamp sirve para darle un valor m�nimo y un valor m�ximo en Y.
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
