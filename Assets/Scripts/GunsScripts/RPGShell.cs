using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class RPGShell : MonoBehaviour
{
    public GameObject Particles;
    public GameObject ExplosionEffect;

    public int damage;
    public float range;
    public float ExplosionForce;

    public AudioClip ExplosionClip;

    public Transform ParticlesPlace;

    private bool HasExplode = false;
    [HideInInspector]public bool Missile = false;

    private Rigidbody rb;

    public Transform CenterOfMass;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CenterOfMass.position;

    }


    public void Update()
    {
        if (Missile)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(rb.velocity),Time.deltaTime * 0.5f);
        }
    }

    public void Shoot()
    {
        Particles.active = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!Missile || HasExplode)
            return;

        HasExplode = true;
        Destroy(Instantiate(ExplosionEffect, transform.position, transform.rotation), 2f);

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(ExplosionClip);

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

        foreach (Collider nearbyObject in collidersToAddForece)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionForce, transform.position, range);
            }
        }

        Collider[] collidersToChangeHealth = Physics.OverlapSphere(transform.position, range);

        foreach (Collider nearbyObject in collidersToChangeHealth)
        {
            PlayerManager pm = nearbyObject.gameObject.GetComponentInChildren<PlayerManager>();

            if (pm != null)
            {
                pm.MinusHealth(damage);
            }

        }
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(Particles,4f);
        Destroy(gameObject, 2f);
    }
}
