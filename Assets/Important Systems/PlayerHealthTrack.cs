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
    public Vector2 healthBarStartPosition;
    public float maxBarWidth;
    public float currentBarWidth;
    public Vector2 healthBarCurrentPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        healthBarStartPosition = healthBar.transform.localPosition;
        maxBarWidth = healthBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        float healthPercent = Mathf.Abs((playerHealth.startingHealth - playerHealth.GetHealth()) / playerHealth.startingHealth);
        healthText.GetComponent<TextMeshProUGUI>().text = playerHealth.GetHealth().ToString() + "/" + playerHealth.startingHealth.ToString();
        currentBarWidth = maxBarWidth * (1 - healthPercent);
        healthBar.transform.localScale = new Vector2(currentBarWidth, healthBar.transform.localScale.y);
        healthBar.transform.localPosition = new Vector2(healthBarStartPosition.x - (healthPercent/2) * 100 , healthBarStartPosition.y);
        healthBarCurrentPosition = healthBar.transform.localPosition;
        

    }
}
