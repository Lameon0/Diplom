using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    // ������ ��������
    [SerializeField] public float MaxHp;
    [SerializeField] public float Hp;
    //��������, ������ ��� ����� ���
    [SerializeField] public CapsuleCollider PlayerHitbox;
    //��������
    [SerializeField] private float moveSpeed = 3.0f;
    //��� ���� ��� ������������� ����������
    [SerializeField] private Transform leftHandAnchor;
    public CharacterController characterController;
    public SteamVR_Action_Vector2 moveAction;
    //��� ��� ������� ������� �������� ������ ��������� ���� ��� ���������
    [SerializeField] Camera steamVRCamera;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Transform vrCamera;
    // ��� ��� ������� ������
    [SerializeField] private Image screenOverlay;
    [SerializeField] private Transform respawnPoint;
    // ��� ��� �������
    [SerializeField] private GameObject targetObject; 

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Hp = MaxHp;
        capsuleCollider.height = vrCamera.position.y;
        ToggleObject(); // ��� �������
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
    public void UpdateColliderHeight() // ������ ��������� ��������� �� ����� ���������� � �������� ����
    {
        if (capsuleCollider != null && vrCamera != null)
        {
            capsuleCollider.height = vrCamera.position.y;
        }
    }
    public void GameOver() //������ ������
    {
        if (Hp <= 0)
        {
            StartCoroutine(DarkenScreenAndTeleport());
        }
    }
    public void ToggleObject() // �������
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

    // ������� ��� ���������� � ��������� ������� � ��������� (�������)
    private IEnumerator ToggleObjectCoroutine()
    {
        yield return new WaitForSeconds(2f);
        // ��������� ������
        targetObject.SetActive(false);
        Debug.Log("������ ��������");
        // �������� ������ �����
        targetObject.SetActive(true);
        Debug.Log("������ �������");
    }
    private IEnumerator DarkenScreenAndTeleport() //�������� + ����������� ������ ��� ������
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
    private IEnumerator FadeInScreen() // ���� ����������
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
    private void TeleportToRespawnPoint() // ���� ����� ��� ������ ������������
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
            private void Moving() // ������������ ����������
    {
        // �������� ����������� ������� ������
        Vector3 forwardDirection = steamVRCamera.transform.forward;

        Vector2 input = moveAction.GetAxis(SteamVR_Input_Sources.Any);

        // �������� ���������� �������� �� ���� X � Z
        float horizontalMovement = input.x;
        float verticalMovement = input.y;

        // ��������� ����������� ��������
        Vector3 movementDirection = new Vector3(horizontalMovement, 0.0f, verticalMovement);

        // ����������� ����������� �������� � ������� ������������
        movementDirection = steamVRCamera.transform.TransformDirection(movementDirection);
        movementDirection.y = 0.0f;

        // ��������� ����������
        if (!characterController.isGrounded)
        {
            // ��������� ����������, ���� ����� �� �� �����
            movementDirection.y -= 20f * Time.deltaTime;
        }

        // ���������� ������
        characterController.Move(movementDirection * moveSpeed * Time.deltaTime);
    }
}
