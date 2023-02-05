using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject cam;
    [SerializeField] private Image pointer;

    [SerializeField] private float playerSpeed;
    public InputSystem inputSystem;
    [SerializeField] public float stamina;
    [SerializeField] private Slider staminaBar;

    [SerializeField] private float rStaminaLooking;
    [SerializeField] private float rStaminaAvoiding;
    [SerializeField] private float lostStamina;

    [SerializeField] private float recoverTime;
    private float timer;
    private bool onMovement;
    private bool looking;

    [SerializeField] private PostProcessVolume pp;
    private Vignette vg;
    private ColorGrading cg;
    private float eyelid;
    [SerializeField] private Image dot;

    [SerializeField] public bool godMode;

    public static Action onBeingLooked;

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
        looking = false;

        pp.profile.TryGetSettings(out vg);
        pp.profile.TryGetSettings(out cg);
    }

    private void OnDisable()
    {
        inputSystem.Player.Disable();
    }

    private void Update()
    {
        //yRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100;

        // Movement and Rotation

        transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);

        Vector2 inputVector = inputSystem.Player.Movement.ReadValue<Vector2>();

        inputVector.y = Mathf.Clamp(inputVector.y, 0, 1);

        // Stamina Control

        if (stamina > 0)
            rb.velocity = inputVector.y * transform.forward * playerSpeed;

        if (stamina <= 0) rb.velocity = Vector3.zero;

        if (inputVector.y > 0) 
        {
            onMovement = true;
            timer = recoverTime;
            if (stamina > 0 && !godMode) stamina -= lostStamina;
        }

        if (rb.velocity == Vector3.zero) onMovement = false;

        if (!onMovement)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) RecoverStamina();
        }

        stamina = Mathf.Clamp(stamina, 0, 100);
        staminaBar.value = stamina;

        // Looking

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            if (hit.transform.tag == "AI" && stamina > 0)
            {
                looking = true;
                onBeingLooked?.Invoke();
            }

            else looking = false;

            if (hit.transform.tag == "Interactable")
            {
                if (hit.transform.GetComponent<InteractableObject>().isInteractable)
                {
                    dot.color = Color.gray;

                    if (inputSystem.Player.Interact.WasPerformedThisFrame())
                    {
                        Debug.Log("Interactuado");
                        hit.transform.GetComponent<InteractableObject>().isInteractable = false;
                        hit.transform.GetComponent<InteractableObject>().wasInteracted = true;
                        dot.color = Color.white;
                    }     
                }  
            }

            else dot.color = Color.white;
        }

        // Eyelid effect

        vg.intensity.value = 0.36f + Mathf.Clamp((0.24f * (100f - stamina)) / 100f, 0f, 0.24f);

        eyelid = 1f - Mathf.Clamp((1f * (100f - stamina)) / 100f, 0f, 1f);

        cg.colorFilter.value.r = eyelid;
        cg.colorFilter.value.g = eyelid;
        cg.colorFilter.value.b = eyelid;
    }

    private void RecoverStamina()
    {
        if (stamina < 100)
        {
            if (looking)
                stamina += rStaminaLooking;
            else stamina += rStaminaAvoiding;
        }
    }

    public void startCMConfig()
    {
        stamina = 100;

        vg.intensity.value = 0.36f;

        cg.colorFilter.value.r = 1f;
        cg.colorFilter.value.g = 1f;
        cg.colorFilter.value.b = 1f;
    }

    public void DisableGodMode()
    {
        godMode = false;
    }
}
