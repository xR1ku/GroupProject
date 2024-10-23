using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //rb.velocity = new Vector3(moveInput.x, rb.velocity.y, moveInput.y) * Time.deltaTime * speed;
        /*if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }*/
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
}
