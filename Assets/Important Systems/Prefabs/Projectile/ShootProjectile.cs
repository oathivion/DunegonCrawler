using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; //What to shoot?
    [SerializeField] private Transform shootTransfrom; //Where to shoot from?

    void Update(){
        if(Input.GetMouseButtonDown(1)) {
            Shoot();
        }
    }

    void Shoot() {
        // Get mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Get direction from player to click location
        Vector2 shootDirection = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        float shootAngle = Mathf.Atan2(shootDirection.x, shootDirection.y) * Mathf.Rad2Deg;

        //Creates The Projectile and applies rotation.
        GameObject projectile = Instantiate(projectilePrefab, shootTransfrom.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(shootDirection);
        
    }
}
