using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class paddleAI : MonoBehaviour
{
    [SerializeField]
    GameObject ball;
    [SerializeField]
    float speed = 1f; // Base speed for calculations
    public float smoothTime = 0.3f;
    public Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ballY = ball.transform.position.y;
        Vector3 targetPoint = new Vector3(transform.position.x,ballY, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPoint,ref velocity,smoothTime, speed);
    }
}
