using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gotitasale : MonoBehaviour
{
    public float speed;
    public bool mover;

    private void OntriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("piso"))
        {
            speed = -8;
        }
        if (coll.CompareTag("Boss"))
        {
            mover = false;
            coll.GetComponent<Animator>().SetBool("gotita", false);
            gameObject.SetActive(false);
            coll.GetComponent<Atackandobillno>().Final_Ani();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mover)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
