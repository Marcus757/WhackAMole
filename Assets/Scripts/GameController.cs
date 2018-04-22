using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject moleContainer;
	public Player player;
	public Text infoText;
    public static int level = 1;
	public float spawnDuration;
	public float spawnDecrement = 0.1f;
	public float minimumSpawnDuration = 0.5f;
	public float gameTimer;

	private Mole[] moles;
	private float spawnTimer;
	private float resetTimer;
    private float countdownTimer;
    public bool isGameInProgress;
    private string scoreFileName;

	// Use this for initialization
	void Start () {
		moles = moleContainer.GetComponentsInChildren<Mole> ();
        infoText.text = "Grab the hammer and get ready!";
        isGameInProgress = false;
        countdownTimer = 10f;
        resetTimer = 5f;
        scoreFileName = "scores.txt";
        LoadScores();
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One))
            ResetGame();

        //if (!isGameInProgress && IsHammerGrabbed())
        //    isGameInProgress = true;

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
                moles[Random.Range(0, moles.Length)].Rise();
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
                //Score score = (Score) JSONNode.LoadFromFile(scoreFileName);
                LoadScores();
                SaveScore(Player.totalScore, "NWA", System.DateTime.Now.Date); ;
            }
            
            ChangeLevel();
            ResetGame();
        }
    }

    public void ResetGame()
    {
        OVRInput.RecenterController();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (level == 1)
            Player.totalScore = 0;
    }

    private bool IsHammerGrabbed()
    {
        OVRGrabber[] grabbers = GameObject.FindObjectsOfType<OVRGrabber>();
        foreach (var grabber in grabbers)
        {
            if (grabber.grabbedObject != null && grabber.grabbedObject.GetComponent<Hammer>() != null)
                return true;
        }

        return false;
    }

    private void ChangeLevel()
    {
        if (level == 3)
            level = 1;
        else
            level++;
    }

    private void SaveScore(int totalScore, string initials, System.DateTime date)
    {
        JSONClass score = new JSONClass();
        score.Add("score", new JSONData(score));
        score.Add("initials", new JSONData(initials));
        score.Add("date", new JSONData(date.ToString()));
        score.SaveToFile(scoreFileName);
    }

    private void LoadScores()
    {
        ScoreLeaderboard scoreLeaderboard = new ScoreLeaderboard();
        scoreLeaderboard.highScores = new List<HighScore>();
        scoreLeaderboard.highScores.Add(new HighScore { Score = 134, Name = "MSN", Date = System.DateTime.Now.AddDays(320) });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 400, Name = "CCM", Date = System.DateTime.Now.AddDays(720) });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 45, Name = "MNO", Date = System.DateTime.Now.AddDays(-720) });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 100, Name = "JKL", Date = System.DateTime.Now.AddDays(-400) });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 200, Name = "MSN", Date = System.DateTime.Now.AddDays(365) });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 50, Name = "GHI", Date = System.DateTime.Now.AddDays(-100) });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 75, Name = "TAM", Date = System.DateTime.Now.AddDays(100) });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 30, Name = "DEF", Date = System.DateTime.Now });
        scoreLeaderboard.highScores.Add(new HighScore { Score = 225, Name = "ABC", Date = System.DateTime.Now });

        foreach (var highScore in scoreLeaderboard.highScores)
        {
            HighScoreDisplay highScoreDisplay = GameObject.FindObjectOfType<HighScoreDisplay>();
            highScoreDisplay.LoadHighScore(highScore);
        }
    }
}
