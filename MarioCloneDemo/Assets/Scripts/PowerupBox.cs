using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position += new Vector3(0, 3, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            transform.position += new Vector3(0, 3, 0);
        }
    }
}
