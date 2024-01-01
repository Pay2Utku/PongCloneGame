using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    InGame,
    Scored,
    Paused,
    GameOver
}
public class GameManager : MonoBehaviour
{
    //public GameObject pauseMenuUI;
    public int scorePlayer1 = 0; // Score for Player 1
    public int scorePlayer2 = 0; // Score for Player 2

    public GameObject player1;
    public GameObject player2;
    public GameObject ball;

    public GameState currentState;

    public static GameManager Instance;

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
    void Start()
    {
        // Set initial game state
        currentState = GameState.MainMenu;
        UIController.Instance.MainMenu();
        Time.timeScale = 0f;
    }

    void Update()
    {
        // Example: Switching states based on player input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case GameState.MainMenu:
                    //Game Starts with Buttons.
                    break;
                case GameState.InGame:
                    Pause();
                    break;
                case GameState.Paused:
                    Resume();
                    break;
                case GameState.Scored:
                    NewBall();
                    break;
                case GameState.GameOver:
                    RestartGame();
                    break;
            }
        }

        if(scorePlayer1 == 7)
        {
            currentState = GameState.GameOver;
            UIController.Instance.Player1Win();
        }
        else if (scorePlayer2 == 7)
        {
            currentState = GameState.GameOver;
            UIController.Instance.Player2Win();
        }

    }
    public void SelectSolo()
    {
        player2.GetComponent<paddleAI>().enabled = true;
        player2.GetComponent<PlayerController>().enabled = false;
        Debug.Log("Solo selected");
        Pause();
    }
    public void SelectMultiplayer()
    {
        player2.GetComponent<paddleAI>().enabled = false;
        player2.GetComponent<PlayerController>().enabled = true;
        Debug.Log("Multiplayer selected");
        Pause();
    }
    void Resume()
    {
        UIController.Instance.InGameMenu();
        Time.timeScale = 1f;
        currentState = GameState.InGame;
    }

    void Pause()
    {
        UIController.Instance.PauseMenu();
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }
    public void Score()
    {
        UIController.Instance.ScoredUI();
        currentState = GameState.Scored;
        /*
        ball.gameObject.SetActive(false);
        ball.GetComponent<BallController>().enabled = false;*/

        ball.GetComponent<SpriteRenderer>().enabled = false;
        ball.transform.position = new Vector3(0, 0, 0);
        ball.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        ball.gameObject.GetComponent<BallController>().speedUpFactor = 0;
    }
    public void NewBall()
    {
        UIController.Instance.InGameMenu();
        /*
        ball.gameObject.SetActive(true);
        ball.GetComponent<BallController>().enabled = true;*/
        ball.GetComponent<SpriteRenderer>().enabled = true;
        ball.transform.position = new Vector3(0, 0, 0);
        ball.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0, 0);
        ball.gameObject.GetComponent<BallController>().speedUpFactor = 1f;

        
        foreach(var i in GameObject.FindGameObjectsWithTag("SpeedUp"))
        {
            Destroy(i.gameObject);
        }
        foreach (var i in GameObject.FindGameObjectsWithTag("PaddleSpeed"))
        {
            Destroy(i.gameObject);
        }
        foreach (var i in GameObject.FindGameObjectsWithTag("PaddleSizeUp"))
        {
            Destroy(i.gameObject);
        }

        Pause();
    }
    public void IncreaseScore(int increment, int playerNumber) // Modified function
    {
        if (playerNumber == 1)
        {
            scorePlayer1 += increment;
            //UIController.Instance.UpdateScore("player1", scorePlayer1);
            UIController.Instance.player1ScoreText.text = scorePlayer1.ToString();
            Debug.Log("Score Player 1: " + scorePlayer1);
        }
        else if (playerNumber == 2)
        {
            scorePlayer2 += increment;
            //UIController.Instance.UpdateScore("player2", scorePlayer2);
            UIController.Instance.player2ScoreText.text = scorePlayer2.ToString();
            Debug.Log("Score Player 2: " + scorePlayer2);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
