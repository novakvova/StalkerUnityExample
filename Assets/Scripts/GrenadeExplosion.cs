using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    private void Start()
    {

        StartCoroutine("Explosion");
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f);
        foreach(var objects in colliders)
        {
            Rigidbody rb = objects.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(100000f, transform.position, 100f);
            }
        }
        Destroy(this.gameObject);
    }
}
