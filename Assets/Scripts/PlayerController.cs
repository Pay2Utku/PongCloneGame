using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField]
    public float playerSpeed = 1f;

    float minX, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        minX = -2;
        maxX = -2;
        minY = -1;
        maxY = 1;
    }

    void FixedUpdate()
    {
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = Vector2.up * moveY * playerSpeed;
        movement.x = 0; // ensures player doesn't move horizontally
        rb.velocity = movement;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerSpeed < 1f)//Speed correction because of power ups
        {
            playerSpeed = 1f;
        }
        Vector2 clampedPosition = new Vector2(
          Mathf.Clamp(transform.position.x, minX, maxX),
          Mathf.Clamp(transform.position.y, minY, maxY)
        );

        transform.position = clampedPosition;
    }
}
