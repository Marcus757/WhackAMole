using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLeaderboard : MonoBehaviour {
    public List<HighScore> highScores = new List<HighScore>();
    public ScoreLeaderboardDisplay scoreLeaderboardDisplayPrefab;

	// Use this for initialization
	void Start () {
        ScoreLeaderboardDisplay scoreLeaderboard = (ScoreLeaderboardDisplay)Instantiate(scoreLeaderboardDisplayPrefab);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
