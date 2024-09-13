using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Field to hold a reference to the Player in the game.
    // Note that we are using the Transform class as opposed to GameObject or PlayerController.
    // This is because all I really want to know is the location of the player so the camera
    // can follow them. You could easily use PlayerController or GameObject as well,
    // all you'd have to do is add one extra step to access the Transform component.
    private Transform player;

    // We'll use this field to store an offset for the Y value so we can control how high or low the camera is.
    [SerializeField] float originalOffset;
    // This field will be equal to double the original offset.
    private float raisedOffset;
    // This field will be used to position the camera.
    private float currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        // As soon as the game starts, we'll search the entire scene for the gameobject with the "Player" tag.
        // We use the FindGameObjectWithTag() method to do this.
        // Then we will access its transform component using .transform, and save that to the player field.
        // We can now interact with the location of the player during runtime.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // This will set our currentOffset to be equal to whatever we input for our originalOffset.
        currentOffset = originalOffset;

        // This will calculate and store the value of raisedOffset which is the position of the camera on the Y
        // axis after the camera has moved up because the player is jumping higher.
        raisedOffset = originalOffset * 2f;
    }

    // LateUpdate is called once per frame after Update() has already been called on everything.
    // Usually camera movement should be in LateUpdate() so that the camera moving is the last thing that happens.
    // This usually helps prevent jerky movement.
    void LateUpdate()
    {
        // Check to see if the player higher than 4 meters above the center of the screen.
        if (player.transform.position.y >= originalOffset + 4f)
        {
            // Change the offset to be 2x what it was before, so you can see more space above the player.
            currentOffset = raisedOffset;
        }
        // If the player isn't 2 meters above the middle of the screen, reset the offset back to the original.
        else
        {
            currentOffset = originalOffset;
        }
        // This sets the position of the camera to be the same as the player's x position, the y position
        // will be our offset for how high or low we want the camera to be in the scene.
        // The z position will just be set to -10. Remember, it's a 2D game,
        // but it's still in 3D space, so we need all the sprites and images to be in "front of" the camera.
        // So we do this by pulling the camera "back" "towards us" by 10 meters.
        // So now all the sprites and stuff are 10 meters in front of the camera.
        transform.position = new Vector3(player.position.x, currentOffset, -10);
    }
}