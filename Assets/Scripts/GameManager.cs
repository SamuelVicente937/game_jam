using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button tryAgainButton;
    public Button menuButton;


    private bool gameOverActivo = false;
    public CanvasGroup gameOverCanvasGroup;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (tryAgainButton != null)
        {
            tryAgainButton.onClick.AddListener(tryAgain);
        }
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(goMenu);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverActivo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                tryAgain();
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
            {
                goMenu();
            }
        }
    }

    public void GameOver()
    {
        if (gameOverActivo) return;

        gameOverActivo = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (gameOverText != null)
        {
            gameOverText.text = "Game over\n\nR - Reiniciar\nESC - MenuPrincipal";
        }

        StartCoroutine(FadeInGameOver());
    }
    IEnumerator FadeInGameOver()
    {
        float time = 0.5f;
        gameOverCanvasGroup.alpha = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            gameOverCanvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }

        gameOverCanvasGroup.alpha = 1f;
    }

    public void tryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void goMenu()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("Menu");
    }
}
