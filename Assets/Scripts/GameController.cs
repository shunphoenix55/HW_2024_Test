using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[DefaultExecutionOrder(-100)]
// Singleton class that controls the game, scoring, and UI
public class GameController : MonoBehaviour
{
    public static GameController instance;


    private int _score = 0;

    private GameObject player;

    public TMP_Text scoreText;
    // Target score to win the game
    public int targetScore = 10;
    // Height at which player dies
    public float deathHeight = -5f;

    [Header("UI Screens")]
    public GameObject winScreen;
    public GameObject loseScreen;


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


    void Start()
    {
        UpdateScoreText();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        // Find player if it's null
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");

        }

        if (player.transform.position.y < deathHeight)
        {
            Lose();
        }
    }
    public void UpdateScoreText()
    {
        scoreText.text = Score.ToString();
    }

    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }

}
