using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        // mover horizontal
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // salto
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // --- elegir animación ---
        if (Mathf.Abs(move) > 0.1f)
        {
            animator.Play("run");
        }
        else
        {
            animator.Play("idle");
        }

        if (move > 0.1f) // derecha
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < -0.1f) // izquierda
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
