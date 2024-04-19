using UnityEngine;

public class ObstacleMonitor : MonoBehaviour
{
    public Transform[] obstacleLocations;

    public GameObject[] obstaclesPrefabs;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (var t in obstacleLocations)
        {
            var randomObstacle = Random.Range(0, obstaclesPrefabs.Length);
            Instantiate(obstaclesPrefabs[randomObstacle], t.position, t.rotation);
        }
    }
}