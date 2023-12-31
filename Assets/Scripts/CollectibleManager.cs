using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private BallController ballController;

    private GameObject lastTouch;

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
    private void Start()
    {
        ballController = FindAnyObjectByType<BallController>().GetComponent<BallController>();
    }
    public void LastTouch(GameObject gameObject)
    {
        lastTouch = gameObject;
    }
    public void CollectBallSpeedUp()
    {
        // Start a coroutine or perform actions as needed
        StartCoroutine(BallSpeedUpEffect());
    }
    private IEnumerator BallSpeedUpEffect()
    {
        ballController.speedUpFactor *= 1.5f;
        yield return new WaitForSeconds(5.0f);
        ballController.speedUpFactor /= 1.2f;
        if (ballController.speedUpFactor < 1.2f)
        {
            ballController.speedUpFactor = 1.2f;
        }
    }
    public void CollectPaddleSpeedUp()
    {
        // Start a coroutine or perform actions as needed
        StartCoroutine(PaddleSpeedUpEffect());
    }
    private IEnumerator PaddleSpeedUpEffect()
    {//paddle speed power up
        if(lastTouch != null)
        {
            //lastTouch.gameObject?.GetComponent<PlayerController>().SpeedEffect(1.5f);
            if (lastTouch.gameObject?.GetComponent<PlayerController>().isActiveAndEnabled == true)
            {
                lastTouch.gameObject?.GetComponent<PlayerController>().SpeedEffect(1.5f);
            }
            //lastTouch.gameObject?.GetComponent<paddleAI>().SpeedEffect(1.5f);
            if (lastTouch.gameObject?.GetComponent<paddleAI>().isActiveAndEnabled == true)
            {
                lastTouch.gameObject?.GetComponent<paddleAI>().SpeedEffect(1.5f);
            }
            yield return new WaitForSeconds(5f);
            //lastTouch.gameObject?.GetComponent<PlayerController>().SpeedEffect(0.8f);
            if (lastTouch.gameObject?.GetComponent<PlayerController>().isActiveAndEnabled == true)
            {
                lastTouch.gameObject?.GetComponent<PlayerController>().SpeedEffect(0.8f);
            }
            //lastTouch.gameObject?.GetComponent<paddleAI>().SpeedEffect(0.8f);
            if (lastTouch.gameObject?.GetComponent<paddleAI>().isActiveAndEnabled == true)
            {
                lastTouch.gameObject?.GetComponent<paddleAI>().SpeedEffect(0.8f);
            }
        }
    }
    public void CollectPaddleSizeUp()
    {
        // Start a coroutine or perform actions as needed
        StartCoroutine(PaddleSizeUpEffect());
    }
    private IEnumerator PaddleSizeUpEffect()
    {//paddle size up power up
        if (lastTouch.gameObject != null)
        {
            GameObject last = lastTouch.gameObject;
            // 2x scale
            last.gameObject.transform.localScale = new Vector3(2f, 2f, last.gameObject.transform.localScale.z);

            yield return new WaitForSeconds(5f);

            last.gameObject.transform.localScale = new Vector3(1f, 1f, last.gameObject.transform.localScale.z);
        }

    }
}
