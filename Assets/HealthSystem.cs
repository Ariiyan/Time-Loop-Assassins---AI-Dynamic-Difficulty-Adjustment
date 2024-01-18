using UnityEngine;
using TMPro; 

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 5;

    public int currentHealth;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");

            GameManager.instance.PlayerLostLife(); 
        }

        Debug.Log("Damage taken: " + damage);
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;



        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Lives: " + currentHealth;
    }

  
    public void ResetHealth()
    {
        currentHealth = maxHealth;


        UpdateHealthUI();
    }
}
