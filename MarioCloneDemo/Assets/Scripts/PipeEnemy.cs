using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEnemy : Enemy
{
    // Any fields/references here will be in addition to any from Enemy as well.
    // We will use the same technique for movement here that we did with the LoopingPlatform.
    private float startLocation;
    private float endLocation;
    [SerializeField] float distance;
    private Vector3 direction = Vector3.up;

    // We'll also store the actual spot in space where it starts.
    Vector3 startingPosition;

    // The only new things we'll do that differ from the LoopingPlatform, is we will add a delay timer.
    // We want the enemy to stay down in the pipe for a bit, then move up and back down, and wait before doing it again.
    // Our currentTimer will be what counts up from 0 until it matches delayTime, then it will allow the enemy to move,
    // and it will also reset back to 0.
    private float currentTimer = 0;
    // The delayTime is what will determine how long the enemy waits before moving out of the pipe again.
    // We are going to randomly decide the delay time.
    private float delayTime;

    // Notice we're overriding the Start() method from the Enemy class.
    // This will allow us to give new behavior in Start() that differs from Start() in Enemy.
    protected override void Start()
    {
        // This here simply says, "do everything in the Start() method from the class I inherit from".
        // So, anything in Start() in the Enemy class, will be done in the Start() method for PipeEnemy.
        base.Start();

        // Now we can add some additional custom behavior as well.
        startLocation = transform.position.y;
        endLocation = transform.position.y + distance;
        startingPosition = transform.position;

        // Let's randomly calculate the delayTime between two times.
        float rand = Random.Range(0.5f, 1f);
        // Set the delay time.
        delayTime = rand;
    }

    // Again, our Enemy class has a default movement pattern of just moving to the left constantly.
    // Our pipe enemy needs to move up and down. So we will override the Move() method to give it custom
    // behavior.
    // NOTE: Here we are NOT using base.Move(), because we don't want it to have the same Move() method PLUS more,
    // we want to completely change it to something new and forget what it originally did in the Enemy class Move().
    // We want our enemy to stay in the pipe for delay seconds, then start moving up until the top of the travel distance.
    // Then, the enemy will go back into the pipe, and the process will repeat continuously.
    protected override void Move()
    {
        // Increase the timer per frame by 1/frameRate.
        currentTimer += Time.deltaTime;
        // Check to see if the timer has been going for long enough.
        if (currentTimer >= delayTime)
        {
            // When the enemy hits the top of their movement location, start going back down.
            if (transform.position.y >= endLocation)
            {
                direction = Vector3.down;
            }
            // Once they've reached the startLocation...
            else if (transform.position.y <= startLocation)
            {
                // Reset the direction to move, back to up.
                direction = Vector3.up;
                // Reset the position.
                transform.position = startingPosition;
                // Reset the currentTimer.
                currentTimer = 0;
            }
            // Move the enemy.
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}