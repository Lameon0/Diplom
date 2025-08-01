using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class Enemyscript : MonoBehaviour
{

    [SerializeField] float attackRange = 2f; // ���������� ��� �����
    [SerializeField] public float HP = 2f;
    [SerializeField] float MaxHP = 10f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform AttackAreaPoint;
    [SerializeField] GameObject HitPrefab;
    public bool IsAttack;
    [SerializeField] float StartTimeBtwAtt = 1.5f;
    [SerializeField] float CurTimeBtwAtt = 1.5f;
    [SerializeField] Transform PlayerPos;
    private NavMeshAgent navMeshAgent;
    [SerializeField] float backingDistance;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject explosionPrefab;
    public bool isDead = false;
    [SerializeField] public bool IsBoss;
    [SerializeField] float avoidanceDistance = 2f;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float desiredDistanceToPlayer = 5f;
    private bool avoidingPlayer = false;
    public float minDistanceToPlayer = 2f;
    public bool isRunning =false;
    private bool isObstacleDetected;

    // Start is called before the first frame update
    void Start()
    {

        HP = MaxHP;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // ���������, ��� �� ������ ������ � ����� "Player"
        if (playerObject != null)
        {
            PlayerPos = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
        Death();
        Attack();
        PlayerTracking();
    }

    public void PlayerTracking()
    {
            if (IsBoss == false)
            {
            if (!IsPlayerVisible())
            { return; }
                if (PlayerPos != null)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, PlayerPos.position);

                   
                        // ���� ���������� �� ������ ������ ��������� ����� ���� �������� ��������� � ������
                        if (distanceToPlayer > attackRange)
                        {
                            navMeshAgent.SetDestination(PlayerPos.position);
                            // ������������ ����� ���, ����� �� ������� �� ������
                            transform.LookAt(PlayerPos);
                        }

                        else
                        {
                            navMeshAgent.ResetPath();
                        }
                    }
                

            }
            else
            {
            if (!IsPlayerVisible())
            { return; }
            if (PlayerPos != null)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, PlayerPos.position);

                    // ���������, ���������� �� ������ ���� � ������
                    if (distanceToPlayer > desiredDistanceToPlayer && !avoidingPlayer)
                    {
                        // ��������� ����� ��������� � ������
                        navMeshAgent.SetDestination(PlayerPos.position);
                        isRunning = true;
                    }
                    else
                    {
                        // ���� ��������� ���������� ������ � ������ ��� ����� ��������, ������������� ��������
                        navMeshAgent.ResetPath();
                        isRunning = false;
                        // ���������, ����� �� ������ �������� �� ������
                        if (distanceToPlayer < minDistanceToPlayer)
                        {
                            avoidingPlayer = true;

                            // ���������� �����, � ������� ����� ������ �� ������
                            Vector3 avoidDirection = (transform.position - PlayerPos.position).normalized;
                            Vector3 avoidPoint = transform.position + avoidDirection * avoidanceDistance;

                            // ��������� ����� ��������� � ����� ���������
                            navMeshAgent.SetDestination(avoidPoint);
                            isRunning = true;
                        }
                        else if (distanceToPlayer >= desiredDistanceToPlayer)
                        {
                            // ���� ���������� ������ �� ������, ���������� ���� ���������
                            avoidingPlayer = false;
                            isRunning = false;
                        }
                    }

                    // ���������, ���� �� ������� �� ���� �����
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, PlayerPos.position - transform.position, out hit, Mathf.Infinity, wallLayer))
                    {
                        // ���� ����, ������� ����� ��������� � ��������� ����� ��������� � ���
                        Vector3 avoidancePoint = hit.point + hit.normal * avoidanceDistance;
                        navMeshAgent.SetDestination(avoidancePoint);
                    }

                    // ������������ ����� ���, ����� �� ������� �� ������
                    transform.LookAt(PlayerPos);
                }
            }
        }




    private bool IsPlayerVisible()
    {
        if (PlayerPos == null) return false;

        Vector3 directionToPlayer = PlayerPos.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, 50, wallLayer))
        {
            // ���������, �������� �� ����������� ������
            if (hit.collider.CompareTag("Wall"))
            {
                // ����������� ����� ������ � �������, ����� �� �����
                return false;
            }
        }

        // ��� �����������, ����� �����
        return true;
    }
    public void Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerPos.position);
        if (isObstacleDetected == false)
        {
            if (IsBoss == false)
            {
                CurTimeBtwAtt -= 1 * Time.deltaTime;

                if (CurTimeBtwAtt <= 0 && distanceToPlayer <= attackRange)
                {
                    IsAttack = true;
                    Instantiate(HitPrefab, AttackAreaPoint.position, AttackAreaPoint.rotation);
                    CurTimeBtwAtt = StartTimeBtwAtt;
                }
                else
                {
                    IsAttack = false;
                }
            }
            else
            {
                CurTimeBtwAtt -= 1 * Time.deltaTime;

                if (CurTimeBtwAtt <= 0)
                {
                    IsAttack = true;
                    Instantiate(HitPrefab, AttackAreaPoint.position, AttackAreaPoint.rotation);
                    CurTimeBtwAtt = StartTimeBtwAtt;
                }
                else
                {
                    IsAttack = false;
                }
            }
        }
    }
    public void Death()
    {
        if (!isDead && HP <= 0)
        {
            isDead = true;

            // ����������� �������� ������, ���� ���� ��������
            if (animator != null)
            {
                animator.SetTrigger("Death");
            }

            // ������� �����
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // ������� ����� ����� ������� �������
            Invoke("DestroyEnemy", 1.5f);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
