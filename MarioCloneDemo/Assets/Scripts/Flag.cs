using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // Check to see if the thing colliding with the flag is the player, if so,
    // tell the UIManager that the player won.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.WinLevel();
        }
    }
}