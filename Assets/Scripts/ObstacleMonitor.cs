using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMonitor : MonoBehaviour
{
    public Transform[] obstacleLocations;

    public GameObject[] obstaclesPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < obstacleLocations.Length; i++)
        {
            int randomObstacle = Random.Range(0, obstaclesPrefabs.Length);
            Instantiate(obstaclesPrefabs[randomObstacle], obstacleLocations[i].position, obstacleLocations[i].rotation);
        }
    }

}
