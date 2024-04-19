using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleType obstacleType;

    public int damage = 5;
    public float knockbackForce = 15;
    public float knockbackDuration = 0.3f;
    public bool isSlamming;

    public List<Collider> damageColliders;

    // Start is called before the first frame update
    [SerializeField] private float speed = 1;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (obstacleType == ObstacleType.spinning)
        {
            Rotate(speed);
        }
        else if (obstacleType == ObstacleType.slamming)
        {
            if (!isSlamming)
                foreach (var c in damageColliders)
                    c.enabled = false;
            else
                foreach (var c in damageColliders)
                    c.enabled = true;
        }
    }

    private void Rotate(float speed)
    {
        transform.Rotate(0, speed, 0);
    }
}

public enum ObstacleType
{
    spinning,
    slamming
}