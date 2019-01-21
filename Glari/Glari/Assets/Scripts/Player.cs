using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 1;
    public float moveSpeedMinMax = 1;
    public float sprintSpeedMinMax = 1.5f;
    public float jumpRaycastDistance = 1.01f;
    private bool isGrounded;
    public float force = 500;

    public Rigidbody rb;

    private Vector3 velocityClamped;
    private Vector3 sprintVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckMovement();
        CheckSprint();
        CheckJump();
    }

    void CheckMovement()
    {
        float horInput = Input.GetAxis("Horizontal") * moveSpeed;
        float verInput = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 forceVector = new Vector3(horInput, 0.0f, verInput).normalized * moveSpeed;
        velocityClamped = new Vector3(Mathf.Clamp(rb.velocity.x, -moveSpeedMinMax, moveSpeedMinMax), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -moveSpeedMinMax, moveSpeedMinMax));
        sprintVelocity = new Vector3(Mathf.Clamp(rb.velocity.x, -sprintSpeedMinMax, sprintSpeedMinMax), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -sprintSpeedMinMax, sprintSpeedMinMax));

        rb.AddRelativeForce(forceVector);
        rb.velocity = velocityClamped;
    }

    void CheckJump()
    {
        {
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position, Vector3.down, out hit, jumpRaycastDistance))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
                if (isGrounded)
                    Jump();
        }
    }

    void Jump()
    {
        rb.AddRelativeForce(Vector3.up * force);
    }

    void CheckSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            Sprint();
    }

    void Sprint()
    {
        rb.velocity = sprintVelocity;
    }
}
