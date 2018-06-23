using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLeaderboardController : MonoBehaviour
{
    public ScoreLeaderboardUI scoreLeaderboardUIPrefab;

    public void ShowScoreLeaderboardUI()
    {
        ScoreLeaderboardUI scoreLeaderboardUI = (ScoreLeaderboardUI)Instantiate(scoreLeaderboardUIPrefab);
        scoreLeaderboardUI.ShowUI();
    }
}
