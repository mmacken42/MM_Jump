using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public float scoreCounter;
    public float highScoreCounter;

    public float pointsPerSecond = 5f;

    public bool scoreIncreasing;

	void Start ()
    {
        scoreIncreasing = true;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCounter = PlayerPrefs.GetFloat("HighScore");
        }
    }

	void Update ()
    {
        if (scoreIncreasing)
        {
            scoreCounter += pointsPerSecond * Time.deltaTime;
        }
        
        if (scoreCounter > highScoreCounter)
        {
            highScoreCounter = scoreCounter;
            PlayerPrefs.SetFloat("HighScore", highScoreCounter);
        }

        scoreText.text = "SCORE: " + Mathf.Round(scoreCounter);
        highScoreText.text = "HIGH SCORE: " + Mathf.Round(highScoreCounter);
	
	}

    public void AddScore(int pointsToAdd)
    {
        scoreCounter += pointsToAdd;
    }
}
