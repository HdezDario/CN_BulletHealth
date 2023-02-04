using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject cam;
    [SerializeField] private Image pointer;

    [SerializeField] private float playerSpeed;
    private InputSystem inputSystem;
    [SerializeField] private float stamina;
    [SerializeField] private Slider staminaBar;
    [SerializeReference] private float recoverTime;
    private float timer;
    [SerializeField] private bool onMovement;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputSystem = new InputSystem();
        inputSystem.Player.Enable();

        rb = GetComponentInParent<Rigidbody>();

        stamina = 100;
        staminaBar.value = stamina;
        recoverTime = 3f;
        timer = recoverTime;
        onMovement = false;
    }

    private void OnDisable()
    {
        inputSystem.Player.Disable();
    }

    private void Update()
    {
        //yRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100;

        transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);

        Vector2 inputVector = inputSystem.Player.Movement.ReadValue<Vector2>();

        inputVector.y = Mathf.Clamp(inputVector.y, 0, 1);

        if (stamina > 0)
            rb.velocity = inputVector.y * transform.forward * playerSpeed;

        if (stamina <= 0) rb.velocity = Vector3.zero;

        if (inputVector.y > 0) 
        {
            onMovement = true;
            timer = recoverTime;
            if (stamina > 0) stamina -= 0.05f;
        }

        if (rb.velocity == Vector3.zero) onMovement = false;

        if (!onMovement)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) RecoverStamina();
        }

        staminaBar.value = stamina;

        //rb.velocity = new Vector3(inputVector.x * playerSpeed, 0, inputVector.y * playerSpeed);
    }

    private void RecoverStamina()
    {
        if (stamina < 100) stamina += 0.1f;
    }
}
