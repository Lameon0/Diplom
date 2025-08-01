using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
    public class ShootProjectileOnHover : MonoBehaviour
    {

    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;

    void Update()
    {
        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out RaycastHit hit, Mathf.Infinity, enemyLayer))
        {
            Debug.Log("detected");
            if (hit.collider.CompareTag("Enemy"))
            {
                ShootProjectile();
            }
        }
    }

    void ShootProjectile()
    {
        Debug.Log("shooted");
        Instantiate(projectilePrefab, shotPoint.position, shotPoint.rotation);
    }
}

