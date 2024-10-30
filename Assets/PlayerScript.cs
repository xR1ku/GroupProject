using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] Target target;
    [SerializeField] GunData gunData;
    private InputManagerScript inputManager;
    private Transform cameraTransform;
    [SerializeField] Camera main;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManagerScript.Instance;
        cameraTransform = main.transform;
        target.setHealth(100);
        gunData.totalAmmo = gunData.maxAmmo;
        gunData.currentAmmo = gunData.magSize;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //move
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //jump
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        //gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            gunData.totalAmmo = gunData.maxAmmo;
            Destroy(other.gameObject);
            Debug.Log("ammo gained");
        }
        else if(other.gameObject.tag == "Health")
        {
            target.Heal(50);
            Destroy(other.gameObject);
            Debug.Log("Healed");
        }
        else if(other.gameObject.tag == "Damage")
        {
            target.Damage(20);
            Debug.Log("damaged");
        }
    }
}
