using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsEnemy : MonoBehaviour
{
    public Transform[] points;
    public GameObject EnemyPrefab;

    [SerializeField] private float delay;

    private bool CanSpawn;

    public float EasyTime;
    public float NormalTime;
    public float HardTime;

    private void Start()
    {
        StartCoroutine(Delay());
        StartCoroutine(Easy());
    }

    private void Update()
    {
        if (CanSpawn)
        {
            CanSpawn = false;
           Instantiate(EnemyPrefab,points[Random.Range(0, points.Length)]);
        }
    }

    IEnumerator Delay()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            CanSpawn = true;
            yield return new WaitForSeconds(delay);
            CanSpawn = false;
        }
    }

    IEnumerator Easy()
    {
        yield return new WaitForSeconds(EasyTime);
        delay -= 1f;
        StartCoroutine(Normal());
    }

    IEnumerator Normal()
    {
        yield return new WaitForSeconds(NormalTime);
        delay -= 2f;
        StartCoroutine(Hard());

    }

    IEnumerator Hard()
    {
        yield return new WaitForSeconds(HardTime);
        delay -= 3f;
    }
}
