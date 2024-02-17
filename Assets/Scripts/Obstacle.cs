using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleType obstacleType;

    public float damage = 5;
    public float knockbackForce = 15;
    public float knockbackDuration = 0.3f;
    public bool isSlamming;
    public List<Collider> damageColliders;
    // Start is called before the first frame update
    [SerializeField] private float speed = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacleType == ObstacleType.spinning)
            Rotate(speed);
        else if( obstacleType == ObstacleType.slamming)
        {
            if (!isSlamming)
            {
                foreach (Collider c in damageColliders)
                    c.gameObject.SetActive(false);
            }
            else
            {
                foreach (Collider c in damageColliders)
                    c.gameObject.SetActive(true);
            }
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
    slamming,
}

