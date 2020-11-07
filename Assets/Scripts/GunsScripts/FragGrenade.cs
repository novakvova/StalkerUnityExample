using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FragGrenade : MonoBehaviour
{
    public int damage;
    public float delay;

    public float range;
    public float explosionForce;

    private float countdown;

    private bool hasExplode = false;

    public GameObject ExplosionPrefab;
    public AudioClip ExplosionSound;

    public bool IsPickUp;
    public bool explode;

    private void Start()
    {
        damage /= 2;
        countdown = delay;
    }

    public void Update()
    {
        if (!IsPickUp)
            countdown -= Time.deltaTime;

        if (explode && !hasExplode)
        {
            GetComponent<AudioSource>().PlayOneShot(ExplosionSound);
            Explode();
            hasExplode = true;
        }

        if (countdown <= 0f && !hasExplode)
        {
            GetComponent<AudioSource>().PlayOneShot(ExplosionSound);
            Explode();
            hasExplode = true;
        } 
    }


    void Explode()
    {
        GetComponent<AudioSource>().PlayOneShot(ExplosionSound);
        GameObject g = Instantiate(ExplosionPrefab, transform.position, transform.rotation);

        Destroy(g, 3f);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, range);

        foreach (Collider destroyObject in collidersToDestroy)
        {
            Destructible d = destroyObject.GetComponent<Destructible>();

            if (d != null)
            {
                d.DestroyGM();
            }
        }

        Collider[] collidersToAddForece = Physics.OverlapSphere(transform.position, range);

        foreach(Collider nearbyObject in collidersToAddForece)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();          

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, range);
            }
        }

        Collider[] collidersToChangeHealth = Physics.OverlapSphere(transform.position, range);

        foreach (Collider nearbyObject in collidersToChangeHealth)
        {
           PlayerManager[] pm = nearbyObject.gameObject.GetComponentsInChildren<PlayerManager>();

            foreach (var a in pm)
            {
                if (a == null)
                    continue;

                a.MinusHealth(damage);             
            }

        }

        foreach (Collider nearbyObject in collidersToChangeHealth)
        {
            FragGrenade fr = nearbyObject.gameObject.GetComponentInChildren<FragGrenade>();

            if (fr != null)
            {
                fr.explode = true;
            }

        }

        foreach (Collider nearbyObject in collidersToChangeHealth)
        {
            EnemyStats es = nearbyObject.GetComponent<EnemyStats>();

            if (es != null)
            {
                es.Damage(damage);
            }

        }

        

        Destroy(gameObject,1f);
    }
}
