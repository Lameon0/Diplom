using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    // Строки здоровья
    [SerializeField] public float MaxHp;
    [SerializeField] public float Hp;
    //Колайдер, вообще это зомби код
    [SerializeField] public CapsuleCollider PlayerHitbox;
    //Скорость
    [SerializeField] private float moveSpeed = 3.0f;
    //Это надо для переписанного управления
    [SerializeField] private Transform leftHandAnchor;
    public CharacterController characterController;
    public SteamVR_Action_Vector2 moveAction;
    //Это для скрипта который колайдер игрока уменьшает если тот приседает
    [SerializeField] Camera steamVRCamera;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Transform vrCamera;
    // Это для скрипта смерти
    [SerializeField] private Image screenOverlay;
    [SerializeField] private Transform respawnPoint;
    // Это для костыля
    [SerializeField] private GameObject targetObject; 

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Hp = MaxHp;
        capsuleCollider.height = vrCamera.position.y;
        ToggleObject(); // Сам костыль
    }

    // Update is called once per frame
    void Update()
    {
        Regeneration();
        GameOver();
        Moving();
        UpdateColliderHeight();
    }

    public void Regeneration()
    {
        if (Hp < MaxHp)
        {
            Hp += 1 * Time.deltaTime;
        }
    }
    public void UpdateColliderHeight() // Скрипт изменения колайдера во время приседания в реальном мире
    {
        if (capsuleCollider != null && vrCamera != null)
        {
            capsuleCollider.height = vrCamera.position.y;
        }
    }
    public void GameOver() //Скрипт смерти
    {
        if (Hp <= 0)
        {
            StartCoroutine(DarkenScreenAndTeleport());
        }
    }
    public void ToggleObject() // Костыль
    {
        if (targetObject != null)
        {
            StartCoroutine(ToggleObjectCoroutine());
        }
        else
        {
            Debug.LogWarning("Target object is not set!");
        }
    }

    // Корутин для выключения и включения объекта с задержкой (костыля)
    private IEnumerator ToggleObjectCoroutine()
    {
        yield return new WaitForSeconds(2f);
        // Выключаем объект
        targetObject.SetActive(false);
        Debug.Log("Объект выключен");
        // Включаем объект снова
        targetObject.SetActive(true);
        Debug.Log("Объект включен");
    }
    private IEnumerator DarkenScreenAndTeleport() //Телепорт + затеменение экрана при смерти
    {
        Color initialColor = screenOverlay.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1); // Full opacity
        float duration = 2.0f; // Duration of the screen darkening
        float elapsedTime = 0;

        // Gradually darken the screenyE
        while (elapsedTime < duration)
        {
            screenOverlay.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the screen is fully darkened
        screenOverlay.color = targetColor;

        // Call the teleport method
        TeleportToRespawnPoint();
    }
    private IEnumerator FadeInScreen() // само затемнение
    {
        Color initialColor = screenOverlay.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Full transparency
        float duration = 2.0f; // Duration of the screen fading in
        float elapsedTime = 0;

        // Gradually fade the screen back in
        while (elapsedTime < duration)
        {
            screenOverlay.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the screen is fully transparent
        screenOverlay.color = targetColor;
    }
    private void TeleportToRespawnPoint() // Этот метод при смерти используется
    {
        if (respawnPoint != null)
        {
            this.transform.position = respawnPoint.transform.position;
        }

        // Reset HP or any other necessary states
        Hp = MaxHp;

        // Optionally, fade the screen back in
        StartCoroutine(FadeInScreen());
    }
            private void Moving() // Переписанное управление
    {
        // Получаем направление взгляда игрока
        Vector3 forwardDirection = steamVRCamera.transform.forward;

        Vector2 input = moveAction.GetAxis(SteamVR_Input_Sources.Any);

        // Получаем компоненты движения по осям X и Z
        float horizontalMovement = input.x;
        float verticalMovement = input.y;

        // Вычисляем направление движения
        Vector3 movementDirection = new Vector3(horizontalMovement, 0.0f, verticalMovement);

        // Преобразуем направление движения в мировое пространство
        movementDirection = steamVRCamera.transform.TransformDirection(movementDirection);
        movementDirection.y = 0.0f;

        // Добавляем гравитацию
        if (!characterController.isGrounded)
        {
            // Учитываем гравитацию, если игрок не на земле
            movementDirection.y -= 20f * Time.deltaTime;
        }

        // Перемещаем игрока
        characterController.Move(movementDirection * moveSpeed * Time.deltaTime);
    }
}
