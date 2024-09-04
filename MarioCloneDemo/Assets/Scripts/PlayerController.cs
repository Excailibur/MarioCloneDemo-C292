using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Fields
    // Fields to store some of the player's "stats".
    // Remember, making the access modifier public will make it show up in the inspector BUT it also will be accessible by any class in your game.
    // If you make it private, it will only be accessible in this class, and you'd have to use getter and setter methods to access it.
    // If you use [SerializeField] you can have it show up in the inspector which is so helpful, while maintaining the private access modifier.
    // So generally this is the best thing to use for almost all fields.
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    // References
    // We will use references anytime we need to access and communicate with a specific instance of any other class.
    // Here we are declaring a field called rb and saying that it's of type Rigidbody2D.
    // Remember classes are objects, which means you can think of classes as data types/structures.
    private Rigidbody2D rb;

    // Start is called a single time before the first frame update
    // This is a good place to initialize fields or references that were not initialized during declaration.
    // This is also a good place to run through any loops to find certain gameobjects in the scene or to randomly place objects, etc.
    void Start()
    {
        // This is calling the GetComponent() method which is part of Monobehaviour and every gameobject.
        // So this is saying, "find the Rigidbody2D component on this gameobject and store a reference to it in the field we called rb.
        // We do this so that we can access the methods and fields of the Rigidbody2D component on our player character.
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame, every single frame.
    // Generally you always want to be checking for any input in Update() (or calling a method that checks for input).
    // Be careful though, anything you put in here HAS to finish before the game will progress through the next frame.
    // So putting loops in here is usually very risky unless it's a very small loop.
    void Update()
    {
        // We'll call the Move() method every single frame because it checks for player input and moves the player accordingly.
        Move();

        // Here we are checking for the input in Update() as opposed to in the method we're calling.
        // As such, we only need to call the Jump() method if the correct button is pushed.
        // Remember, GetButtonDown or GetKeyDown is a boolean that returns true on the frame the button is first pressed.
        // The parameters of both methods are the name of the button, or the KeyCode of the specific key.
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    // Method to handle player input, and apply movement if necessary.
    private void Move()
    {
        // Input is a built-in Unity class that handles, you guessed it, player input.
        // Here we are storing the result of an Axis into a local variable.
        // Any axis has a positive and negative button associated with it.
        // Our specific "Horizontal" axis which is defined in the Unity Input Manager (edit -> project settings -> input manager)
        // uses the a key for negative and the d key for positive.
        // All axis inputs return a float from -1 to 1. 1 being you're pressing the positive button,
        // and -1 meaning you're pressing the negative button.
        // So if we're pressing 'd' on the keyboard, moveInput will be: 1
        // If we press 'a' it'll be -1.
        // If we're pressing nothing, it will be 0.
        float moveInput = Input.GetAxis("Horizontal");

        // (1,0)
        // new Vector2(1, 0)
        // Here we are calling the Translate() method on the transform component that is on this specific gameobject, which is the player character.
        // Translate() needs a vector to be passed in as an argument.
        // It simply takes the gameobject from whatever position it's currently in,
        // and moves it in the direction and distance specified by the vector.
        // If you don't know what a vector is, it's a unit that defines direction and magnitude.
        // So in 2D space, we have the x and y axis.
        // So, (x, y) is the format of the vector.
        // (1, 0) would be saying, "move 1 unit on the positive x axis", which is to the right.
        // (-1, 0) is the same thing, but now it's to the left.
        // (0, 1) is telling it to move 1 unit on the positive y axis, which is upwards.
        // (0, -1) is the same, but downwards.
        // (1, 1) means move 1 unit right, and one unit upward.
        // So basically if you measured from the horizontal axis, it's 45 degrees up and to the right.
        transform.Translate(moveInput * Vector2.right * moveSpeed * Time.deltaTime);
        // In the line above, Vector2.right is simply shorthand for: new Vector2(1, 0).
        // So let's say our current input is the 'd' key, that means moveInput = 1.
        // Let's say our moveSpeed = 5 and Time.deltaTime = 0.0167 (this is the time between each frame when running at 60 fps).
        // So our equation is 1 * (1, 0) * 5 * 0.0167 = (0.0835, 0).
        // So our player will move 0.0835 meters to the right every frame.
        // If our frame rate is 60 fps: 0.0835 * 60 = 5 meters per second movement speed.
    }

    // Method to make the player jump in the air using a physics force applied to the Rigidbody2D on this player.
    private void Jump()
    {
        // We're calling the AddForce() method from the Rigidbody2D class we referenced earlier (the one on the player).
        // AddForce is an overloaded method meaning there are different versions of it that take different types/numbers of arguments.
        // This particular version takes a vector, and the ForceMode2D of the physics force, which is just an enum defined in the Rigidbody2D class.
        // So again, Vector2.up = new Vector2(0, 1).
        // So we're multiplying that vector by the jumpForce which doesn't change the direction of the jump, but it does change the magnitude.
        // And ForceMode.Impulse means we want 100% of the force applied instantly in a single frame.
        // If you're wondering, all forces in Unity are in Newtons.
        // If you don't know what a Newton is, and want to know, just ask us. This isn't a physics class unfortunately.
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}