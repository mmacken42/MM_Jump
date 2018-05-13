using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController player;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformArr;

    private ScoreManager scoreManager;

    public DeathMenu deathScreen;

	void Start ()
    {
        platformStartPoint = platformGenerator.position;

        playerStartPoint = player.transform.position;

        scoreManager = FindObjectOfType<ScoreManager>();
	}

    public void RestartGame()
    {
        //StartCoroutine("RestartGameCoroutine");

        scoreManager.scoreIncreasing = false;

        player.gameObject.SetActive(false);

        deathScreen.gameObject.SetActive(true);
    }

    public void Reset()
    {
        deathScreen.gameObject.SetActive(false);

        scoreManager.scoreIncreasing = false;

        player.gameObject.SetActive(false);

        platformArr = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformArr.Length; ++i)
        {
            platformArr[i].gameObject.SetActive(false);
        }

        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;

        player.gameObject.SetActive(true);

        scoreManager.scoreCounter = 0;
        scoreManager.scoreIncreasing = true;
    }
}
