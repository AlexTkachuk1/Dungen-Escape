using System.Collections;
using UnityEngine;

public class MossGiant : Enemy, IDamagble
{
    public int Health { get; set; }
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
}
