using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atackandobillno : MonoBehaviour
{
    public GameObject[] Lanza;
    public Transform punto;
    private float cronometro;
    public bool Salto;
    public Animator ani;
    public int ataques;
    public bool atacando;
    public GameObject target;
    public GameObject[] ultis;

    private RaycastHit2D hit;
    public Vector3 v3;
    public float distance;
    public LayerMask layer;

    private float gravedad;
    public int fases;

    void Star()
    {
        ani = GetComponent<Animator>();
        cronometro = 1.5f;
    }
    public void Final_Ani()
    {
        Lanza[0].SetActive(false);
        Lanza[1].SetActive(false);
        ani.SetBool("shield", false);
        ani.SetBool("gigaAttack", false);
        ataques = 0;
        cronometro = 1.5f;
        atacando = false; 
    }
    void Ataque_Distancia()
    {
        Lanza[0].SetActive(true);
        Lanza[0].transform.position = punto.position;
        Lanza[0].transform.rotation = transform.rotation;
        Lanza[0].GetComponent<Gotitasale>().speed = 8;
        Lanza[0].GetComponent<Gotitasale>().mover = true;
    }
    public void Ulti1()
    {
        ultis[0].SetActive(true);
    }
    public void Ulti2()
    {
        ultis[1].SetActive(true);
    }
    public void Ulti3()
    {
        ultis[2].SetActive(true);
    }

}
