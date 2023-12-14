using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> PowerUps = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {//spawn random PowerUp to Random Position
        Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Instantiate(PowerUps[Random.Range(0,3)], randomPosition, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        StartCoroutine(SpawnPowerUp());
    }
}
