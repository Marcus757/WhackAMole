using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLeaderboard : MonoBehaviour {
    public List<HighScore> highScores = new List<HighScore>();
    public ScoreLeaderboardDisplay scoreLeaderboardDisplayPrefab;
    
	// Use this for initialization
	void Start () {
        ScoreLeaderboardDisplay scoreLeaderboardDisplay = (ScoreLeaderboardDisplay)Instantiate(scoreLeaderboardDisplayPrefab);
        scoreLeaderboardDisplay.LoadScores(highScores);
	}
	
    public void LoadScores(List<HighScore> _highScores)
    {
        highScores = _highScores;
    }
}
