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
    }

    // Again, our Enemy class has a default movement pattern of just moving to the left constantly.
    // Our pipe enemy needs to move up and down. So we will override the Move() method to give it custom
    // behavior.
    // NOTE: Here we are NOT using base.Move(), because we don't want it to have the same Move() method PLUS more,
    // we want to completely change it to something new and forget what it originally did in the Enemy class Move().
    protected override void Move()
    {
        
    }
}