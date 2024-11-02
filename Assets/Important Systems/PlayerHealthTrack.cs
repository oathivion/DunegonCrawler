using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthTrack : MonoBehaviour
{
    HealthScript playerHealth;
    [SerializeField] GameObject healthText;


    //Helth Bar variables
    [SerializeField] GameObject healthBar;
    public Vector2 healthBarPosition;
    public float maxBarWidth;
    public float currentBarWidth;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        healthBarPosition = healthBar.transform.localPosition;
        maxBarWidth = healthBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = Mathf.Abs((playerHealth.startingHealth - playerHealth.currentHealth) / playerHealth.startingHealth);
        healthText.GetComponent<TextMeshProUGUI>().text = playerHealth.currentHealth.ToString() + "/" + playerHealth.startingHealth.ToString();
        currentBarWidth = maxBarWidth * (1 - healthPercent);
        healthBar.transform.localScale = new Vector2(currentBarWidth, healthBar.transform.localScale.y);
        healthBar.transform.localPosition = new Vector2(healthBarPosition.x - healthPercent * 100, healthBarPosition.y);

    }
}
