using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour {
    public string rankFieldName = "Rank";
    public string scoreFieldName = "Score";
    public string nameFieldName = "Name";
    public string dateFieldName = "Date";
    public Text rank;
    public Text score;
    public Text name;
    public Text date;
    private HighScore highScore;
    
    void Awake()
    {
        rank = GetComponentsInChildren<Text>().Where(textField => textField.name == rankFieldName).FirstOrDefault();
        score = GetComponentsInChildren<Text>().Where(textField => textField.name == scoreFieldName).FirstOrDefault();
        name = GetComponentsInChildren<Text>().Where(textField => textField.name == nameFieldName).FirstOrDefault();
        date = GetComponentsInChildren<Text>().Where(textField => textField.name == dateFieldName).FirstOrDefault();
    }

    public void LoadHighScore(HighScore _highScore, int rankCount)
    {
        highScore = _highScore;

        if (score != null)
            score.text = highScore.Score.ToString();

        if (name != null)
            name.text = highScore.Name.ToString();

        if (date != null)
            date.text = highScore.Date.ToString("MM/dd/yyyy");

        if (rank != null)
            rank.text = rankCount.ToString();
    }
}
