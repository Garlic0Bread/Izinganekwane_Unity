using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Char_Ctrl : MonoBehaviour
{
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private Animator anim;

    int noOfClicks = 0;
    float verticalInput;
    float horizontalInput;
    float maxComboDelay = 3;
    float lastClickedTime = 0.2f;

    Rigidbody rb;
    Vector3 moveDir;

    [Header("Movement")]
    public float speed;
    public Transform orientation;
    public float groundDrag;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool canJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        MyInput();
        Combo_no1();
        Speed_Ctr();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0f;
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void OnClick()
    {
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.SetBool("Attack1_Stab", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks > 1 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1_Stab"))
        {
            anim.SetBool("Attack1_Stab", false);
            anim.SetBool("Attack2_Slash", true);
        }
        if (noOfClicks > 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2_Slash"))
        {
            anim.SetBool("Attack2_Slash", false);
            anim.SetBool("Attack3_HeavySlash", true);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.x);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResteJump()
    {
        canJump = true;
    }
    private void MyInput()
    {
        if(Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResteJump), jumpCoolDown);
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //walking animation
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("CanWalk", true);
            anim.SetBool("IsIdle", false);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("CanWalk", false);
            anim.SetBool("IsIdle", false);
        }
    }
    private void Movement()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * speed * 10f * airMultiplier , ForceMode.Force);

        }
    }
    private void Speed_Ctr()
    {
        Vector3 flatLvl = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatLvl.magnitude > speed)
        {
            Vector3 limitedlv = flatLvl.normalized * speed; 
            rb.velocity = new Vector3(limitedlv.x, rb.velocity.y, limitedlv.z);
        }
    }
    private void Combo_no1()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1_Stab"))
        {
            anim.SetBool("Attack1_Stab", false);
            noOfClicks = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2_Slash"))
        {
            anim.SetBool("Attack2_Slash", false);
            noOfClicks = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3_HeavySlash"))
        {
            anim.SetBool("Attack3_HeavySlash", false);
            noOfClicks = 0;
        }


        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        //cooldown time
        if (Time.time > nextFireTime)
        {
            // Check for mouse input
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }
}
