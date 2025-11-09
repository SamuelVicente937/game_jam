using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image rellenoVida;
    private PlayerMovement playerController;
    private float maxLife;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerMovement>();    
        maxLife = playerController.life;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoVida.fillAmount = playerController.life / maxLife;
    }
}
