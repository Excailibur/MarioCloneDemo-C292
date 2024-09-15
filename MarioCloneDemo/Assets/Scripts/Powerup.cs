using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Enum to store which type of powerup this one is.
    private enum PowerupType
    {
        Diamond,
        Fireball
    }

    // Field to store which powerup subtype this is.
    [SerializeField] PowerupType powerupType;
    // The speed the powerup will move.
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly determine which direction the powerup will travel along the ground.
        // This will randomly pick a number between 0 and 2 (but not including 2, so just 0 or 1).
        int rand = Random.Range(0, 2);
        // If the randomly generated number was 1, then we will switch the moveSpeed to a negative number,
        // which effectively reverses the direction the object will be moving.
        if (rand == 1)
        {
            moveSpeed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Make the powerup move horizontally at a constant speed.
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Make sure it's the player that's colliding with the powerup.
        if (collision.gameObject.tag == "Player")
        {
            // Save a local variable that references the PlayerController script on the player that collided with the powerup.
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            // Check for which type of powerup this is, and trigger the relevant powerup activation on the player.
            if (powerupType == PowerupType.Diamond)
            {
                player.PickUpDiamondPowerup();
            }
            else if (powerupType == PowerupType.Fireball)
            {
                player.PickUpFireballPowerup();
            }
            // Despawn the powerup.
            Destroy(gameObject);
        }
    }
}