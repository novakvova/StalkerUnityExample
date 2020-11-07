using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{
    private GameObject Target;
    private NavMeshAgent agent;

    public void Start()
    {
        Target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        agent.SetDestination(Target.transform.position + Vector3.forward);
    }
}
