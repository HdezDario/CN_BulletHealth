using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float reducedSpeed;
    private NavMeshAgent agent;

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
        agent.speed = normalSpeed;
    }

    private void BeingObserved()
    {
        agent.speed = reducedSpeed;
    }
}
