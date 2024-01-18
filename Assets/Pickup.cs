using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Ammo, Health };
    public PickupType type;
    public int amount;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player picked up " + type);
            switch (type)
            {
                case PickupType.Ammo:
                    ShootingScript shootingScript = other.GetComponent<ShootingScript>();
                    if (shootingScript != null)
                    {
                        shootingScript.AddAmmo(amount);
                    }
                    break;

                case PickupType.Health:
                    HealthSystem healthSystem = other.GetComponent<HealthSystem>();
                    if (healthSystem != null)
                    {
                        healthSystem.Heal(amount);
                    }
                    break;
            }

            Destroy(gameObject); 
        }
    }
}
