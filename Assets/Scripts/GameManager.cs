using UnityEngine;


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
    private Vector3 lastFrameBallVelocity;

    public GameState currentState;
    void Start()
    {
        Pause();
        // Set initial game state
        currentState = GameState.Paused;

    }

    void Update()
    {
        // Example: Switching states based on player input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                /*case GameState.MainMenu:
                    StartGame();
                    break;*/
                case GameState.InGame:
                    Pause();
                    break;
                case GameState.Paused:
                    Resume();
                    break;
                case GameState.Scored:
                    NewBall();
                    break;
                    /*case GameState.GameOver:
                        RestartGame();
                        break;*/
            }
        }


    }

    void Resume()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        currentState = GameState.InGame;
    }

    void Pause()
    {
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }
    public void Score()
    {
        //Write Activate Scored UI here in future
        currentState = GameState.Scored;

        player1.gameObject.GetComponent<PlayerController>().enabled = false;
        player1.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, 1f, 0);

        player2.GetComponent<paddleAI>().enabled = false;
        player2.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, -1f, 0);

        ball.gameObject.SetActive(false);
        ball.GetComponent<BallController>().enabled = false;
        //Time.timeScale = 0.2f;
    }
    public void NewBall()
    {
        //Check if there are controllers 
        player1.gameObject.GetComponent<PlayerController>().enabled = true;
        player1.transform.position = new Vector3(-2f, 0, 0);
        player1.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        player1.transform.localScale = Vector3.one;
        player1.gameObject.GetComponent<PlayerController>().playerSpeed = 1f;

        player2.GetComponent<paddleAI>().enabled = true;
        player2.transform.position = new Vector3(2f, 0, 0);
        player2.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        player2.transform.localScale = Vector3.one;
        player2.gameObject.GetComponent<paddleAI>().speed = 1f;

        ball.gameObject.SetActive(true);
        ball.GetComponent<BallController>().enabled = true;
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
        currentState = GameState.Paused;
        Time.timeScale = 0f;
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
