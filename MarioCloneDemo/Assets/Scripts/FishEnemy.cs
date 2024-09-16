using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : Enemy
{
    // Field to store the sprites for the animation.
    [SerializeField] Sprite animationSprite;
    private Sprite normalSprite;
    // Field to store the jump direction.
    private Vector2 direction;

    // Reference to the rigidbody on the Fish.
    private Rigidbody2D rb;

    protected override void Start()
    {
        // Use the original Enemy behavior for Start().
        base.Start();
        // Initialize the rb field.
        rb = GetComponent<Rigidbody2D>();

        // Initialize the starting sprite.
        normalSprite = spriteRenderer.sprite;

        // Calculate the direction.
        RandomDirection();

        // Launch the fish!
        Launch();

        // Start the Coroutine for playing the "animation".
        StartCoroutine(PlayAnimation());
    }

    protected override void Update()
    {
        // This is here so we DON'T inherit the original behavior of the Enemy Move() method.
    }

    // This method launches the fish at a 45 degree angle either to the left or right.
    private void Launch()
    {
        rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }

    // We'll use this method to calculate a random direction for this fish to jump at spawn time.
    private void RandomDirection()
    {
        // Randomly generate a horizontal direction between straight left, to straight right.
        int x = Random.Range(-1, 2);
        // Initialize the direction with the randomly generated x value, and 2 for the y value.
        direction = new Vector2(x, 3);
    }

    // This is called a Coroutine. They are ran on a separate thread on the CPU.
    // This means they don't block any other method calls like Update().
    // Here we are using it to play the animation of the fish opening and closing its mouth.
    // NOTE: The most important reason we're using this is because we can use a loop that doesn't freeze
    // the rest of the game, (like putting this in Update() would).
    // And so we can take advantage of WaitForSeconds, which literally waits for X seconds,
    // and then continues afterwards.
    private IEnumerator PlayAnimation()
    {
        // Endless loop, always true.
        while (true)
        {
            // Switch the sprite to the image of the fish with the open mouth.
            spriteRenderer.sprite = animationSprite;
            // Wait for 0.1 seconds.
            yield return new WaitForSeconds(0.1f);
            // Switch the sprite to the image of the fish with the closed mouth.
            spriteRenderer.sprite = normalSprite;
            // Wait for 0.1 seconds.
            yield return new WaitForSeconds(0.1f);
        }
        // NOTE: You could totally use an Animator and Animation Clip for this too.
        // I just wanted to show off Couroutines.
    }
}