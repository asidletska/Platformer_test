using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coins = 0;

    public GameObject playerGameObject;
    private PlayerController player;
    public Text coinText;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        coinText.text = coins.ToString();

    }
    public void OnPlayButtonHandler()
    {

    }
    public void OnExitButtonHandler()
    {
        Application.Quit();
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }



}
