using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingPlatform : MonoBehaviour
{
    // Fields
    // We'll use this speed field to give our moving platform a speed it will move at.
    // We're using the [SerializeField] attribute in a few of these to allow us to keep the field private,
    // but have it show up in the Inspector so we can change the value in the Editor without having to change it in the code.
    [SerializeField] float speed;
    // If we're going to have a platform start at one location, move to another, then go back to the start location,
    // we need to make sure we know where the start location was, so we'll store it in a field.
    // We'll make sure to grab this location at runtime so we'll come back to this later, and only declare it here with no value initialized.
    // NOTE: You can see I'm using a float data type for the startLocation and not a Vector3. Why?
    // Well, it's because I only want to know a single component of the location vector, in my case only the y value, which is just a float.
    private float startLocation;
    // We also need to know where it will end before it turns around and heads back the other way.
    // Again, this is something we want to calculate at runtime, so we'll only declare it, and initialize it later.
    // NOTE: You can see I'm using a float data type for the endLocation as well, as I only want the y value.
    private float endLocation;
    // This field will be used to specify the distance in meters we want the platform to move before it turns around.
    // We'll serialize this so we can see it in the Inspector for easier changing of the value.
    // This field is also necessary to calculate the endLocation field.
    [SerializeField] float distance;
    // And finally, we need to use a field to store which direction the platform is currently moving.
    // I'm choosing to move my platform up and down, but you could choose left and right if you'd like!
    // Remember, the direction is represented as a Vector, and Vector3.up = new Vector3(0, 1, 0).
    private Vector3 direction = Vector3.up;

    // We didn't fully go into this, but in lab I mentioned how instead of using a specific distance we could travel,
    // we could have it travel in one direction for a specific time, then turn around and go back, and keep repeating.
    // To do it like this using time, we need to store two values, we need to know the current 'stopwatch' time,
    // and we need to know the time of how long we want it to be moving (3 seconds in this case).
    // To implement this, we'd need to make sure that the currentTime was constantly counting up, and that when the currentTime
    // was the same as the moveTime, the object changed directions, and then currentTime reset back to 0, and the cycle repeats.
    //private float currentTime = 0;
    //private float moveTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Remember, Start() is called before ANY player input is accepted. In fact, it's called before the first frame is even
        // rendered to the screen. Which means it's a perfect spot to procedurally generate stuff, grab references to stuff,
        // or do any logic for initializing fields or doing calculations.
        // Here we are simply setting the startLocation to be equal to the y position of the platform when the game first starts.
        // This is the best way to do this, because if we later want to move the platform to a different location in the editor,
        // we don't need to make any more changes anywhere, it'll just simply always work.
        // Example: If our platform is at (5, 7, 0), then startLocation = 7.
        startLocation = transform.position.y;

        // Now we need to calculate the endLocation based on the startLocation and the distance we want to move.
        // We will simply set the endLocation to the startLocation + the distance.
        // NOTE: If you wanted it to start moving downwards/left first, you'd subtract the distance.
        // And you'd add distance if you want to move up/right first.
        // Example: If the startLocation is 7 and the distance is 5, then endLocation = 12.
        // So if we move from y = 7 to y = 12, we have traveled the 5 meters we wanted to travel, so this works.
        endLocation = startLocation + distance;
    }

    // Update is called once per frame
    void Update()
    {
        // We need to move the platform a tiny bit each frame, so just like we did with our character,
        // we'll use the Translate() method on the Transform component that's attached to the gameObject with this script attached.
        // Notice that we do NOT have any type of moveInput field like we did with the PlayerController, because we want this to always move,
        // and not be based on any type of input.
        // We want to move in the direction (which is a vector, in this case I set it to Vector3.up), then multiply it by the speed, and Time.deltaTime.
        // Let's say the speed is 5 and my framerate is 60fps so Time.deltaTime = 1/60 = 0.0167.
        // Formula: (0, 1, 0) * 5 * 0.0167 = 0.0835.
        // So our platform will move 0.0835 meters per second per FRAME. So at 60fps that's 60 * 0.0835 = 5 meters per second.
        // This is what we wanted!
        transform.Translate(direction * speed * Time.deltaTime);

        // We didn't use a time based method, but we mentioned it.
        // This is how you use a timer.
        // Every single frame our currentTime field will increase by Time.deltaTime.
        // At this point you should be pretty comfortable with what this means.
        // It's the time between the last frame and the current frame.
        // It can be calculated by doing 1/frameRate.
        // The easiest way to remember what it does though, is just remember, "It converts things into ___ per second.
        // It could be meters per second, or hitpoints per second, or in this case, it's just going to add up to 1 after 1 full second.
        // If it's adding 1/60 to the value each frame, after 60 frames (1 second time at 60fps) then currentTime will have increased by 1.
        // So it's literally just counting seconds.
        // We didn't write this code, but essentially you'd change the conditional statements below to use the value of currentTime and moveTime
        // instead of endLocation and startLocation to move the platform in one direction for moveTime seconds.
        // Then when currentTime was equal to moveTime, you'd swap the movement direction, and reset currentTime back to 0.
        //currentTime += Time.deltaTime;

        // After we've moved the platform, we're immediately going to check to see if the current y value of its position is greater than or equal to our endLocation.
        if (transform.position.y >= endLocation)
        {
            // If its height is at our endLocation, we'll simply switch the position to moving down.
            direction = Vector3.down;
        }
        // Likewise, if the above if statement returns false, we'll check to see if it has dropped to or below our startLocation.
        else if (transform.position.y <= startLocation)
        {
            // If the height (y position) of our object is indeed at or below our startLocation, it means we've reached or dropped below our starting location,
            // and that it's time to move back up, so we swap directions again.
            direction = Vector3.up;
        }
        // Finally, if neither of the above return true, then that means we must be in between the end and start locations,
        // and will just keep moving in the direction that's currently stored in the 'direction' field.
    }
}