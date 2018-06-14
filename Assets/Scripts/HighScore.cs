using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {
    public int Rank { get; set; }
    public int Score { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    //public NewHighScoreUI newHighScoreDisplayPrefab;

        
    public HighScore(int rank, int score, string name, DateTime date)
    {
        this.Rank = rank;
        this.Score = score;
        this.Name = name;
        this.Date = date;
    }

    public void LoadScore(int score)
    {
        this.Score = score;
    }

    //public void ShowNewHighScoreUI()
    //{
    //    NewHighScoreUI newHighScoreDisplay = (NewHighScoreUI)Instantiate(newHighScoreDisplayPrefab);
    //    newHighScoreDisplay.LoadHighScore(this);
    //}
}
