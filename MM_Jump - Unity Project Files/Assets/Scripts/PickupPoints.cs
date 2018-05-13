using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour
{
    public int scoreToGive;

    private ScoreManager manager;

    private AudioSource coinSound;

	void Start ()
    {
        manager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.AddScore(scoreToGive);
            if (coinSound.isPlaying)
            {
                coinSound.Stop();
            }
            coinSound.Play();
            gameObject.SetActive(false);
        }
    }
}
