using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float reboundForce = 5f;
    private Rigidbody2D rb;
    public Animator animator;
    public Collider2D attackCollider;
    public float raycastLength = 0.1f;

    public LayerMask groundLayer;

    private bool isGrounded;
    private bool wasGrounded;

    private bool isHit;
    private bool isAttacking;
    private int jumpCount = 0;
    public int maxJumps = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            movement();
        }
        // detectar suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayer);
        isGrounded = hit.collider != null;

        // solo resetea cuando pasa de no estar grounded a estar grounded
        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        wasGrounded = isGrounded;

        // salto
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps && !isHit)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            attack();
        }
        animations();
     
    }

    public void movement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (!isHit) rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        else moveInput = 0;


        animator.SetFloat("movement", Mathf.Abs(moveInput));

        if (moveInput > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    public void animations()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("getDamage", isHit);
        animator.SetBool("isAttacking", isAttacking);
    }
    //void FixedUpdate()
    //{
    //    // movimiento físico
    //    float moveInput = Input.GetAxis("Horizontal");
    //    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    //}
    public void getsDamage(Vector2 direction, int amountDamage)
    {
        if (!isHit)
        {
            isHit = true;
            Vector2 rebound = ((Vector2)transform.position - direction).normalized;
            rebound.y = 0.5f; // fuerza vertical
            //rb.velocity = Vector2.zero; // opcional
            rb.AddForce(rebound * reboundForce, ForceMode2D.Impulse);
        }
    }

    public void desactiveDamage()
    {
        isHit = false;
        rb.velocity = Vector2.zero;
    }

    public void attack()
    {
        isAttacking = true;
    }
    public void desactiveAttack()
    {
        isAttacking = false;
    }
    public void actAttackCollider()
    {
        attackCollider.enabled = true;
    }
    public void deactAttackCollider()
    {
        attackCollider.enabled = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // punto de inicio del rayo
        Vector3 start = transform.position;

        // punto final del rayo
        Vector3 end = transform.position + Vector3.down * raycastLength;

        Gizmos.DrawLine(start, end);
    }


}
