using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackgroundTrigger : MonoBehaviour
{
    public Sprite newBackground;
    public SpriteRenderer backgroundRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            backgroundRenderer.sprite = newBackground;
        }
    }
}
