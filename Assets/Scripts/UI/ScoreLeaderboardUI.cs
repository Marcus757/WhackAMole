using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLeaderboardUI : WorldSpaceUI
{
    public Transform targetTransform;
    public HighScoreUI highScoreDisplayPrefab;
    public HighScoresData highScoresData;
    private int rank = 1;

    public void ShowUI()
    {
        foreach (HighScore highScore in highScoresData.highScores)
        {
            HighScoreUI highScoreDisplay = (HighScoreUI)Instantiate(highScoreDisplayPrefab);
            highScoreDisplay.transform.SetParent(targetTransform, false);
            highScoreDisplay.LoadHighScore(highScore, rank);
            rank++;
        }
    }

}
