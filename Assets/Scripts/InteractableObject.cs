using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public bool isInteractable;
    public bool wasInteracted;

    public static Action OnCollected;

    [SerializeField] private bool isStartGame;
    [SerializeField] private bool isKey;
    [SerializeField] public bool isWin;

    [SerializeField] GameObject cinematic;
    [SerializeField] GameObject winCinematic;

    private void OnEnable()
    {
        wasInteracted = false;
        isWin = false;
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

            else if (isKey)
            {
                KeyCollected();
            }

            else if (isWin)
            {
                WonGame();
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
        isStartGame = false;
        wasInteracted = false;
        cinematic.SetActive(true);
    }

    private void KeyCollected()
    {
        Debug.Log("Key Collected");
        wasInteracted = false;

        GameManager.keysCollected += 1;
        Debug.Log(GameManager.keysCollected);

        float stamina = FindObjectOfType<PlayerMovement>().stamina = 100;
        Debug.Log(stamina);

        GetComponentInParent<KeyMovement>().isCollected = true;
        Destroy(this.gameObject);
    }

    public void WinCon()
    {
        isWin = true;
    }

    private void WonGame()
    {
        winCinematic.SetActive(true);
    }
}
