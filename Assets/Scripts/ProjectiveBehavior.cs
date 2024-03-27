using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float lifespan = 2.0f; // Adjust the lifespan as needed
    public float speed = 10f;

    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward in its facing direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Update the timer
        timer += Time.deltaTime;

        // Check if the bullet has exceeded its lifespan
        if (timer >= lifespan)
        {
            DeactivateProjectile();
        }
    }

    public void Fire()
    {
        gameObject.SetActive(true);
    }

    // Method to initialize the projectile
    public void Initialize()
    {
        timer = 0f; // Reset the timer when firing
    }

    // Method to deactivate the projectile
    private void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }
}
