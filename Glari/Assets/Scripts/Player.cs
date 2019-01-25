using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Movement
    public float moveSpeed = 1;
    public float moveSpeedMinMax = 1;
    public float sprintSpeedMinMax = 1.5f;
    public float jumpRaycastDistance = 1.01f;
    private bool isGrounded;
    public float force = 500;


    public Rigidbody rb;
    public GameObject Model;

    private Vector3 velocityClamped;
    private Vector3 sprintVelocity;
    
    // Power Ups
    private bool ExtraJump;
    private bool candoublejump;


    // Health
    public float Health = 6;
    public Sprite FullHealth;
    public Sprite HeartLost;
    public Sprite TwoHeartsLost;
    public Sprite NoHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckMovement();
        CheckSprint();
        CheckJump();
        HealthManager();
        Hit();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Health--;
        }
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

        Vector3 movement = new Vector3(horInput, 0.0f, verInput);
        Model.transform.rotation = Quaternion.LookRotation(movement).normalized;

        if (horInput != 0 || verInput != 0)
        {
            Model.GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else
        {
            Model.GetComponent<Animator>().SetBool("IsWalking", false);
        }
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
            {
                if (isGrounded)
                {
                    Jump();
                    if(ExtraJump == true)
                    {
                        candoublejump = true;
                    }
                }
                else
                {
                    if(candoublejump)
                    {
                        rb.AddRelativeForce(Vector3.up * force);
                    }
                }
            }
        }
    }

    void Jump()
    {
            rb.AddRelativeForce(Vector3.up * force);
    }

    void CheckSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
    }

    void Sprint()
    {
        rb.velocity = sprintVelocity;
    }

    void HealthManager()
    {
      if(Health <= 0)
        {
            Model.GetComponent<Animator>().SetBool("IsDead", true);
        }
    }

    void Hit()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Model.GetComponent<Animator>().SetBool("IsFighting", true);
            StartCoroutine(Punching());
        }
    }

    IEnumerator Punching()
    {
        yield return new WaitForSeconds(0.5f);
        Model.GetComponent<Animator>().SetBool("IsFighting", false);
    }
}
