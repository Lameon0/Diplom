using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float lifespan = 0.3f; // Время жизни снаряда
    [SerializeField] float damage = 4f; // Урон, который наносит снаряд
    [SerializeField] float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Получаем компонент Enemyscript у врага
            PlayerLogic Player = other.GetComponent<PlayerLogic>();
            if (Player != null)
            {
                // Уменьшаем здоровье игрока на количество урона
                Player.Hp -= damage;
                // Уничтожаем снаряд после попадания
                Destroy(gameObject);
                // Проверяем, достигло ли здоровье врага нуля
                if (Player.Hp <= 0)
                {
                    // Если да, вызываем метод смерти игрока
                    Player.GameOver();
                }
            }

        }
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // Неприрывное движение атаки вперёд
        lifespan -= 1 * Time.deltaTime;
        if (lifespan <= 0)  // уничтожение атаки по истечению времени жизни
        {
            Destroy(gameObject);
        }
    }
}
