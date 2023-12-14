using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsPaused = true;
    //public GameObject pauseMenuUI;
    public int scorePlayer1 = 0; // Score for Player 1
    public int scorePlayer2 = 0; // Score for Player 2

    public GameObject player1;
    public GameObject player2;
    public GameObject ball;
    private Vector3 lastFrameBallVelocity;

    void Start()
    {
        Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameIsPaused)
            {
                Resume();
                //ResetAfterScore();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Score()
    {
        //Write Activate Scored UI here in future
        Pause();
    }
    public void IncreaseScore(int increment, int playerNumber) // Modified function
    {
        if (playerNumber == 1)
        {
            scorePlayer1 += increment;
            Debug.Log("Score Player 1: " + scorePlayer1);
        }
        else if (playerNumber == 2)
        {
            scorePlayer2 += increment;
            Debug.Log("Score Player 2: " + scorePlayer2);
        }
    }
}
