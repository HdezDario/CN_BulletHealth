using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static int keysCollected;
    private InputSystem inputSystem;
    [SerializeField] private GameObject goalCinematic;

    private void OnEnable()
    {
        inputSystem = new InputSystem();
        inputSystem.Player.Enable();
        inputSystem.Player.Exit.performed += Exit;

        keysCollected = 0;
    }

    private void OnDisable()
    {
        inputSystem.Player.Exit.performed -= Exit;
    }

    void Update()
    {
        if (keysCollected >= 4)
        {
            goalCinematic.SetActive(true);
            Debug.Log("Game Won");
        }
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }

    private void Exit(InputAction.CallbackContext context)
    {
        if (context.performed)
            Application.Quit();
    }

    public void OnGameFinish()
    {
        SceneManager.LoadScene(0);
    }

    public void OnLooseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
