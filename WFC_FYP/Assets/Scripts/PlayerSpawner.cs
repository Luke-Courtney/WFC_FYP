using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Find all spawnable tiles
        GameObject[] spawnables = GameObject.FindGameObjectsWithTag("Spawnable");

        //Find the nearest spawnable
        GameObject nearestSpawnable = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPosition = player.transform.position;

        foreach (GameObject spawnable in spawnables)
        {
            float distance = Vector3.Distance(playerPosition, spawnable.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestSpawnable = spawnable;
            }
        }

        // Move the player
        if (nearestSpawnable != null)
        {
            Vector3 spawnablePosition = nearestSpawnable.transform.position;
            player.transform.position = new Vector3(spawnablePosition.x, spawnablePosition.y + 1f, spawnablePosition.z);
            Debug.Log("Player moved to 1 meter above " + nearestSpawnable.name);
        }
    }
}
