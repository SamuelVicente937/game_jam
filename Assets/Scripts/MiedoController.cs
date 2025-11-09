using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiedoController : MonoBehaviour
{
    public Transform player;
    public Animator anim;

    [Header("Rangos")]
    public float detectRange = 6f;
    public float attackRange = 1.2f;

    [Header("Velocidades")]
    public float normalSpeed = 1.5f;
    public float rageSpeed = 3.5f;
    public float rageLife = 20f;

    [Header("Vida")]
    public float life = 100f;

    private float currentSpeed;
    public SpriteRenderer spriteRenderer;
    public LayerMask obstacleMask; // Asignar solo lo que quieres que bloquee
    private bool playerAlive;

    void Start()
    {
        playerAlive = true;
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        if (life <= 0) return;

        // si entra en modo furia
        if (life <= rageLife)
            currentSpeed = rageSpeed;

        float dist = Vector2.Distance(player.position, transform.position);

        // si está muy lejos -> no hace nada
        if (dist > detectRange)
        {
            anim.SetBool("isMoving", false);
            return;
        }

        // si está en rango de ataque
        if (dist <= attackRange)
        {
            anim.SetBool("isMoving", false);
            anim.SetTrigger("doAttack");
        }
        else
        {
            // mira hacia donde está el player
            if (player.position.x > transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);

            // moverse
            anim.SetBool("isMoving", true);
            Vector2 dir = new Vector2(player.position.x - transform.position.x, 0).normalized;
            transform.position += (Vector3)dir * currentSpeed * Time.deltaTime;

            if (dir.x > 0.1f)
                spriteRenderer.flipX = false; // mira a la derecha
            else if (dir.x < -0.1f)
                spriteRenderer.flipX = true;  // mira a la izquierda


        }
    }

    public void TakeDamage(Vector2 hitDirection, int dmg)
    {
        life -= dmg;

        if (life <= 0)
        {
            anim.SetBool("isDead", true);
            Destroy(gameObject, 1f);
        }
        else
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 rebound = ((Vector2)transform.position - hitDirection).normalized;
                rebound.y = 0.1f;
                rb.AddForce(rebound * 1f, ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter: " + other.name);
        if (other.gameObject.CompareTag("punch"))
        {
            Vector2 damageDirection = new Vector2(other.gameObject.transform.position.x, 0);
            TakeDamage(damageDirection, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 damageDirection = new Vector2(transform.position.x, 0);
            PlayerMovement playerScript = other.gameObject.GetComponent<PlayerMovement>();
            playerScript.getsDamage(damageDirection, 1);
            playerAlive = !playerScript.isDead;
            
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
