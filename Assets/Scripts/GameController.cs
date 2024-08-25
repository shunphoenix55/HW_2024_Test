using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-100)]
// Singleton class that controls the game, scoring, and UI
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public TMP_Text scoreText;

    private int _score = 1;

    private void Awake()
    {
        // Singleton class
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            UpdateScoreText();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = Score.ToString();
    }

}
