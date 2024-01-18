using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // The amount of damage the projectile deals

    void OnTriggerEnter(Collider other)
    {
        // Check if the projectile hits the player
        if (other.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                // Apply damage to the player
                healthSystem.TakeDamage(damage);
            }

            // Destroy the projectile after hitting the player
            Destroy(gameObject);
        }
    }
}
