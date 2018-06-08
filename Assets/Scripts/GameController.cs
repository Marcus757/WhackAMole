using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;
using System;
using System.Linq;
using SQLiter;

public class GameController : MonoBehaviour {

	public GameObject moleContainer;
	public Player player;
	public Text infoText;
    public static int level = 1;
	public float spawnDuration;
	public float spawnDecrement = 0.1f;
	public float minimumSpawnDuration = 0.5f;
	public float gameTimer;
    public ScoreLeaderboard scoreLeaderboardPrefab;
    public SQLite sqLitePrefab;
    public HighScore highScorePrefab;
    public static SQLite sqLite;
    public static bool areInitialsEntered = false;
    public bool DebugMode = false;

    private Mole[] moles;
	private float spawnTimer;
	private float resetTimer;
    private float countdownTimer;
    private bool isGameInProgress;
    private string scoreFileName;
    private bool areScoresDisplayed = false;
    private List<HighScore> highScores;
    private bool isNewHighScoreUIDisplayed = false;
    private HighScore highScore;
    private int levelToShowUI = 3;
    private bool isGameOverDisplayed = false;

    public void Awake()
    {
        if (DebugMode)
        {
            player.score = GetRandomScore();
            levelToShowUI = 1;
            gameTimer = 10;
        }
    }

    // Use this for initialization
    void Start () {
		moles = moleContainer.GetComponentsInChildren<Mole>();
        infoText.text = "Grab the hammer and get ready!";
        isGameInProgress = false;
        isGameOverDisplayed = false;
        countdownTimer = 10f;
        resetTimer = 5f;
        scoreFileName = "scores.txt";
        highScore = null;

        if (sqLite == null)
            sqLite = Instantiate(sqLitePrefab);

        Instantiate(scoreLeaderboardPrefab);
    }
	
	// Update is called once per frame
	void Update () {

        if (player.IsResetGamePressed())
            player.ResetGame();

        if (!isGameInProgress && player.IsHammerGrabbed())
            isGameInProgress = true;

        if (!isGameInProgress)
            return;

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
            Player.totalScore += player.score;
            infoText.text = "Game over!";
            infoText.text += "\nScore: " + Mathf.Floor(player.score);
            infoText.text += "\nTotal Score: " + Mathf.Floor(Player.totalScore);
            isGameOverDisplayed = true;    
        }

        resetTimer -= Time.deltaTime;

        if (resetTimer > 0f)
            return;

        if (level == levelToShowUI)
        {
            // Compare player's score with high scores
            highScores = sqLite.GetAllHighScores();
            if (!isNewHighScoreUIDisplayed && !areScoresDisplayed && IsPlayerScoreNewHighScore(Player.totalScore, highScores))
                ShowNewHighScoreUI();

            if (isNewHighScoreUIDisplayed)
            {
                if (!areInitialsEntered)
                    return;

                //highScore = null;
            }
                
            if (!areScoresDisplayed)
            {
                isNewHighScoreUIDisplayed = false;
                areInitialsEntered = false;
                LoadScores();
            }
        }

        if (player.IsEnterPressed())
            areScoresDisplayed = false;

        if (areScoresDisplayed)
            return;

        ChangeLevel();
        player.ResetGame();
        
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

    private void GameOver()
    {

    }

    private void ChangeLevel()
    {
        if (level == 3)
            level = 1;
        else
            level++;
    }

    private void LoadScores()
    {
        List<HighScore> highScores = sqLite.GetAllHighScores();
        //highScores = highScores.OrderByDescending(highScore => highScore.Score).Take(10).ToList();
        ScoreLeaderboard scoreLeaderboard = (ScoreLeaderboard)Instantiate(scoreLeaderboardPrefab);
        scoreLeaderboard.LoadScores(highScores);
        areScoresDisplayed = true;
    }

    private bool IsPlayerScoreNewHighScore(int score, List<HighScore> highScores)
    {
        if (highScores.Count < 10)
            return true;

        return score > highScores.Select(highScore => highScore.Score).Min();
    }

    private void ShowNewHighScoreUI()
    {
        highScore = (HighScore)Instantiate(highScorePrefab);
        highScore.LoadScore(Player.totalScore);
        highScore.ShowNewHighScoreUI();
        isNewHighScoreUIDisplayed = true;
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
