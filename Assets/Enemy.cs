using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 1;


    public float sightRange = 10f;

    public float attackRange = 5f;

    public float stopDistance = 8f;


    public Transform target;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private float projectileSpeed = 10f;
    private float shootingCooldown = 2f;

    public int attackDamage = 1;

    [SerializeField] private GameObject muzzleFlashPrefab;


    [SerializeField] private Transform flashPoint;



    private const string IdleAnimation = "Idle_Guard_AR";
    private const string RunAnimation = "Run_gunMiddle_AR";
    private const string ShootAnimation = "Shoot_SingleShot_AR";
    private const string DieAnimation = "Die";
    private const string BurstShootAnimation = "Shoot_BurstShot_AR";

    private Animator animator;
    private NavMeshAgent agent;
    private enum State { Idle, Search, Attack, Die,  } 
    private State currentState = State.Idle;

    private float lastShotTime;
    public float patrolRadius = 20f;

    private Vector3 startingPosition;
    private bool isDying = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stopDistance;

        lastShotTime = Time.time - shootingCooldown;
        startingPosition = transform.position;

        ChangeState(State.Search); 
    }

    void Update()
    {

    



        if (currentState == State.Die || health <= 0)
        {
            PerformDie();
            return;
        }

        switch (currentState)
        {
            case State.Idle:
                PerformIdle();
                break;
            case State.Search:
                PerformSearch();
                break;


            case State.Attack:
                PerformAttack();
                break;
           
        }
    }

    private void PerformIdle()
    {
        SetAnimation(IdleAnimation);
        if (CanSeeTarget())
            ChangeState(State.Search);
    }

    private void PerformSearch()
    {
        SetAnimation(RunAnimation);

        if (IsTargetInRange())
        {
            ChangeState(State.Attack);
        }
        else
        {
            PatrolRandomly(); 
        }

        if (CanSeeTarget())
        {
            agent.SetDestination(target.position); 
        }
    }

    private void PatrolRandomly()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not active or not on a NavMesh");
            return;
        }

        if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance))
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += startingPosition;
            NavMeshHit hit;


            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    private void PerformAttack()
    {
        if (!IsTargetInRange())
            ChangeState(State.Search);
        else
        {
            agent.isStopped = true;


            FaceTarget();
            if (Time.time > lastShotTime + shootingCooldown)
                AttackTarget();
        }
    }

    private void PerformDie()
    {
        if (!isDying)
        {
            SetAnimation(DieAnimation);


            agent.enabled = false;
            this.enabled = false;

            currentState = State.Die;

            isDying = true;
            Invoke("DestroyEnemy", 1.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
        agent.isStopped = (newState == State.Attack);
    }

    private void SetAnimation(string stateName)
    {
        animator.Play(stateName);
    }

    private void AttackTarget()
    {
        if (target == null)
        {
           
            return;
        }

        Vector3 rayDirection = (target.position - transform.position).normalized;
        FaceTarget(); 

        if (Time.time > lastShotTime + shootingCooldown)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, attackRange))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    HandlePlayerHit(hit.collider.gameObject);
                    ShowMuzzleFlash();
                }
            }

          
            lastShotTime = Time.time;
            SetAnimation(ShootAnimation);
        }
    }

    private void ShowMuzzleFlash()
    {
        if (muzzleFlashPrefab != null && flashPoint != null)
        {
            var muzzleFlashInstance = Instantiate(muzzleFlashPrefab, flashPoint.position, flashPoint.rotation);
            Destroy(muzzleFlashInstance, 0.5f); 
        }
    }

    private void HandlePlayerHit(GameObject player)
    {
        HealthSystem healthSystem = player.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(attackDamage);
           
        }
        else
        {
            
        }
    }

    void FaceTarget()
    {
        Vector3 targetPosition = target.position;
        FaceTargetPosition(targetPosition);
    }

    void FaceTargetPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    bool CanSeeTarget()
    {
        return target != null && Vector3.Distance(transform.position, target.position) < sightRange;
    }

    bool IsTargetInRange()
    {
        return target != null && Vector3.Distance(transform.position, target.position) <= attackRange;
    }

    public void TakeDamage(int damage)
     {
         health -= damage;
     } 



    public void MoveToFormationPosition(Vector3 position)
    {
        agent.destination = position;
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }



    //enemy cover transition







}
