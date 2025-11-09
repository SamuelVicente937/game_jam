using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public void CallTakeDamageFromParent()
    {
        // Busca el script PlayerMovement en el padre y llama TakeDamage()
        PlayerMovement player = transform.parent.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.deactivateDamage();
        }
        else
        {
            Debug.LogWarning("No se encontró PlayerMovement en el padre.");
        }
    }

    public void CallEndAnimationAttack()
    {
        PlayerMovement player = transform.parent.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.desactiveAttack(); // activa el collider justo en el frame de golpe
        }
        else
        {
            Debug.LogWarning("No se encontró PlayerMovement en el padre.");
        }
    }
    public void CallAttackFromParent()
    {
        PlayerMovement player = transform.parent.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.actAttackCollider(); // activa el collider justo en el frame de golpe
        }
        else
        {
            Debug.LogWarning("No se encontró PlayerMovement en el padre.");
        }
    }

    public void CallEndAttackFromParent()
    {
        PlayerMovement player = transform.parent.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.deactAttackCollider(); // desactiva el collider al terminar el golpe
        }
        else
        {
            Debug.LogWarning("No se encontró PlayerMovement en el padre.");
        }
    }

    public void CallDestroyEnemyLvl2()
    {
        EnemyController enemy = transform.parent.GetComponentInParent<EnemyController>();
        if (enemy != null)
        {
            enemy.deleteBody(); // desactiva el collider al terminar el golpe
        }
        else
        {
            Debug.LogWarning("No se encontró PlayerMovement en el padre.");
        }
    }
}
