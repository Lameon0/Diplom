using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFromObject : MonoBehaviour
{
    // ������ �� ��������� Line Renderer
    public LineRenderer lineRenderer;
    [SerializeField] Transform shotPoint;
    [SerializeField] Transform shotPoint1;
    [SerializeField] Transform shotPoint2;
    [SerializeField] Transform shotPoint3;
    [SerializeField] Transform shotPoint4;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float timeBTWshoot;
    [SerializeField] float curTimeBTWshoot;

    // ������ �� ������ GameObject, �� �������� ����� �������� �����
    public Transform laserOriginPoint;

    void Update()
    {
        // �������� ������� ������ ����� �� �������
        Vector3 laserStartPosition = laserOriginPoint.position;

        // ������������� ��������� ����� ������
        lineRenderer.SetPosition(0, laserStartPosition);

        // ���������� �������� ����� ������ (��������, �� ���������� 100 ������ �� ��������� ����� � �����������, ��������� ����������� �������)
        Vector3 laserEndPosition = laserStartPosition + laserOriginPoint.forward * 100f;

        // ������� ���
        Ray laserRay = new Ray(laserStartPosition, laserOriginPoint.forward);
        RaycastHit hitInfo;

        // ��������� ������������ ���� � ��������� �� �����
        if (Physics.Raycast(laserRay, out hitInfo, 100f))
        {
            // ���� ��� ���������� � �������� � ����� "Enemy", ������� ������
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                //������ �� �������� ��������
                if (curTimeBTWshoot <= 0)
                     { 
                // ������� ������ 
                ShootProjectile();
                    curTimeBTWshoot = timeBTWshoot;
                     }
            }

            // ������������� �������� ����� ������ � ����� ������������ ���� � ��������
            laserEndPosition = hitInfo.point;
            curTimeBTWshoot = curTimeBTWshoot - Time.deltaTime;
        }

        // ������������� �������� ����� ������
        lineRenderer.SetPosition(1, laserEndPosition);
    }
    void ShootProjectile()
    {
        Debug.Log("shooted");
        Instantiate(projectilePrefab, shotPoint.position, shotPoint.rotation);
        if (shotPoint1 != null)
        {
            Instantiate(projectilePrefab, shotPoint1.position, shotPoint.rotation);
        }
        if (shotPoint2 != null)
        {
            Instantiate(projectilePrefab, shotPoint2.position, shotPoint.rotation);
        }
        if (shotPoint3 != null)
        {
            Instantiate(projectilePrefab, shotPoint3.position, shotPoint.rotation);
        }
        if (shotPoint4 != null)
        {
            Instantiate(projectilePrefab, shotPoint4.position, shotPoint.rotation);
        }
    }
}
