using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Speed the enemy will move at.
    [SerializeField] float moveSpeed = 3f;
    // How many "points" this enemy is worth.
    [SerializeField] int points;

    // Field to store the reference of the Sprite Renderer component on this enemy.
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Find and store the reference.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy at a consistent speed along the world X axis.
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void SwapMoveDirection()
    {
        // This will reverse the moveDirection of the enemy by toggling between -moveSpeed and movespeed.
        moveSpeed *= -1;
        // This will flip the sprite on the X axis, swapping the direction it's facing from left to right.
        // We're simply setting the value of the flipX boolean to be the opposite of what it is at the time
        // the method is called.
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // This method simply allows us a way to get the points this enemy is worth.
    public int GetPoints()
    {
        return points;
    }
}