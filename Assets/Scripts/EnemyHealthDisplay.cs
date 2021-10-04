using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    RectTransform healthBar;
    int enemyStartingHealth;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        Transform child = transform.Find("HealthBar");
        healthBar = child.GetComponent<RectTransform>();
    }

    public void displayHealth(int current, int max)
    {
        Debug.Log("displayHealth: " + current + " max: " + max);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
