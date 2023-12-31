using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSizeUp : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        CollectibleManager.Instance.CollectPaddleSizeUp();
        Destroy(gameObject);
    }
}
