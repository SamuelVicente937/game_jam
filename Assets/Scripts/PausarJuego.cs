using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPause;
    public bool isPaused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continue();
            }

            else
            {
                pause();
            }
        }
    }

    public void Continue()
    {
        menuPause.SetActive(false); 
        Time.timeScale = 1;
        isPaused = false;
    }
    public void pause()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
}
