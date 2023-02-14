using UnityEngine;

public class AcidEffect : MonoBehaviour, IDamagble
{
    public int Health { get; set; }
    private void Start()
    {
        Health = 1;
        Destroy(this.gameObject, 4f);
    }

    public void Damage(int damageAmount)
    {
        Health--;
        if (Health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
