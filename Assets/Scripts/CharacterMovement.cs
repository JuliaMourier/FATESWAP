using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    //for A&D  /  Right&Left Arrow And Jump Movement
    //for Adding W&S Movement, Remove The Slashes Below And Add Slashes 
    //To Jump Movement Only Also In RigidBody2D Remove Gravity For It

    public float jumpHeight = 1.0f;
    public float speed = 1.0f;
    public float fallingSpeed = -30.0f;
    public Rigidbody2D rb;
    public CircleCollider2D circular;
    private bool IsGrounded = true;
    private float MoveX;
    private float jumpForce;
    private bool jumpKeyHeld;
    public Animator animator;
    public bool enableMovement = true;
    private bool power = false;


    //private float MoveY;
    //private bool DoDeath = true;
    // public GameObject DeathScreen;
    //public GameObject LevelCompleteScreen;
    void Start()
    {
        //DeathScreen.gameObject.SetActive(false);
        //LevelCompleteScreen.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        circular = GetComponent<CircleCollider2D>();
        jumpForce = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * jumpHeight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!IsGrounded)
        {
            if (!jumpKeyHeld || Vector2.Dot(rb.velocity, Vector2.up) < -0.5f)
            {
                rb.AddForce(new Vector2(0, fallingSpeed) * rb.mass);
            }

        }

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            animator.SetTrigger("transformation");
            power = !power;
        }
        if (!enableMovement) { return; }
        MoveX = Input.GetAxisRaw("Horizontal");

        rb.AddForce(new Vector2(MoveX * speed * rb.mass, 0));
        if (rb.velocity.x > 0.01f)
        {
            transform.localScale =new Vector3(0.65f, 0.65f, 0.65f);
        }
        else if (rb.velocity.x < -0.01f)
        {
         
        transform.localScale = new Vector3(-0.65f, 0.65f, 0.65f);
        }

        animator.SetBool("move", MoveX != 0);


        if ((Input.GetButtonDown("Jump") && IsGrounded))
        {
            jumpKeyHeld = true;
           // animator.SetBool("isJumping", true);

            rb.AddForce(new Vector2(0, 2) * jumpForce * rb.mass, ForceMode2D.Impulse);
            IsGrounded = false;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpKeyHeld = false;
        }
        animator.SetBool("power", power);
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IsGrounded = false;
           // animator.SetBool("isJumping", true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IsGrounded = true;
            // animator.SetBool("isJumping", false);
        }
    }

    public void SetEnableMovement(bool value)
    {

        this.enableMovement = value;
    }



}
