using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // This simply is a field to store how many "points" this particular Coin is worth when picked up.
    // This is what will be passed into the IncreaseScore() method in the UIManager class.
    [SerializeField] int pointValue;
}