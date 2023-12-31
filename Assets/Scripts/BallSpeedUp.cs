using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedUp : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        CollectibleManager.Instance.CollectBallSpeedUp();
        Destroy(gameObject);
    }
}
