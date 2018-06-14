using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLeaderboardUI : WorldSpaceUI {
    public Transform targetTransform;
    public HighScoreUI highScoreDisplayPrefab;
    private int rank = 1;

    public void LoadScores(List<HighScore> highScores)
    {
        foreach (HighScore highScore in highScores)
        {
            HighScoreUI highScoreDisplay = (HighScoreUI)Instantiate(highScoreDisplayPrefab);
            highScoreDisplay.transform.SetParent(targetTransform, false);
            highScoreDisplay.LoadHighScore(highScore, rank);
            rank++;
        }
    }

}
