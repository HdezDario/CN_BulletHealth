using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject cam;
    [SerializeField] private Image pointer;

    [SerializeField] private float playerSpeed;
    private InputSystem inputSystem;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputSystem = new InputSystem();
        inputSystem.Player.Enable();

        rb = GetComponentInParent<Rigidbody>();
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

        rb.velocity = inputVector.y * transform.forward * playerSpeed;

        //rb.velocity = new Vector3(inputVector.x * playerSpeed, 0, inputVector.y * playerSpeed);
    }
}
