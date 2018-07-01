using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    private NewHighScoreController newHighScoreController;
    private ScoreLeaderboardController scoreLeaderboardController;
    private GameObject keyboard;

	// Use this for initialization
	void Awake () 
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
