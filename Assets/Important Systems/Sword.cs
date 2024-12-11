using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector2 facingDirection = Vector2.down; // Default facing direction
    private Collider2D swordCollider;
    private bool isJabbing;

    [SerializeField] private float jabDistance = 1f; // How far to jab
    [SerializeField] private float jabTime = 0.2f; // How long to jab
    [SerializeField] private float damage = 10f; // Damage per jab
    [SerializeField] private string targetTag = "Enemy"; // Tag to apply damage
    [SerializeField] private string ignoreTag = "Player"; // Tag to ignore
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
        isJabbing = false;
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        HandleInput();
        if (Input.GetMouseButtonDown(0) && !isJabbing)
        {
            StartCoroutine(JabSword());
        }
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W)) facingDirection = Vector2.up;
        else if (Input.GetKey(KeyCode.S)) facingDirection = Vector2.down;
        else if (Input.GetKey(KeyCode.A)) facingDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.D)) facingDirection = Vector2.right;

        RotateSword();
    }

    private void RotateSword()
    {
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // Adjust by -90 degrees
    }


    private IEnumerator JabSword()
    {
        isJabbing = true;
        spriteRenderer.enabled = true;
        swordCollider.enabled = true;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + (Vector3)facingDirection * jabDistance;

        float elapsedTime = 0;
        while (elapsedTime < jabTime)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / jabTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
        swordCollider.enabled = false;
        isJabbing = false;
        spriteRenderer.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            
            if (other.gameObject.GetComponent<HealthScript>() != null)
            {
                var health = other.GetComponent<HealthScript>();
                health?.TakeDamage(damage);
            }
            else if (other.gameObject.GetComponent<EnemyFollow>() !=null)
            {
                
                other.gameObject.GetComponent<EnemyFollow>().TakeDamage(damage);
            }
            
        }
        else if (other.CompareTag(ignoreTag))
        {
            return;
        }
    }
}
