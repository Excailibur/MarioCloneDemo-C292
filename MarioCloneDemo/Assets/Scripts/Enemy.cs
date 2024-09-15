using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: This is an abstract class. What's that mean?
// It means we cannot directly attach this script to anything.
// So, if we want anything to use the stuff in here, it has to be on a separate script that
// inherits from Enemy.
public abstract class Enemy : MonoBehaviour
{
    // Speed the enemy will move at.
    [SerializeField] protected float moveSpeed = 3f;
    // How many "points" this enemy is worth.
    [SerializeField] int points;

    // Field to store the reference of the Sprite Renderer component on this enemy.
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    // Note we use the protected keyword.
    // Remember this means this method can be seen/called from not only this class,
    // but also any class that inherits from this one.
    // We also use the virtual keyword, what's this mean?
    // The virtual keyword means this method can be overridden. That means in any class
    // that inherits from this Enemy class, we can choose to:
    // A: Use this same Start() method with no changes.
    // B: Use a completely different Start() method in the class that inherits from this one.
    // C: We can call the same Start() method from this class AND add extra behavior to it.
    protected virtual void Start()
    {
        // Find and store the reference.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
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

    protected virtual void Move()
    {
        // Move the enemy at a consistent speed along the world X axis.
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}