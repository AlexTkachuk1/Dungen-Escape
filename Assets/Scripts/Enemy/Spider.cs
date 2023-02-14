using UnityEngine;

public class Spider : Enemy, IDamagble
{
    public int Health { get; set; }
    [SerializeField]
    private GameObject acidPrefab;
    [SerializeField]
    private Transform fierPoint;
    private void Start()
    {
        Init();
        Health = base.health;
    }

    public void Damage(int damageAmount)
    {
        Hit();
        Health--;
        if (Health < 1)
        {
            Deth();
            for (var index = 0; index < gems; index++)
            {
                CreateLoot();
            }
        }
    }

    public void CreateAcidBoll()
    {
        var acid = Instantiate(acidPrefab, fierPoint.position, Quaternion.identity);
        var rb = acid.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(transform.position.x < _currentTarget.x ? 4.5f : -4.5f, rb.velocity.y);
    }
}
