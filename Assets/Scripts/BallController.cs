using System.Collections;
using UnityEngine;


public class BallController : MonoBehaviour
{
    [SerializeField]
    private Vector3 initialVelocity =new Vector3(-1f,0,0); //Start with this velocity

    [SerializeField]
    private float minVelocity = 1f;
    [SerializeField]
    private float maxSpeed = 1f;

    private Vector3 lastFrameVelocity;
    private Rigidbody2D rb;
    public float speedUpFactor = 1f;

    [SerializeField]
    private GameObject lastTouch;

    [SerializeField]
    private GameObject gameManagerObj;
    GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = gameManagerObj.GetComponent<GameManager>();
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        //ball move operations
        Vector3 velocity = rb.velocity;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        velocity *= speedUpFactor; // Apply speed-up factor
        rb.velocity = velocity;
        lastFrameVelocity = rb.velocity;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {//every collision
        Bounce(collision.contacts[0].normal, collision);
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "player1Wall")
        {
            gameManager.IncreaseScore(1, 2);
            gameManager.Score();
        }
        else if (collision.gameObject.tag == "player2Wall")
        {
            gameManager.IncreaseScore(1, 1);
            gameManager.Score();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collect powerups
        if (collision.transform.tag == "SpeedUp")
        {
            StartCoroutine(SpeedUp());
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "PaddleSpeed")
        {
            StartCoroutine(PaddleSpeed());
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "PaddleSizeUp")
        {
            StartCoroutine(PaddleSizeUp());
            Destroy(collision.gameObject);
        }

    }
    private void Bounce(Vector3 collisionNormal, Collision2D collision)
    {//bounce function
        //speed up every bounce
        speedUpFactor += 0.1f;
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        if (collision.transform.tag == "Paddle")
        {
            //paddle add angle offset 
            direction += new Vector3(0, (transform.position.y - collision.transform.position.y), 0);
            lastTouch = collision.gameObject; //last touch paddle
        }

        //adjust direction to left and right. Instead of up and down
        if (direction.y > 0.75f)
        {
            direction = new Vector3(direction.x, 0.75f, direction.z);
        }
        if (direction.y < -0.75f)
        {
            direction = new Vector3(direction.x, -0.75f, direction.z);
        }
        if (direction.y < 0.05f & direction.y > -0.05f)
        {
            direction = new Vector3(direction.x, UnityEngine.Random.Range(-0.1f, 0.1f), direction.z);
        }
        if (direction.x < 0.01f & direction.x > -0.01f)
        {
            direction = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), direction.z);
        }
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

    private IEnumerator SpeedUp()
    {//speedup power up
        speedUpFactor *= 1.5f;
        yield return new WaitForSeconds(5f);
        speedUpFactor /= 1.75f;
    }
    private IEnumerator PaddleSpeed()
    {//paddle speed power up

        if (lastTouch != null)
        {
            // 2x scale
            //lastTouch.gameObject.transform.localScale = new Vector3(lastTouch.gameObject.transform.localScale.x, 2f, lastTouch.gameObject.transform.localScale.z);

            if (lastTouch.gameObject.name == "pRed")
            {
                lastTouch.gameObject.GetComponent<paddleAI>().speed *= 1.5f;
            }
            if (lastTouch.gameObject.name == "pBlue")
            {
                lastTouch.gameObject.GetComponent<PlayerController>().playerSpeed *= 1.5f;
            }
            yield return new WaitForSeconds(5f);
            if (lastTouch.gameObject.name == "pRed")
            {
                lastTouch.gameObject.GetComponent<paddleAI>().speed /= 1.75f;
            }
            if (lastTouch.gameObject.name == "pBlue")
            {
                lastTouch.gameObject.GetComponent<PlayerController>().playerSpeed /= 1.75f;
            }
        }

    }
    private IEnumerator PaddleSizeUp()
    {//paddle size up power up
        GameObject last = lastTouch.gameObject;
        if (last != null)
        {
            // 2x scale
            last.gameObject.transform.localScale = new Vector3(last.gameObject.transform.localScale.x, 2f, last.gameObject.transform.localScale.z);

            yield return new WaitForSeconds(5f);

            last.gameObject.transform.localScale = new Vector3(last.gameObject.transform.localScale.x, 1f, last.gameObject.transform.localScale.z);
        }

    }
}
