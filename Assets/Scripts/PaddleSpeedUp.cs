using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSpeedUp : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        CollectibleManager.Instance.CollectPaddleSpeedUp();
        Destroy(gameObject);
    }
}
