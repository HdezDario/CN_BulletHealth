using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isInteractable;
    public bool wasInteracted;

    [SerializeField] private bool isStartGame;

    [SerializeField] GameObject cinematic;

    private void OnEnable()
    {
        wasInteracted = false;
    }

    private void OnDisable()
    {
        wasInteracted = false;
    }

    private void Update()
    {
        if (wasInteracted)
        {
            if (isStartGame)
            {
                StartGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactor") isInteractable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactor") isInteractable = false;
    }

    private void StartGame()
    {
        cinematic.SetActive(true);
    }
}
