using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    private NewHighScoreController newHighScoreController;
    private ScoreLeaderboardController scoreLeaderboardController;

	// Use this for initialization
	void Start () 
	{
        newHighScoreController = GetComponent<NewHighScoreController>();
        scoreLeaderboardController = GetComponent<ScoreLeaderboardController>();
    }
	
	public void ShowNewHighScoreUI()
    {
        newHighScoreController.ShowNewHighScoreUI();
    }

    public void ShowScoreLeaderboard()
    {
        scoreLeaderboardController.ShowScoreLeaderboardUI();
    }
}
