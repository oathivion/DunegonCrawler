using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites; // Array to hold your 4 sprites
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Change sprite on '1' key
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Change sprite on '2' key
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Change sprite on '3' key
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Change sprite on '4' key
        {
            spriteRenderer.sprite = sprites[3];
        }
    }
}
