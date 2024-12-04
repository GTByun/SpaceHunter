using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static int HighScore;
    public TextMeshProUGUI scoreText, highScoreText;
    
    private void Start()
    {
        int Score = GameManager.score;
        scoreText.text = Score.ToString();
        if (Score > HighScore)
        {
            HighScore = Score;
        }
        highScoreText.text = HighScore.ToString();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
