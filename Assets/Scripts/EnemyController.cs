using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public float reboundForce = 6f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool inMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int lifes = 2; 
    private bool playerAlive;
    private bool getDamage;

    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        playerAlive = true;
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive && !isDead)
        {
            Movimiento();
        }
        animator.SetBool("isMoving", inMovement);
        animator.SetBool("isDead", isDead);
    }

    private void Movimiento()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x > 0.1f)
                spriteRenderer.flipX = false;
            else if (direction.x < -0.1f)
                spriteRenderer.flipX = true;


            movement = new Vector2(direction.x, 0);
            inMovement = true;
        }
        else
        {
            movement = Vector2.zero;
            inMovement = false;
        }
        if (!getDamage) rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 damageDirection = new Vector2(transform.position.x, 0);
            PlayerMovement playerScript = other.gameObject.GetComponent<PlayerMovement>();
            playerScript.getsDamage(damageDirection, 1);
            playerAlive = !playerScript.isDead;
            if (!playerAlive)
            {
                inMovement = false;
            }

        }
    }
    public void getsDamage(Vector2 direction, int amountDamage)
    {
        if (!getDamage)
        {
            lifes -= amountDamage;
            getDamage = true;
            if (lifes <= 0)
            {
                isDead = true;
                inMovement = false;
            }
            else
            {
                Vector2 rebound = ((Vector2)transform.position - direction).normalized;
                rebound.y = 0.5f; // fuerza vertical
                                  //rb.velocity = Vector2.zero; // opcional
                rb.AddForce(rebound * reboundForce, ForceMode2D.Impulse);
                StartCoroutine(deactivateDamage());
            }
           
        }
    }

    IEnumerator deactivateDamage()
    {
        yield return new WaitForSeconds(0.9f);
        getDamage = false;
        rb.velocity = Vector2.zero;
    }

    public void deleteBody()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("punch"))
        {
            Vector2 damageDirection = new Vector2(other.gameObject.transform.position.x, 0);
            getsDamage(damageDirection, 1);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
