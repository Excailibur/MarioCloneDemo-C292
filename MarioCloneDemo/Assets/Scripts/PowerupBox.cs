using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBox : MonoBehaviour
{
    // Field for which particular powerup to spawn.
    [SerializeField] GameObject powerUp;
    // Field for the sprite to swap to after the powerup has been activated.
    [SerializeField] Sprite inactiveSprite;
    // Keep track of if the box has been activated already or not.
    private bool isUsed = false;

    // Reference for the Sprite Renderer component on this powerupBox.
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Initialize the spriteRenderer.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // This is a built-in Event in Unity that is triggered when a gameObject with a collider bumps into
    // the collider attached to the gameObject this script is attached to.
    // This particular one only works with 2D colliders (not 3D and not triggers).
    // It is called a single time on the frame any new colllision first happens.
    // You can see it accepts a parameter, whenever something collides with this collider,
    // a reference to the collider that hit it, is passed in automatically and stored in the collision variable.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Here we are using the reference we grabbed before automatically to access the gameObject it's attached to.
        // collision.gameObject means "The gameObject that the collider stored in the 'collision' variable is attached to.
        // Then, we're accessing the tag associated with that gameObject using .tag.
        // NOTE: The tag for gameobjects can be changed inside the Inspector, at the very top right under the object name, it's a dropdown box that says "Tag".
        // We're checking to see if the tag is "Player".
        // So we're basically saying, "Is the thing that just collided with us the Player?".
        if (collision.gameObject.tag == "Player")
        {
            // If it is true that the thing that just hit us is in fact the player, we want to do something specific.
            // This could be maybe taking damage, dealing damage, changing size, changing our sprite, changing color, anything.
            // In this particular case, we are simply going to teleport the gameobject that this script is attached to, 3 meters up into the air.
            // To do this, we are directly accessing the position of the transform component that's attached to this gameObject.
            // We can access the Transform component using 'transform', and then using .position, we can directly access the position property
            // of this Transform component on this gameObject and either set it, or read it.
            // In this case we want to set it, and we're using += to take what it was before, and add a Vector to it.
            // We're creating a new vector so we need to use the 'new' keyword followed by the data type (Vector3 in this case).
            // The Vector3 constructor needs an x, y, and z value.
            // Because I want to move the object upwards, I need to put something in for the y value.
            // I'm putting 3 cause I want it to move up 3 meters. I'm using 0 for the x and z value because I don't want to change those.
            // So all together this is saying, "Move the object this script is on upward 3 meters from where it already was".
            //transform.position += new Vector3(0, 3, 0);

            // First we check to see if the player is jumping (we only want to spawn a powerup if the player jumps into the powerup box.
            if (collision.gameObject.GetComponent<PlayerController>().isJumping)
            {
                // The player is jumping, so we spawn the powerup.
                SpawnPowerup();
            }
        }
    }

    // This does the same as the OnCollisionEnter2D Event above, but this is for triggers instead.
    // NOTE: You have to use different Events depending on if it's a trigger or collider.
    // Remember, colliders are a physical object that will act as a barrier and slow down, or stop things that hit it.
    // But triggers, act as objects that don't physically block anything, but they still can be used to detect collisions with them.
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            transform.position += new Vector3(0, 3, 0);
        }
    }

    // This method will be called when the player hits the powerup.
    void SpawnPowerup()
    {
        // First check to make sure this powerupBox hasn't already spawned a powerup.
        if (!isUsed)
        {
            // Flip the boolean to true so it can't spawn more powerups after this one.
            isUsed = true;
            // Spawn the powerup at the location of the powerup box, but 1 meter higher.
            Instantiate(powerUp, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            // Switch the sprite for the box from the "active" version to the "used" version.
            spriteRenderer.sprite = inactiveSprite;
        }
    }
}