using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canAttack = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagble hit = other.GetComponent<IDamagble>();
        if (hit != null && canAttack)
        {
            canAttack = false;
            hit.Damage(1);
            StartCoroutine(attackReset());
        }
    }
    IEnumerator attackReset()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}
