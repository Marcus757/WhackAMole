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
    public NewHighScoreDisplay newHighScoreDisplayPrefab;
    public SQLite sqlLite;

    private Mole[] moles;
	private float spawnTimer;
	private float resetTimer;
    private float countdownTimer;
    private bool isGameInProgress;
    private string scoreFileName;
    private bool areScoresDisplayed = false;
    private List<HighScore> highScores;
    private bool isNewHighScoreUIDisplayed = false;

    // Use this for initialization
    void Start () {
		moles = moleContainer.GetComponentsInChildren<Mole> ();
        infoText.text = "Grab the hammer and get ready!";
        isGameInProgress = false;
        countdownTimer = 10f;
        resetTimer = 5f;
        scoreFileName = "scores.txt";
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
            infoText.text = "Time: " + Mathf.Floor(gameTimer) + "\nScore: " + player.score + "\nLevel: " + level;
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f) {
                moles[UnityEngine.Random.Range(0, moles.Length)].Rise();
                spawnDuration -= spawnDecrement;

                if (spawnDuration < minimumSpawnDuration) {
                    spawnDuration = minimumSpawnDuration;
                }

                spawnTimer = spawnDuration;
            }

            return;
        }

        if (gameTimer <= 0f)
        {
            infoText.text = "Game over! \nYour score: " + Mathf.Floor(player.score);
            resetTimer -= Time.deltaTime;

            if (resetTimer > 0f)
                return;

            if (level == 1)
            //if (level == 3)
            {
                // Compare player's score with high scores
                highScores = sqlLite.GetAllHighScores();
                if (!isNewHighScoreUIDisplayed && IsPlayerScoreNewHighScore(player.score, highScores))
                    ShowNewHighScoreUI();

                if (!newHighScoreDisplayPrefab.areInitialsEntered)
                    return;

                // If player's score ranks in top ten, then present player with initials input ui.
                // If player's score does not rank in top then display high scores
                // Player enters initials and player's score gets saved to db.
                // High scores are then retrieved from database and displayed on screen


                if (!areScoresDisplayed)
                    LoadScores();

                //SaveScore(Player.totalScore, "NWA", System.DateTime.Now.Date); 
            }

            if (player.IsEnterPressed())
                areScoresDisplayed = false;

            if (areScoresDisplayed)
                return;

            ChangeLevel();
            player.ResetGame();
        }
    }

    private void ChangeLevel()
    {
        if (level == 3)
            level = 1;
        else
            level++;
    }

    private void SaveScore(int score, string initials, System.DateTime date)
    {
        //JSONClass highScore = new JSONClass();
        //highScore.Add("score", new JSONData(score));
        //highScore.Add("initials", new JSONData(initials));
        //highScore.Add("date", new JSONData(date.ToString()));
        //highScore.SaveToFile(scoreFileName);
    }

    private void LoadScores()
    {
        List<HighScore> highScores = sqlLite.GetAllHighScores();
        //highScores = highScores.OrderByDescending(highScore => highScore.Score).Take(10).ToList();
        ScoreLeaderboard scoreLeaderboard = (ScoreLeaderboard)Instantiate(scoreLeaderboardPrefab);
        scoreLeaderboard.LoadScores(highScores);
        areScoresDisplayed = true;
    }

    private bool IsPlayerScoreNewHighScore(int score, List<HighScore> scores)
    {
        return true;
    }

    private void ShowNewHighScoreUI()
    {
        NewHighScoreDisplay newHighScoreDisplay = Instantiate(newHighScoreDisplayPrefab);
        isNewHighScoreUIDisplayed = true;
    }

    #region Testing
    private List<HighScore> GetMockTestScores()
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
    #endregion

}
