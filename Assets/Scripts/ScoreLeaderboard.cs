using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLeaderboard : MonoBehaviour {
    public List<HighScore> highScores = new List<HighScore>();
    public ScoreLeaderboardUI scoreLeaderboardDisplayPrefab;
    
	// Use this for initialization
	void Start () {
        ScoreLeaderboardUI scoreLeaderboardUI = (ScoreLeaderboardUI)Instantiate(scoreLeaderboardDisplayPrefab);
        scoreLeaderboardUI.LoadScores(highScores);
	}
	
    public void LoadScores(List<HighScore> _highScores)
    {
        highScores = _highScores;
    }
}
