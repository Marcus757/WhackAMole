using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {
    public Text rank;
    public Text score;
    public Text name;
    public Text date;
    private HighScore highScore;


    private void Awake()
    {
        Text[] textFields = GetComponentsInChildren<Text>();
        foreach (Text textField in textFields)
        {
            switch (textField.name)
            {
                case "Rank":
                    rank = textField;
                    break;
                case "Score":
                    score = textField;
                    break;
                case "Name":
                    name = textField;
                    break;
                case "Date":
                    date = textField;
                    break;
                default:
                    break;
            }
        }
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
