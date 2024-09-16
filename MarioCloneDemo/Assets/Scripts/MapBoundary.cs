using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapBoundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When any object enters the trigger of the MapBoundary object, it will check if it's the player.
        // If it's the player, it'll restart the level.
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            // If it's anything else like an enemy or projectile, it'll be destroyed.
            Destroy(collision.gameObject);
        }
    }
}