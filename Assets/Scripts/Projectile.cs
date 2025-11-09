using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 2f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Prueba primero con EnemyController
            EnemyController ec = other.GetComponentInParent<EnemyController>();
            if (ec != null)
            {
                ec.getsDamage(Vector2.zero, (int)damage);
                Destroy(gameObject);
                return;
            }

            // Prueba con MiedoController
            MiedoController mc = other.GetComponentInParent<MiedoController>();
            if (mc != null)
            {
                Vector2 hitDir = transform.position - mc.transform.position;
                mc.TakeDamage(hitDir, (int)damage);
                Destroy(gameObject);
                return;
            }
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
