using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform gunBarrel;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float StartTimeBtwAtt = 1.5f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private Transform AttackAreaPoint;
    [SerializeField] private GameObject HitPrefab;

    private Quaternion initialRotation;
    private bool isObstacleDetected;
    private float CurTimeBtwAtt;

    private void Start()
    {
        initialRotation = gunBarrel.rotation;
        CurTimeBtwAtt = StartTimeBtwAtt;
        FindPlayer();
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            FindPlayer();
            if (playerTransform == null) return;
        }

        DetectObstacle();

        if (!IsPlayerVisible())
        {
            Rotate180Degrees();
        }
        else
        {
            LookAtPlayer();
            TryShoot();
        }
    }

    private bool IsPlayerVisible()
    {
        if (playerTransform == null) return false;

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, 50, obstacleMask))
        {
            // ѕровер€ем, €вл€етс€ ли преп€тствие стеной
            if (hit.collider.CompareTag("Wall"))
            {
                // ѕреп€тствие между врагом и игроком, игрок не видим
                return false;
            }
        }

        // Ќет преп€тствий, игрок видим
        return true;
    }
    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    private void DetectObstacle()
    {
        if (playerTransform == null) return;

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, 50, obstacleMask))
        {
            isObstacleDetected = !hit.collider.CompareTag("Player");
        }
        else
        {
            isObstacleDetected = false;
        }
    }

    private void Rotate180Degrees()
    {
        Quaternion targetRotation = gunBarrel.rotation * Quaternion.Euler(0, 180, 0);
        gunBarrel.rotation = Quaternion.Lerp(gunBarrel.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        if (playerTransform == null) return;

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        gunBarrel.rotation = Quaternion.Slerp(gunBarrel.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void TryShoot()
    {
        CurTimeBtwAtt -= Time.deltaTime;

        if (CurTimeBtwAtt <= 0)
        {
            Shoot();
            CurTimeBtwAtt = StartTimeBtwAtt;
        }
    }

    private void Shoot()
    {
        if (firePoint != null && bulletPrefab != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        if (HitPrefab != null && AttackAreaPoint != null)
        {
            Instantiate(HitPrefab, AttackAreaPoint.position, AttackAreaPoint.rotation);
        }
    }

}
