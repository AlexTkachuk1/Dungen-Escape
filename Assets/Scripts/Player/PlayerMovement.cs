using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamagble
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _speed = 20f, _jumpforse = 10f, checkRadius;

    [SerializeField]
    private int health = 1000;

    [SerializeField]
    private Transform _groundChecker, attackPoint;

    [SerializeField]
    private Animator _animator, _swordAnimator;

    [SerializeField]
    private LayerMask _groundMask, enemyLaers;

    private bool _isOnGround, _canAttack = true;
    private float inputX;
    private int attackDirection = 1;
    public int Health { get; set; }

    private bool isAlive = true;
    private void Start()
    {
        Health = health;
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            _rb.velocity = new Vector2(inputX * _speed, _rb.velocity.y);
        }
    }
    private void Update()
    {
        if (isAlive)
        {
            _isOnGround = Physics2D.OverlapCircle(_groundChecker.position, checkRadius, _groundMask);

            Move();
            Jump();
            Attack();
            Flip();

            _animator.SetBool("Run", inputX != 0 && _isOnGround);
            _animator.SetBool("Jump", !_isOnGround);
        }
    }

    private void Move()
    {
        inputX = Input.GetAxis("Horizontal");
        if (inputX != 0) attackDirection = inputX > 0 ? 1 : -1;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _animator.SetBool("Jump", true);
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpforse);
        }
    }
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && _isOnGround && _canAttack)
        {
            _canAttack = false;
            StartCoroutine(AttackReload());
            _animator.SetTrigger("Attack");
            _swordAnimator.SetTrigger("SwordAnim");
        }
    }
    IEnumerator AttackReload()
    {
        yield return new WaitForSeconds(1f);
        _canAttack = true;
    }
    private void Flip()
    {
        if (inputX > 0 && transform.eulerAngles.y != 0) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (inputX < 0 && transform.eulerAngles.y != 180) transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }


    public void Damage(int damageAmount)
    {
        Health--;
        _animator.SetTrigger("Hit");
        if (Health < 1)
        {
            isAlive = false;
            _animator.SetBool("Death", true);
        }
    }
}
