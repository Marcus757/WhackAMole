using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {
    private Text rank;
    private Text score;
    private Text name;
    private Text date;
    private HighScore highScore;
    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadHighScore(HighScore _highScore)
    {
        highScore = _highScore;

        if (score != null)
            score.text = _highScore.Score.ToString();

        if (name != null)
            name.text = _highScore.Name.ToString();

        if (date != null)
            date.text = _highScore.Date.ToString("MM/dd/yyyy");
    }
}
