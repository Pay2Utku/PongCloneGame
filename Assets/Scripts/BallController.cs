using System.Collections;
using UnityEngine;


public class BallController : MonoBehaviour
{
    [SerializeField]
    private Vector3 initialVelocity =new Vector3(-1f,0,0); //Start with this velocity

    [SerializeField]
    private float minVelocity = 1f;
    [SerializeField]
    private float clampSpeed = 1f;

    private Vector3 lastFrameVelocity;
    private Rigidbody2D rb;
    public float speedUpFactor = 1f;

    [SerializeField]
    private GameObject lastTouch;

    [SerializeField]
    private GameObject gameManagerObj;
    GameManager gameManager;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioClip boomClip;
    [SerializeField] ParticleSystem boomEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager = gameManagerObj.GetComponent<GameManager>();
        rb.velocity = initialVelocity;
    }

    private void FixedUpdate()
    {
        //ball move operations
        Vector3 velocity = rb.velocity;
        velocity = Vector3.ClampMagnitude(velocity, clampSpeed);
        velocity *= speedUpFactor; // Apply speed-up factor
        rb.velocity = velocity;
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {//every collision
        Bounce(collision.contacts[0].normal, collision);
        if (collision.gameObject.tag == "player1Wall")
        {
            Instantiate(boomEffect, this.gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(boomClip);

            gameManager.IncreaseScore(1, 2);
            gameManager.Score();
        }
        else if (collision.gameObject.tag == "player2Wall")
        {
            Instantiate(boomEffect, this.gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(boomClip);

            gameManager.IncreaseScore(1, 1);
            gameManager.Score();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collect powerups
        collision?.GetComponent<ICollectible>().Collect();
    }
    private void Bounce(Vector3 collisionNormal, Collision2D collision)
    {//bounce function
        //speed up every bounce
        speedUpFactor += 0.1f;
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        if (collision.transform.tag == "Paddle")
        {
            CollectibleManager.Instance.LastTouch(collision.gameObject); //last touch paddle
        }
        rb.velocity = direction * Mathf.Max(speed, minVelocity);

        //audio
        audioSource.PlayOneShot(audioClip);
    }
    
    
}
