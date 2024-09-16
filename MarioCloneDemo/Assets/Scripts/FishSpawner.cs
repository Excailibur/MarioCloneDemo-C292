using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    // Field to store what prefab this will spawn.
    [SerializeField] GameObject fish;
    // Field to store the spawn rate in seconds.
    [SerializeField] float spawnRate;

    private void Start()
    {
        InvokeRepeating("SpawnFish", 0, spawnRate);
    }

    // This spawns the fish.
    private void SpawnFish()
    {
        Instantiate(fish, transform.position, transform.rotation);
    }
}