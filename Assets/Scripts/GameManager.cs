using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coins = 0;

    [Header("UI")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button playButton;
    public Text coinText;


    [Header("Intro Settings")]
    [SerializeField] private Animator introAnimator; 
    [SerializeField] private float introDuration = 2f; 

    [Header("References")]
    [SerializeField] private GameObject player; 

    private bool isGameActive = false;
    private PlayerController playerController;

    private void Start()
    {
        Time.timeScale = 1f;

        if (player != null)
            playerController = player.GetComponent<PlayerController>();
        else
            playerController = FindObjectOfType<PlayerController>();

        if (playerController != null)
            playerController.enabled = false;

 
    }

    void Update()
    {
        coinText.text = coins.ToString();
    }

    public void OnPlayButtonPressed()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        isGameActive = false;

        if (introAnimator != null)
        {
            introAnimator.SetTrigger("Play");
            yield return new WaitForSeconds(introDuration);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }

        if (playerController != null)
            playerController.enabled = true;

        mainMenuPanel.SetActive(false);
        isGameActive = true;
    }

    public void LoseGame()
    {
        if (!isGameActive) return;
        StartCoroutine(LoseCoroutine());
    }

    private IEnumerator LoseCoroutine()
    {
        isGameActive = false;

        if (playerController != null)
            playerController.enabled = false;

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator WinCoroutine()
    {
        isGameActive = false;

        if (playerController != null)
            playerController.enabled = false;

        winPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void WinGame()
    {
        StartCoroutine(WinCoroutine());
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public void OnExitButtonHandler()
    {
        Application.Quit();
    }
}
