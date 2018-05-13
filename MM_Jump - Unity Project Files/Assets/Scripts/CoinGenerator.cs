using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPool coinPool;

    public float distanceBetweenCoins;

    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin0 = coinPool.GetPooledObject();
        coin0.transform.position = startPosition;
        coin0.SetActive(true);

        GameObject coin1 = coinPool.GetPooledObject();
        coin1.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        coin2.SetActive(true);
    }
}
