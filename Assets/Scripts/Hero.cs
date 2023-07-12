using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5; // Количество жизней
    [SerializeField] private float jumpForce = 1f;
    private bool isGrounded = false;



    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;


    public static player Instance { get; set; }


    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }


    private void Awake()
    {
        Instance = this;
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Run()
    {
        if (isGrounded) State = States.Run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }
    private void Update()
    {
        if (isGrounded) State = States.Idle;

        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) State = States.Jump;
    }

    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }

}


public enum States
{
    Idle,
    Run,
    Jump
}