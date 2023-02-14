using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health, speed, gems;

    [SerializeField]
    protected Transform[] points;
    protected int counter = 1;

    protected Animator animator;
    protected Vector3 _currentTarget;
    protected bool isAlave = true;
    [SerializeField]
    protected Rigidbody2D _rb;
    protected int currentSpeed;
    protected bool canMove = false;

    [SerializeField]
    protected float idleTime, hitTime, attackTime, dethTime, attackRange = 1f;

    [SerializeField]
    protected LayerMask playerLayer;
    [SerializeField]
    protected Transform detect;
    [SerializeField]
    protected float detectRange;

    protected bool playerDetected = false;
    protected bool canAttack = true;
    protected Transform playerTransform;

    [SerializeField]
    private GameObject gemPrefab;
    [SerializeField]
    private Transform gemPoint;
    protected States State
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }
    protected enum States
    {
        Idle = 0,
        Walk = 1,
        Attack = 2,
        Hit = 3,
        Death = 4,
    }
    protected virtual void Update()
    {
        if (!isAlave) return;
        LookingForPlaer();
        UpdateTarget();
        if (canMove) Move();
        Flip();
    }

    protected virtual void Init()
    {
        StartCoroutine(IdleCorutine());
        _currentTarget = points[counter].position;
        animator = GetComponentInChildren<Animator>();
    }
    protected virtual void Flip()
    {
        if (transform.position.x < _currentTarget.x) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    protected virtual void FlipEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(detect.position, transform.position.x < _currentTarget.x ? Vector2.left : Vector2.right, detectRange, playerLayer);
        if (hit != null && hit.collider != null)
        {
            playerTransform = hit.transform;
            playerDetected = true;
        }
    }
    protected virtual void Move()
    {
        State = States.Walk;
        if (canMove) transform.position = Vector2.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

    protected void UpdateTarget()
    {
        if (isAlave)
        {
            if (Vector2.Distance(_currentTarget, transform.position) < 0.3f && !playerDetected)
            {
                canMove = false;
                State = States.Idle;
                StartCoroutine(IdleCorutine());
                counter++;
                if (counter > points.Length - 1)
                {
                    counter = 0;
                }
                _currentTarget = points[counter].position;
            }
            else if (playerDetected)
            {
                _currentTarget = playerTransform.position;
                if (Vector2.Distance(_currentTarget, transform.position) < attackRange)
                {
                    if (canAttack) Attack();
                }
                else
                {
                    animator.SetBool("InCombat", false);
                    canMove = true;
                    State = States.Walk;
                }
                //Vector3 direction = playerTransform.localPosition - transform.localPosition;
            }

            if (playerDetected && Vector2.Distance(_currentTarget, transform.position) > 5f)
            {
                _currentTarget = points[counter].position;
                playerDetected = false;
                animator.SetBool("InCombat", false);
                canMove = true;
                State = States.Walk;
            }
        }
    }

    protected virtual void LookingForPlaer()
    {
        RaycastHit2D hit = Physics2D.Raycast(detect.position, transform.position.x < _currentTarget.x ? Vector2.right : Vector2.left, detectRange, playerLayer);
        if (hit != null && hit.collider != null)
        {
            Debug.Log("Detected");
            playerTransform = hit.transform;
            playerDetected = true;
        }
    }
    protected virtual void Attack()
    {
        canAttack = false;
        canMove = false;
        State = States.Attack;
        animator.SetBool("InCombat", true);
        StartCoroutine(AttackCorutine());
    }
    protected virtual void Hit()
    {
        canMove = false;
        State = States.Hit;
        StartCoroutine(HitCorutine());
    }

    protected virtual void Deth()
    {
        isAlave = false;
        canMove = false;
        State = States.Death;
        StartCoroutine(DethCorutine());
    }

    protected virtual void CreateLoot()
    {
        var spawnPosition = new Vector3(gemPoint.position.x + Random.Range(-0.5f, 0.5f), gemPoint.position.y, gemPoint.position.z);
        var gem = Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        var rb = gem.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 1f));
    }
    protected virtual IEnumerator IdleCorutine()
    {
        yield return new WaitForSeconds(idleTime);
        State = States.Walk;
        canMove = true;
    }

    protected IEnumerator HitCorutine()
    {
        yield return new WaitForSeconds(hitTime);
        if (!playerDetected) FlipEnemy();
        canMove = true;
        State = States.Walk;
    }

    protected IEnumerator AttackCorutine()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("InCombat", false);
        canAttack = true;
        isAlave = true;
    }
    protected IEnumerator DethCorutine()
    {
        yield return new WaitForSeconds(dethTime);
        Destroy(this.gameObject);
    }
}
