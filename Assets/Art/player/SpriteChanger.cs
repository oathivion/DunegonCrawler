using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] downSprites;    // Array for down-facing frames
    public Sprite[] upSprites;      // Array for up-facing frames
    public Sprite[] leftSprites;    // Array for left-facing frames
    public Sprite[] rightSprites;   // Array for right-facing frames
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private float animationSpeed = 0.15f; // Speed of animation (seconds between frames)
    private int animationIndex = 0; // Current index for animation frames
    private string currentDirection = "down"; // Initial direction (can be set dynamically based on input)
    private bool isKeyPressed = false; // Flag to check if key is being held down
    private bool isAnimating = false; // Flag to check if animation is ongoing

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void Update()
    {
        // Handle key presses and check which direction to animate
        if (Input.GetKey(KeyCode.W)) // Move Up
        {
            currentDirection = "up";
            StartAnimation();
        }
        else if (Input.GetKey(KeyCode.S)) // Move Down
        {
            currentDirection = "down";
            StartAnimation();
        }
        else if (Input.GetKey(KeyCode.A)) // Move Left
        {
            currentDirection = "left";
            StartAnimation();
        }
        else if (Input.GetKey(KeyCode.D)) // Move Right
        {
            currentDirection = "right";
            StartAnimation();
        }
        else
        {
            StopAnimation(); // Stop animation when no key is pressed
        }
    }

    void StartAnimation()
    {
        // Only start animation if it isn't already running
        if (!isAnimating)
        {
            isAnimating = true;
            animationIndex = 0; // Reset animation index to the first frame
            AnimateSprite(); // Start animating immediately
        }
    }

    void StopAnimation()
    {
        // Stop the animation and keep the last frame visible
        isAnimating = false;
    }

    void AnimateSprite()
    {
        if (!isAnimating) return; // Exit if animation is stopped

        // Get the appropriate sprite array based on the current direction
        Sprite[] currentDirectionSprites = GetCurrentDirectionSprites();

        // Set the sprite for the current frame
        spriteRenderer.sprite = currentDirectionSprites[animationIndex];

        // Increment animation index
        animationIndex++;

        // If animation index exceeds the array length, loop back to the first frame
        if (animationIndex >= currentDirectionSprites.Length)
        {
            animationIndex = 0;
        }

        // Wait before switching to the next frame, but only call again if the key is still pressed
        if (isAnimating)
        {
            Invoke("AnimateSprite", animationSpeed); // Call the method again after animationSpeed seconds
        }
    }

    Sprite[] GetCurrentDirectionSprites()
    {
        // Return the appropriate array based on the current direction
        switch (currentDirection)
        {
            case "up":
                return upSprites;
            case "down":
                return downSprites;
            case "left":
                return leftSprites;
            case "right":
                return rightSprites;
            default:
                return downSprites; // Default to down if no direction is set
        }
    }
}
