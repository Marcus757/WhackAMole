﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {
    public int Rank { get; set; }
    public int Score { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }

    public HighScore(int rank, int score, string name, DateTime date)
    {
        this.Rank = rank;
        this.Score = score;
        this.Name = name;
        this.Date = date;
    }
}