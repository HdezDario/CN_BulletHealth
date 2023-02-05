using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeyMovement : MonoBehaviour
{

    [SerializeField] private List<Transform> points;
    private NavMeshAgent agent;

    private float dist;
    [SerializeField] private int index;

    public bool isCollected;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        isCollected = false;
    }

    void Update()
    {
        agent.updatePosition = true;

        if (agent.enabled)
            dist = agent.remainingDistance;
        if (!agent.pathPending && dist < 0.5f)
            NextPosition();

        if (isCollected)
            Destroy(this.gameObject);
    }

    public void NextPosition()
    {
        agent.destination = points[index].position;

        if (index < points.Count - 1)
            index++;
        else if (index < points.Count)
            index = 0;
    }
}
