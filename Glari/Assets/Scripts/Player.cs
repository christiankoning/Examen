using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

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

    // Power Ups & Collectible
    private bool ExtraJump;
    private bool CanShoot;
    private bool candoublejump;
    public GameObject DoubleJumpPower;
    public GameObject ShootPower;
    public int Collected;
    public GameObject FireBall;
    public float ShootForce;
    public GameObject FirePosition;
    private bool AntiSpam;

    // Health
    public float Health = 3;

    // Finish
    public GameObject Finish;
    private bool CanFinish;
    public int level;

    // Sounds
    public SoundManager SManager;

    //BossBattle
    public bool StartBattle;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        level = PlayerPrefs.GetInt("Level");
    }

    void Update()
    {
        CheckMovement();
        CheckSprint();
        CheckJump();
        HealthManager();
        Hit();
        Collecting();
    }

    void CheckMovement()
    {
        float horInput = Input.GetAxis("Horizontal") * moveSpeed;
        float verInput = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 forceVector = new Vector3(horInput, 0.0f, verInput).normalized * moveSpeed;
        velocityClamped = new Vector3(Mathf.Clamp(rb.velocity.x, -moveSpeedMinMax, moveSpeedMinMax), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -moveSpeedMinMax, moveSpeedMinMax));
        sprintVelocity = new Vector3(Mathf.Clamp(rb.velocity.x, -sprintSpeedMinMax, sprintSpeedMinMax), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -sprintSpeedMinMax, sprintSpeedMinMax));

        rb.velocity = velocityClamped;

        if(StartBattle == false)
        {
            Vector3 movement = new Vector3(horInput, 0.0f, verInput);
            Model.transform.rotation = Quaternion.LookRotation(movement).normalized;
            rb.AddRelativeForce(forceVector);
        }
        else
        {
            Vector3 movement = new Vector3(-horInput, 0.0f, -verInput);
            Model.transform.rotation = Quaternion.LookRotation(movement).normalized;
            rb.AddRelativeForce(-forceVector);
        }

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
                    if (ExtraJump == true || StartBattle == true)
                    {
                        candoublejump = true;
                    }
                }
                else
                {
                    if (candoublejump)
                    {
                        rb.AddRelativeForce(Vector3.up * force);
                        candoublejump = false;
                    }
                }
            }
        }
    }

    public void Jump()
    {
        rb.AddRelativeForce(Vector3.up * force);
        SManager.AudioPlayerMovement = SManager.AddAudio(SManager.Jumping, false, 0.2f);
        SManager.AudioPlayerMovement.Play();
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
        if (Health <= 0)
        {
            Model.GetComponent<Animator>().SetBool("IsDead", true);
            rb.isKinematic = true;
            Scene currentscene = SceneManager.GetActiveScene();
            string scenename = currentscene.name;
            SceneManager.LoadScene(scenename);
        }
    }

    void Hit()
    {
        Vector3 fwd = FirePosition.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Debug.DrawRay(FirePosition.transform.position, fwd);

        if (Input.GetMouseButtonDown(0) && CanShoot == false)
        {
            Model.GetComponent<Animator>().SetBool("IsFighting", true);
            StartCoroutine(Punching());

            if(Physics.Raycast(FirePosition.transform.position, fwd, out hit, 3))
            {
                if(hit.collider.gameObject.GetComponent<Mushroom>())
                {
                    Mushroom enemy = hit.collider.GetComponent<Mushroom>();
                    enemy.MHealth--;
                    Debug.Log(hit.collider.gameObject);
                }
            }
        }

        if(Input.GetMouseButtonDown(0) && CanShoot == true)
        {
            if(AntiSpam == true)
            {
                GameObject FireObject = (GameObject)Instantiate(FireBall, FirePosition.transform.position + transform.forward, Quaternion.Euler(Model.transform.rotation.eulerAngles.x, Model.transform.rotation.eulerAngles.y, Model.transform.rotation.eulerAngles.z));
                Vector3 myForward = Model.transform.TransformDirection(Vector3.forward);
                FireObject.GetComponent<Rigidbody>().AddForce(myForward * ShootForce);
                Fire FD = FireObject.GetComponent<Fire>();
                FD.DestroyFire();
                FireObject.transform.parent = null;
                AntiSpam = false;
                StartCoroutine(ShotCoolDown());
            }
        }
    }

    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(1);
        AntiSpam = true;
    }

    IEnumerator Punching()
    {
        yield return new WaitForSeconds(0.5f);
        Model.GetComponent<Animator>().SetBool("IsFighting", false);
    }

    void Collecting()
    {
        if(Collected == 3)
        {
            CanFinish = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == DoubleJumpPower)
        {
            ExtraJump = true;
            StartCoroutine(JumpAbilityTimer());
            DoubleJumpPower.SetActive(false);
            SManager.AudioCollecting = SManager.AddAudio(SManager.PowerUp, false, 0.2f);
            SManager.AudioCollecting.Play();
        }

        if(collision.gameObject == ShootPower)
        {
            CanShoot = true;
            StartCoroutine(ShootAbilityTimer());
            ShootPower.SetActive(false);
            AntiSpam = true;
            SManager.AudioCollecting = SManager.AddAudio(SManager.PowerUp, false, 0.2f);
            SManager.AudioCollecting.Play();
        }

        if(collision.gameObject.name == "DeathGround")
        {
            Health = 0;
        }

        if (collision.gameObject == Finish)
        {
            if(CanFinish == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                level++;
                PlayerPrefs.SetInt("Level", level);
            }
            else
            {
                // Not Allowed to finish
            }
        }

        if(collision.gameObject.name == "Enemy")
        {
            Health--;
        }

        if(collision.gameObject.name == "Wood")
        {
            Health = 0;
        }
    }

    IEnumerator JumpAbilityTimer()
    {
        yield return new WaitForSeconds(30);
        ExtraJump = false;
    }

    IEnumerator ShootAbilityTimer()
    {
        yield return new WaitForSeconds(30);
        CanShoot = false;
    }

    public void BossBattleManager()
    {
        if(StartBattle == true)
        {
            rb.isKinematic = true;
            StartCoroutine(SeeBoss());
        }
    }

    IEnumerator SeeBoss()
    {
        yield return new WaitForSeconds(10);
        rb.isKinematic = false;
    }
}