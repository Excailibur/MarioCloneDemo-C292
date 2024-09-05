using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScipt_Edison : MonoBehaviour
{
    [SerializeField] private Transform player;  // The player to follow

    [SerializeField] private Vector3 offset;    // The offset from the player

    [SerializeField] private bool isFollowing = true;  // Whether the camera is following the player
    [SerializeField] private bool xFollow = true;      // Whether the camera follows the player on the x axis
    [SerializeField] private bool yFollow = true;      // Whether the camera follows the player on the y axis


    void Start()
    {
        //Find the object named Player in the scene
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Call the function to follow the player
        FollowPlayer();
    }

    /// <summary>
    /// Follows the player base of certain conditions
    /// </summary>
    void FollowPlayer()
    {
        //Check if the camera can follow the player
        if (isFollowing)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            //Toggle the x and y follow
            if (!xFollow)
            {
                targetPosition.x = transform.position.x;
            }
            if (!yFollow)
            {
                targetPosition.y = transform.position.y;
            }

            //Move the camera to the target position + offset
            transform.position = targetPosition + offset;
        }
    }
}
