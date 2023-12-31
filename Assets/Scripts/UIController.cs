using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject inGameMenuPanel;
    [SerializeField] GameObject scoredPanel;

    [SerializeField] public TMPro.TMP_Text player1ScoreText;
    [SerializeField] public TMPro.TMP_Text player2ScoreText;

    private void Awake()
    {
        // Ensure only one instance of the manager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void MainMenu()
    {
        CloseAllPanel();
        mainMenuPanel.SetActive(true);
    }
    public void PauseMenu()
    {
        CloseAllPanel();
        pauseMenuPanel.SetActive(true);
        inGameMenuPanel.SetActive(true);
    }
    public void InGameMenu()
    {
        CloseAllPanel();
        inGameMenuPanel.SetActive(true);
    }
    public void ScoredUI()
    {
        CloseAllPanel();
        scoredPanel.SetActive(true);
        inGameMenuPanel.SetActive(true);
    }
    private void CloseAllPanel()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        inGameMenuPanel.SetActive(false);
        scoredPanel.SetActive(false);
    }

    public void UpdateScore(string playerName, int playerScore)
    {
        if(playerName == "player1")
        {
            player1ScoreText.text = playerScore.ToString();
        }else if(playerName == "player2")
        {
            player2ScoreText.text = playerScore.ToString();
        }
    }
}
