using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AICollision : MonoBehaviour
{
    [SerializeField] private float minuteToRealTime;
    private float timer;
    private float randFloat;
    [SerializeField] private NavMeshAgent agent;

    private void OnEnable()
    {
        timer = minuteToRealTime;
        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            randFloat = Random.Range(-0.1f, 0.1f);

            timer = minuteToRealTime;
        }

        transform.Rotate(new Vector3 (Random.rotationUniform.x, Random.rotationUniform.y, Random.rotationUniform.z) * agent.speed);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
