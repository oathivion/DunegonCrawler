using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float facingWhatDirection; // Change to float to avoid precision issues
    private Vector2 facingWhatVector; //Variable used For Calculations
    [SerializeField] private float jabDistance; // How Far to Jab
    [SerializeField] private float jabTime; //How long to jab
    [SerializeField] private float damage; // How Much Damage Per Jab
    [SerializeField] private string whoShouldIHitTag; //What Tag should I check for when compairing damage
    [SerializeField] private string whoShouldIIngnoreTag; //Who should I ignore.

    void Update () {
        if (Input.GetKey(KeyCode.A)) { //Left
            facingWhatDirection = -90f;
            RotateSword(facingWhatDirection);
        }
        if (Input.GetKey(KeyCode.D)) { //Right
            facingWhatDirection = 90f;
            RotateSword(facingWhatDirection);
        }
        if (Input.GetKey(KeyCode.W)) { //Up
            facingWhatDirection = 180f;
            RotateSword(facingWhatDirection);
        }
        if (Input.GetKey(KeyCode.S)) { //Down
            facingWhatDirection = 0f;
            RotateSword(facingWhatDirection);
        }
        if (Input.GetMouseButtonDown(0)) {
            JabSword();
        }
    }

    private void RotateSword (float rotateDir) { //Rotates the sword to face the right direction;
        transform.rotation = Quaternion.Euler(0, 0, rotateDir);
    }

    private void JabSword () {
        facingWhatVector = ConvertFacingWhatDirectionToVector2(facingWhatDirection);
        StartCoroutine(JabSwordMovement());
    }

    private IEnumerator JabSwordMovement() {
    Vector3 startPos = transform.position;  // Store the starting position
    Vector3 endPos = startPos + (Vector3)facingWhatVector * jabDistance;  // Target position
    float elapsedTime = 0;

    // Move the sword forward over time
    while (elapsedTime < jabTime) {
        transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / jabTime);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure the sword reaches the end position
    transform.position = endPos;

    // After the jab is complete, reset the sword to its original position
    yield return new WaitForSeconds(0.1f);  // Optional small delay before resetting
    transform.position = startPos;  // Reset position
}


    private Vector2 ConvertFacingWhatDirectionToVector2 (float facingWhatDirection) {
        switch (facingWhatDirection) {
            case 0f:
                return Vector2.down;
            case -90f:
                return Vector2.left;
            case 90f:
                return Vector2.right;
            case 180f:
                return Vector2.up;
            default:
                Debug.LogWarning("FacingWhatDirection Not Properly Set, Returning Vector2.zero");
                return Vector2.zero;
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.CompareTag(whoShouldIHitTag)) {
            other.GetComponent<HealthScript>().TakeDamage(damage);
        }
        else if(other.CompareTag(whoShouldIIngnoreTag)) {
            return;
        }
        else {
            Destroy(gameObject);
        }
    } 
}
