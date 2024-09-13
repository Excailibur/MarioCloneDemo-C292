using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // This will allow us to specfici specific layers that exist in the Editor.
    // I created a custom "Ground" layer and applied it to ALL blocks the player can walk on.
    // Then, in the Inspector, I checked the box for groundLayer next to "Ground".
    [SerializeField] LayerMask groundLayer;

    // We'll use this field to store if the player is currently jumping or not.
    private bool isJumping = false;

    // References
    // We will use references anytime we need to access and communicate with a specific instance of any other class.
    // Here we are declaring a field called rb and saying that it's of type Rigidbody2D.
    // Remember classes are objects, which means you can think of classes as data types/structures.
    private Rigidbody2D rb;

    // We need a reference to the Animator component on the player to have control over the animations.
    private Animator animator;

    // A reference to the Sprite Renderer on the player.
    private SpriteRenderer spriteRenderer;

    // Start is called a single time before the first frame update
    // This is a good place to initialize fields or references that were not initialized during declaration.
    // This is also a good place to run through any loops to find certain gameobjects in the scene or to randomly place objects, etc.
    void Start()
    {
        // This is calling the GetComponent() method which is part of Monobehaviour and every gameobject.
        // So this is saying, "find the Rigidbody2D component on this gameobject and store a reference to it in the field we called rb.
        // We do this so that we can access the methods and fields of the Rigidbody2D component on our player character.
        rb = GetComponent<Rigidbody2D>();

        // Initialize the Animator.
        animator = GetComponent<Animator>();

        // Initialize the Sprite Renderer.
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Vector3 position = transform.position;
        //Vector3 size = transform.localScale;
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

        // There are a lot of ways to check if the player is on the ground.
        // The easiest way is to make it so when the Jump() method is called, it sets isJumping to true.
        // And then using OnCollisionEnter2D, we check to see if the player collides with the ground,
        // and if they do, set isJumping back to false.
        // But what if the player simply walks off a floating platform but doesn't jump first?
        // isJumping would still be set to false, which means they'd be able to jump in the air.
        // So instead, in addition to setting isJumping to true in Jump(), we'll also check to see
        // if the ground is closeby and underneath the player.
        // We will use a raycast to do this. This essentially just shoots a straight invisible laser beam
        // from a position we choose (so the player's position) in a direction we choose (so down in this case).
        // We also can specify the max range of the laser. And we will also tell it what layer it can hit.
        // It won't hit anything that's not in this layer.
        if (Physics2D.Raycast(transform.position, Vector2.down, .6f, groundLayer))
        {
            // If our ray sucessfully hits something on the ground layer, we're obviously not jumping.
            isJumping = false;
        }
        else
        {
            // And the other option is we are jumping since the ground isn't below us.
            isJumping = true;
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

        // If our moveInput is anything other than 0, we will tell the Animator that we're moving.
        // This will then update our isMoving parameter in the Animator Controller for the player.
        // And our animation graph has it set up so if isMoving is true, the animation for walking will play.
        if (moveInput != 0f)
        {
            // Call the SetBool() method on the animator, passing in the name of the parameter we set up in the Animator,
            // and then the value for the boolean. In this case, the player isn't stationary, so we set it to true.
            animator.SetBool("isMoving", true);
        }
        else
        {
            // Here, the only other option is the player isn't moving, so we set it to false.
            animator.SetBool("isMoving", false);
        }

        // Flip the player to facing left/right depending on movement direction.
        if (moveInput > 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput < 0f)
        {
            spriteRenderer.flipX = false;
        }

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
        // Let's first check to see if the player is already jumping.
        if (isJumping)
        {
            // If we are jumping, we just exit the method by returning.
            // You could also check in Update() to see if the player is already jumping before allowing
            // Jump() to be called.
            return;
        }

        // We're calling the AddForce() method from the Rigidbody2D class we referenced earlier (the one on the player).
        // AddForce is an overloaded method meaning there are different versions of it that take different types/numbers of arguments.
        // This particular version takes a vector, and the ForceMode2D of the physics force, which is just an enum defined in the Rigidbody2D class.
        // So again, Vector2.up = new Vector2(0, 1).
        // So we're multiplying that vector by the jumpForce which doesn't change the direction of the jump, but it does change the magnitude.
        // And ForceMode.Impulse means we want 100% of the force applied instantly in a single frame.
        // If you're wondering, all forces in Unity are in Newtons.
        // If you don't know what a Newton is, and want to know, just ask us. This isn't a physics class unfortunately.
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Set isJumping to true, since we just jumped.
        isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with an enemy, they die and the level restarts.
        // BUT, we also want to check if the player is currently jumping or not, cause if they are, they should kill
        // the enemy instead.
        if (collision.gameObject.tag == "Enemy")
        {
            // If we are jumping, kill the enemy.
            if (isJumping)
            {
                Destroy(collision.gameObject);

                // Let's also add a little upward force to the player.
                rb.AddForce(Vector2.up * 50f, ForceMode2D.Impulse);

                // Let's also update the score with how much this enemy was worth.
                // We're calling the IncreaseScore method from the UIManager instance we made.
                // We're then passing in the points the enemy was worth.
                // We do this by accessing the gameobject on the collider that we hit, and then
                // we get the Enemy component on that specific enemy, and call its GetPoints method.
                UIManager.Instance.IncreaseScore(collision.gameObject.GetComponent<Enemy>().GetPoints());
            }
            // Otherwise, the player dies and we restart the scene.
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}