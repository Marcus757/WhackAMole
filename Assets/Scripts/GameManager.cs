﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject moleContainer;
    public Player player;
    public GameObject hammer;
    public Text infoText;
    public static int level = 1;
    public float spawnDuration;
    public float spawnDecrement = 0.1f;
    public float minimumSpawnDuration = 0.5f;
    public float roundDuration;
    public HighScoresData highScoresData;
    public UIManager uiManager;
    public bool DebugMode = false;
    public float debugGameTime = 10f;
    public int displayUIOnLevel;

    private float gameTimer;
    private Mole[] moles;
    private Repository repository;
    private float spawnTimer;
    private float countdownTimer;
    private string scoreFileName;
    private float endDelay = 5f;
    private WaitForSeconds endWait;

    // Use this for initialization
    void Start() {
        endWait = new WaitForSeconds(endDelay);
        moles = moleContainer.GetComponentsInChildren<Mole>();
        repository = new Repository();
        StartCoroutine(GameLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsResetGamePressed())
            ResetGame();

        ToggleGazePointer();
    }

    private IEnumerator GameLoop()
    {
        SetupGame();
        yield return StartCoroutine(WaitForGrabbedItem());
        yield return StartCoroutine(LevelStarting());
        yield return StartCoroutine(LevelPlaying());
        yield return StartCoroutine(LevelEnding());

        if (level == displayUIOnLevel)
        {
            yield return StartCoroutine(ShowNewHighScoreUI());
            yield return StartCoroutine(ShowScoreLeaderboardUI());
        }
            
        HideScoreLeaderboardDisplay();
        ChangeLevel();
        StartCoroutine(GameLoop());
    }

    private void SetupGame()
    {
        if (level == 1)
            player.playerScore.ResetScore();

        infoText.text = "Grab the hammer and get ready!";
        countdownTimer = 10f;

        if (DebugMode)
            gameTimer = debugGameTime;
        else
            gameTimer = roundDuration;
    }

    private IEnumerator WaitForGrabbedItem()
    {
        while (!player.IsItemGrabbed(hammer))
        {
            yield return null;
        }
    }

    private IEnumerator LevelStarting()
    {
        while (countdownTimer > 0)
        {
            infoText.text = Mathf.Floor(countdownTimer).ToString();
            countdownTimer -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator LevelPlaying()
    {
        while (gameTimer > 0f)
        {
            if (DebugMode && player is NonVRPlayer)
            {
                var nonVRPlayer = (NonVRPlayer)player;

                if (nonVRPlayer.IsPrimaryMouseButtonPressed())
                    player.playerScore.score++;
            }

            PlayGame();
            gameTimer -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator LevelEnding()
    {
        infoText.text = "End of round " + level + "!";
        infoText.text += "\nScore: " + Mathf.Floor(player.playerScore.score);
        infoText.text += "\nTotal Score: " + Mathf.Floor(player.playerScore.score);
        yield return endWait;
    }

    private void PlayGame()
    {
        infoText.text = 
            "Time: " + Mathf.Floor(gameTimer) + 
            "\nScore: " + player.playerScore.score + 
            "\nLevel: " + level;
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            moles[UnityEngine.Random.Range(0, moles.Length)].Rise();
            spawnDuration -= spawnDecrement;

            if (spawnDuration < minimumSpawnDuration)
                spawnDuration = minimumSpawnDuration;

            spawnTimer = spawnDuration;
        }
    }
    
    private void ChangeLevel()
    {
        if (level == 3)
            level = 1;
        else
            level++;
    }

    private IEnumerator ShowNewHighScoreUI()
    {
        // Compare player's score with high scores
        highScoresData.highScores = repository.GetAllHighScores();

        if (IsPlayerScoreNewHighScore(player.playerScore.score, highScoresData.highScores))
            uiManager.ShowNewHighScoreUI();

        while (IsNewHighScoreUIDisplayed())
        {
            if (!OVRGazePointer.instance.gameObject.activeSelf)
                OVRGazePointer.instance.gameObject.SetActive(true);

            yield return null;
        }

        if (OVRGazePointer.instance.gameObject.activeSelf)
            OVRGazePointer.instance.gameObject.SetActive(false);
    }

    private bool IsNewHighScoreUIDisplayed()
    {
        return GameObject.FindObjectOfType<NewHighScoreUI>() == null ? false : true;
    }

    private IEnumerator ShowScoreLeaderboardUI()
    {
        highScoresData.highScores = repository.GetAllHighScores();
        uiManager.ShowScoreLeaderboard();

        while (!player.IsEnterPressed())
        {
            yield return null;
        }
    }
    
    private void HideScoreLeaderboardDisplay()
    {
        var scoreLeaderboard = GameObject.FindObjectOfType<ScoreLeaderboardUI>();
        if (scoreLeaderboard != null)
            Destroy(scoreLeaderboard.gameObject);
    }

    private bool IsPlayerScoreNewHighScore(int score, List<HighScore> highScores)
    {
        if (highScores.Count < 10)
            return true;

        return score > highScores.Select(highScore => highScore.Score).Min();
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ToggleGazePointer()
    {
        if (IsNewHighScoreUIDisplayed() && !OVRGazePointer.instance.gameObject.activeSelf)
            OVRGazePointer.instance.gameObject.SetActive(true);
        else if (!IsNewHighScoreUIDisplayed() && OVRGazePointer.instance.gameObject.activeSelf)
            OVRGazePointer.instance.gameObject.SetActive(false);
    }

    #region Testing
    private List<HighScore> GetMockHighScores()
    {
        List<HighScore> highScores = new List<HighScore>();
        highScores.Add(new HighScore(1, 134, "MSN", DateTime.Now.AddDays(320)));
        highScores.Add(new HighScore(2, 400, "CCM", DateTime.Now.AddDays(720)));
        highScores.Add(new HighScore(3, 45, "MNO", DateTime.Now.AddDays(-720)));
        highScores.Add(new HighScore(4, 100, "JKL", DateTime.Now.AddDays(-400)));
        highScores.Add(new HighScore(5, 200, "MSN", DateTime.Now.AddDays(365)));
        highScores.Add(new HighScore(6, 50, "GHI", DateTime.Now.AddDays(-100)));
        highScores.Add(new HighScore(7, 75, "TAM", DateTime.Now.AddDays(100)));
        highScores.Add(new HighScore(8, 30, "DEF", DateTime.Now));
        highScores.Add(new HighScore(9, 225, "ABC", DateTime.Now));
        highScores.Add(new HighScore(10, 125, "BFF", DateTime.Now));
        return highScores;
    }

    private int GetRandomScore()
    {
        return UnityEngine.Random.Range(0, 500);
    }
    #endregion

}
