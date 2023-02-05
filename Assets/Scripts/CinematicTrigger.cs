using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cinematic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") cinematic.SetActive(true);
    }
}
