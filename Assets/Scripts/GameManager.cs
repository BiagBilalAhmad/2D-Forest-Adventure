using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Coins")]
    public TextMeshProUGUI coinText;
    public int coinCount = 0;

    [Header("Key")]
    public GameObject key;

    [Header("Panels")]
    public GameObject victoryPanel;

    void Start()
    {
        key.SetActive(false);
        coinText.text = coinCount.ToString();
        victoryPanel.SetActive(false);
    }

    public void EnableKeyUI()
    {
        key.SetActive(true);
    }

    public void AddToCoins()
    {
        coinCount++;
        coinText.text = coinCount.ToString();
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
