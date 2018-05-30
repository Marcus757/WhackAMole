using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLeaderboardDisplay : MonoBehaviour {
    public Transform targetTransform;
    public HighScoreDisplay highScoreDisplayPrefab;
    private int rank = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScores(List<HighScore> highScores)
    {
        foreach (HighScore highScore in highScores)
        {
            HighScoreDisplay highScoreDisplay = (HighScoreDisplay)Instantiate(highScoreDisplayPrefab);
            highScoreDisplay.transform.SetParent(targetTransform, false);
            highScoreDisplay.LoadHighScore(highScore, rank);
            rank++;
        }
    }

}
