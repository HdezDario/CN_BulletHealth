using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float reducedSpeed;
    [SerializeField] private float chaseSpeed;
    private NavMeshAgent agent;

    private bool isChasing;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        PlayerMovement.onBeingLooked += BeingObserved;
    }

    private void OnDisable()
    {
        PlayerMovement.onBeingLooked -= BeingObserved;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
        if (isChasing) agent.speed = normalSpeed;
    }

    private void BeingObserved()
    {
        agent.speed = reducedSpeed;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ChaseZone")
        {
            Debug.Log("empieza cacería");
            agent.speed = chaseSpeed;
            isChasing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChaseZone")
        {
            Debug.Log("cazando");
            isChasing = true;
        }
    }
}
