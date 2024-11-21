using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; //What to shoot?
    [SerializeField] private Transform shootTransfrom; //Where to shoot from?
    [SerializeField] private bool IsEnemy = false;

    void Update(){
        
        if(Input.GetMouseButtonDown(1) && !IsEnemy) {
            Shoot();
        }
    }

    public void Shoot() {
        // Get mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootAt(mousePosition);
    }

    public Projectile shootAt(Vector2 position)
    {
        // Get mouse position in world coordinates

        //Get direction from player to click location
        Vector2 shootDirection = (position - new Vector2(transform.position.x, transform.position.y)).normalized;
        float shootAngle = Mathf.Atan2(shootDirection.x, shootDirection.y) * Mathf.Rad2Deg;

        //Creates The Projectile and applies rotation.
        GameObject projectile = Instantiate(projectilePrefab, shootTransfrom.position, Quaternion.identity);
        Projectile _projectile = projectile.GetComponent<Projectile>();
        _projectile.SetDirection(shootDirection);

        return _projectile;
    }
}
