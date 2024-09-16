using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : Enemy
{
    // Field to store the radius of the circular path this enemy will follow.
    [SerializeField] float radius;

    // The initial spawn location of this enemy (the center of the path).
    private Vector3 spawnPosition;

    // Override the Start method.
    protected override void Start()
    {
        // Initialize the spawn position.
        spawnPosition = transform.position;
    }

    protected override void Move()
    {
        // Calculate the positions around the circle.
        // We use Time.time to make the number adjust and change.
        float x = Mathf.Cos(Time.time * moveSpeed) * radius;
        float y = Mathf.Sin(Time.time * moveSpeed) * radius;
        // Move the enemy in a circular path around the spawn position.
        transform.position = new Vector3(spawnPosition.x + x, spawnPosition.y + y, 0f);
    }
}