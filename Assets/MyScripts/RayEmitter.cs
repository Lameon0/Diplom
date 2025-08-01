using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFromObject : MonoBehaviour
{
    // Ссылка на компонент Line Renderer
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

    // Ссылка на пустой GameObject, из которого будет исходить лазер
    public Transform laserOriginPoint;

    void Update()
    {
        // Получаем позицию пустой точки на объекте
        Vector3 laserStartPosition = laserOriginPoint.position;

        // Устанавливаем начальную точку лазера
        lineRenderer.SetPosition(0, laserStartPosition);

        // Определяем конечную точку лазера (например, на расстоянии 100 единиц от начальной точки в направлении, указанном ориентацией объекта)
        Vector3 laserEndPosition = laserStartPosition + laserOriginPoint.forward * 100f;

        // Создаем луч
        Ray laserRay = new Ray(laserStartPosition, laserOriginPoint.forward);
        RaycastHit hitInfo;

        // Проверяем столкновение луча с объектами на сцене
        if (Physics.Raycast(laserRay, out hitInfo, 100f))
        {
            // Если луч столкнулся с объектом с тегом "Enemy", создаем снаряд
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                //Таймер на задержку выстрела
                if (curTimeBTWshoot <= 0)
                     { 
                // Создаем снаряд 
                ShootProjectile();
                    curTimeBTWshoot = timeBTWshoot;
                     }
            }

            // Устанавливаем конечную точку лазера в точке столкновения луча с объектом
            laserEndPosition = hitInfo.point;
            curTimeBTWshoot = curTimeBTWshoot - Time.deltaTime;
        }

        // Устанавливаем конечную точку лазера
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
