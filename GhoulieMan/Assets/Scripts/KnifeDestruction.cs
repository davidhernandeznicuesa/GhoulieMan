using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destruye el cuchillo al cabo de 2 segundos.
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    //Si el cuchillo choca.
    private void OnTriggerEnter(Collider other)
    {
        //Preguntamos al collider si ha chocado con un objeto.
        if (other.gameObject)
        {
            //Debug.Log("Choca");
            //Destruyo el cuchillo.
            Destroy(this.gameObject);
        }
    }
}
