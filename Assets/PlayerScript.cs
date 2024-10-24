using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    /*Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float mouseSensitivity;
    [SerializeField] Camera mainCamera;
    [SerializeField] float verticalRotaion;
    //[SerializeField] float mouseSensitivity;
    [SerializeField] float upDownRange;

    private InputAction lookAction;

    Vector2 moveInput;
    Vector2 mouseInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lookAction = playerControls.FindActionMap("PlayerControls").FindAction("Look");
    }

    private void OnEnable()
    {
        lookAction.Enable();
    }
    private void OnDisable()
    {
        lookAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveInput.x, 0, moveInput.y)*speed,ForceMode.Acceleration);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void Rotate()
    {
        float mouseXRotation = mouseInput.x * mouseSensitivty;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= mouseInput.y * mouseSensitivity;
        verticalRotaion = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = quaternion.Euler(verticalRotation, 0, 0);
    }*/
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    private InputManagerScript inputManager;
    private Transform cameraTransform;
    [SerializeField] Camera main;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManagerScript.Instance;
        cameraTransform = main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        /*if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }*/

        // Changes the height position of the player..
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
