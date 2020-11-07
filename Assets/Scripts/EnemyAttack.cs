using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float delay;

    private bool CanAttack;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (CanAttack)
        {
            CanAttack = false;

            PlayerManager[] pm = collision.transform.GetComponentsInChildren<PlayerManager>();

            foreach (var a in pm)
            {
                if (a == null)
                    continue;


                a.MinusHealth(damage);
            }
        }
    }
    
    IEnumerator Delay()
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            CanAttack = true;
            yield return new WaitForSeconds(delay);
            CanAttack = false;
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("KillsCount").GetComponent<KillsCountScript>().AddKill();
    }
}
