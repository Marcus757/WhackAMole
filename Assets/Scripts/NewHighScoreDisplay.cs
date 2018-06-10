using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using SQLiter;
using System.Linq;
using System;

public class NewHighScoreDisplay : UIDisplay {
    public VRKeyboard vrKeyboardPrefab;
    private HighScore highScore;
    private Text score;
    private InputField initials;
    private VRKeyboard vrKeyboard;

    private void Awake()
    {
        Text[] textFields = GetComponentsInChildren<Text>();
        foreach (Text textField in textFields)
        {
            switch (textField.name)
            {
                case "ScoreField":
                    score = textField;
                    break;
                default:
                    break;
            }
        }

        InputField[] inputFields = GetComponentsInChildren<InputField>();
        foreach (InputField inputField in inputFields)
        {
            switch (inputField.name)
            {
                case "InitialsField":
                    initials = inputField;
                    break;
                default:
                    break;
            }
        }

        vrKeyboard = Instantiate(vrKeyboardPrefab);
    }

    public override void Start()
    {
        base.Start();
        gameObject.AddComponent<OVRRaycaster>().sortOrder = 20;
    }

    public void LoadHighScore(HighScore _highScore)
    {
        highScore = _highScore;

        if (score != null)
            score.text = highScore.Score.ToString();
    }
	
	// Update is called once per frame

    public void SaveScore()
    {
        if (!string.IsNullOrEmpty(initials.text))
            GameController.areInitialsEntered = true;

        highScore.Name = initials.text;
        highScore.Date = DateTime.Now;

        List<HighScore> highScores = GameController.sqLite.GetAllHighScores();
        highScores.Add(highScore);
        highScores = highScores.OrderByDescending(score => score.Score).Take(10).ToList();

        foreach (HighScore _highScore in highScores)
        {
            int rank = highScores.FindIndex(hs => hs.Name == _highScore.Name && hs.Score == _highScore.Score && hs.Date == _highScore.Date);
            _highScore.Rank = ++rank;
        }
        
        GameController.sqLite.DeleteAllScores();
        GameController.sqLite.SaveScores(highScores);
        Destroy(vrKeyboard);
        Destroy(gameObject);
    }

    public void OnInitialsValueChanged()
    {
        initials.text = initials.text.ToUpper();
    }
}
