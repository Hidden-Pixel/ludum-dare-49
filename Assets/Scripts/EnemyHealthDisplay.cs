using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    RectTransform healthBar;
    int enemyStartingHealth;
    int currentHealth;
    Image img;

    // Start is called before the first frame update
    void Start()
    {

        Transform child = transform.Find("HealthBar");
        healthBar = child.GetComponent<RectTransform>();
        
        img = healthBar.GetComponent<Image>();
    }

    public void SetStartingHealth(int health)
    {
        currentHealth = enemyStartingHealth = health;
    }

    public void UpdateHealth(int health)
    {
        currentHealth = health;

        // Set the health bar, convert to float before attempting division.
        float percentRemaining = (currentHealth * 1.0f / enemyStartingHealth);

        // Set health bar color
        if (percentRemaining <= .4f)
        {
            img.color = Color.red;
        }

        else if (percentRemaining <= .6f)
        {
            img.color = Color.yellow;
        }

        // Set health bar scale.
        healthBar.localScale = new Vector3(percentRemaining, 1, 1);
    }
}
