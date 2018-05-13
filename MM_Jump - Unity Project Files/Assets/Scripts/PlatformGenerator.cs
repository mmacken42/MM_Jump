using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour
{
    //public GameObject[] platforms;
    private int platformIndex;
    private float[] platformWidths;

    public Transform generationPoint;
    private float distanceBetween;
    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    public ObjectPool[] pools;

    private float minHeight;
    private float maxHeight;
    public Transform maxHeightPoint;
    public float maxHeightChange;
    private float heightChange;

    private CoinGenerator coinGen;

    public float randomCoinThreshold = 75f;

    public float randomSpikeTheshold = 50f;
    public ObjectPool spikePool;

	void Start ()
    {
        //platformWidth = pool.GetPooledObject().GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[pools.Length];

        for (int i = 0; i < platformWidths.Length; ++i)
        {
            platformWidths[i] = pools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        coinGen = FindObjectOfType<CoinGenerator>();

    }

	void Update ()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformIndex = Random.Range(0, pools.Length);

            heightChange = transform.position.y + Random.Range(-maxHeightChange, maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformIndex] / 2) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(platforms[platformIndex], transform.position, transform.rotation);

            GameObject newPlatform = pools[platformIndex].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                coinGen.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomSpikeTheshold)
            {
                GameObject spike = spikePool.GetPooledObject();

                
                float spikeXPosition = Random.Range(-platformWidths[platformIndex] / 2f + 1f, platformWidths[platformIndex] / 2f - 1f);
                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);
                spike.transform.position = transform.position + spikePosition;
                spike.transform.rotation = transform.rotation;
                spike.SetActive(true);
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformIndex] / 2), transform.position.y, transform.position.z);
        }
	
	}
}
