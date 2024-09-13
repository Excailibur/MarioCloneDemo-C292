using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // This simply is a field to store how many "points" this particular Coin is worth when picked up.
    // This is what will be passed into the IncreaseScore() method in the UIManager class.
    [SerializeField] int pointValue;

    private void Update()
    {
        // This rotates the coin as it sits there.
        transform.Rotate(new Vector3(0, -180, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // If the player touches the coin, increase the score according to this coin's value.
            UIManager.Instance.IncreaseCoins(pointValue);
            // Destroy the coin.
            Destroy(gameObject);
        }
    }
}