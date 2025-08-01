using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectyle : MonoBehaviour
{
    [SerializeField] float speed = 10f; // Скорость снаряда
    [SerializeField] float lifespan = 3f; // Время жизни снаряда
    [SerializeField] int damage = 1; // Урон, который наносит снаряд

    private float spawnTime; // Время создания снаряда

    private void Start()
    {
        // Записываем время создания снаряда
        spawnTime = Time.time;
    }

    private void Update()
    {
        // Перемещаем снаряд вперед
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Проверяем, истекло ли время жизни снаряда
        if (Time.time - spawnTime >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулся ли снаряд с объектом с тегом "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Получаем компонент Enemyscript у врага
            Enemyscript enemy = other.GetComponent<Enemyscript>();
            if (enemy != null)
            {
                // Уменьшаем здоровье врага на количество урона
                enemy.HP -= damage;

                // Проверяем, достигло ли здоровье врага нуля
                if (enemy.HP <= 0)
                {
                    // Если да, вызываем метод смерти врага
                    enemy.Death();
                }
            }

            // Уничтожаем снаряд после попадания
            Destroy(gameObject);
        }
    }
}
