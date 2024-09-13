using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check to see if an enemy collided with the edge.
        if (collision.gameObject.tag == "Enemy")
        {
            // If it was an enemy, access their Enemy script and call the SwapMoveDirection().
            // This will prevent them from falling off the edge, and turn them around to walk the other way.
            collision.gameObject.GetComponent<Enemy>().SwapMoveDirection();
        }
    }
}