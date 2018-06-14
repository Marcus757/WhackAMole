using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameController : MonoBehaviour {

	public GameObject moleContainer;
	public Player player;
    public Hammer hammerPrefab;
	public Text infoText;
    public static int level = 1;
	public float spawnDuration;
	public float spawnDecrement = 0.1f;
	public float minimumSpawnDuration = 0.5f;
	public float gameTimer;
    public ScoreLeaderboard scoreLeaderboardPrefab;
    public HighScore highScorePrefab;
    public bool DebugMode = false;

    private Mole[] moles;
    private GameObject hammer;
    private Repository repository;
    private int totalScore;
	private float spawnTimer;
	private float resetTimer;
    private float countdownTimer;
    private bool isGameInProgress;
    private string scoreFileName;
    private List<HighScore> highScores;
    private HighScore highScore;
    private int displayUIOnLevel = 3;
    private bool isGameOverDisplayed = false;
    
    // Use this for initialization
    void Start () {
        if (DebugMode)
        {
            displayUIOnLevel = 1;
            gameTimer = 10;
        }

        moles = moleContainer.GetComponentsInChildren<Mole>();
        infoText.text = "Grab the hammer and get ready!";
        isGameInProgress = false;
        isGameOverDisplayed = false;
        countdownTimer = 10f;
        resetTimer = 5f;
        highScore = null;
        repository = new Repository();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.IsResetGamePressed())
            ResetGame();

        if (!isGameInProgress && player.IsItemGrabbed(hammer))
            isGameInProgress = true;

        if (!isGameInProgress)
            return;

        if (DebugMode && player is NonVRPlayer)
        {
            var nonVRPlayer = (NonVRPlayer)player;

            if (nonVRPlayer.IsPrimaryMouseButtonPressed())
                player.score++;
        }

        HideGazePointer();

        if (countdownTimer > 0)
        {
            infoText.text = Mathf.Floor(countdownTimer).ToString();
            countdownTimer -= Time.deltaTime;
            return;
        }

        gameTimer -= Time.deltaTime;

        if (gameTimer > 0f) {
            PlayGame();
            return;
        }

        // Game Over, gameTimer <= 0f
        if (!isGameOverDisplayed)
        {
            totalScore += player.score;
            infoText.text = "Game over!";   
            infoText.text += "\nScore: " + Mathf.Floor(player.score);
            infoText.text += "\nTotal Score: " + Mathf.Floor(totalScore);
            isGameOverDisplayed = true;    
        }

        resetTimer -= Time.deltaTime;

        if (resetTimer > 0f)
            return;

        if (level == displayUIOnLevel)
        {
            // Compare player's score with high scores
            highScores = repository.GetAllHighScores();
            if (!IsNewHighScoreUIDisplayed() && !IsScoreLeaderboardUIDisplayed() && IsPlayerScoreNewHighScore(totalScore, highScores))
                ShowNewHighScoreUI();

            if (IsNewHighScoreUIDisplayed())
                return;
                
            if (!IsScoreLeaderboardUIDisplayed())
                ShowScoreLeaderboardUI();
        }

        if (player.IsEnterPressed())
            HideScoreLeaderboardDisplay();

        if (IsScoreLeaderboardUIDisplayed())
            return;

        ChangeLevel();
        ResetGame();
    }

    private void PlayGame()
    {
        infoText.text = "Time: " + Mathf.Floor(gameTimer) + "\nScore: " + player.score + "\nLevel: " + level;
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

    private void ShowNewHighScoreUI()
    {
        highScore = (HighScore)Instantiate(highScorePrefab);
        highScore.LoadScore(totalScore);
        //highScore.ShowNewHighScoreUI();
        NewHighScoreUI.Create(highScore);
    }

    private bool IsNewHighScoreUIDisplayed()
    {
        return GameObject.FindObjectOfType<NewHighScoreUI>() == null ? false : true;
    }

    private void ShowScoreLeaderboardUI()
    {
        List<HighScore> highScores = repository.GetAllHighScores();
        ScoreLeaderboard scoreLeaderboard = (ScoreLeaderboard)Instantiate(scoreLeaderboardPrefab);
        scoreLeaderboard.LoadScores(highScores);
    }

    private bool IsScoreLeaderboardUIDisplayed()
    {
        return GameObject.FindObjectOfType<ScoreLeaderboardUI>() == null ? false : true;
    }

    private void HideScoreLeaderboardDisplay()
    {
        var scoreLeaderboard = GameObject.FindObjectOfType<ScoreLeaderboardUI>();
        if (scoreLeaderboard != null)
            Destroy(scoreLeaderboard);
    }

    private bool IsPlayerScoreNewHighScore(int score, List<HighScore> highScores)
    {
        if (highScores.Count < 10)
            return true;

        return score > highScores.Select(highScore => highScore.Score).Min();
    }

    private void HideGazePointer()
    {
        OVRGazePointer ovrGazePointer = GameObject.FindObjectOfType<OVRGazePointer>();
        
        if (player.IsItemGrabbed(hammer))
            OVRGazePointer.instance.RequestHide();
        else
            OVRGazePointer.instance.RequestShow();
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
