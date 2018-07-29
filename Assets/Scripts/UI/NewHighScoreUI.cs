using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine;

public class NewHighScoreUI : WorldSpaceUI
{
    public string scoreFieldName = "ScoreField";
    public string initialsFieldName = "InitialsField";
    public PlayerScore playerScore;

    private HighScore highScore;
    private Text score;
    private InputField initials;
    private Repository repository;

    void Awake()
    {
        score = GetComponentsInChildren<Text>().Where(textField => textField.name == scoreFieldName).FirstOrDefault();
        initials = GetComponentsInChildren<InputField>().Where(inputField => inputField.name == initialsFieldName).FirstOrDefault();
        repository = new Repository();
        LoadHighScore(playerScore.score);

        if (GameObject.FindObjectOfType<Player>() is OculusRiftPlayer)
        {
            ConvertToVR();
            gameObject.AddComponent<OVRRaycaster>().sortOrder = 20;
        }
    }

    private void LoadHighScore(int highScore)
    {
        if (score != null)
            score.text = highScore.ToString();
    }

    public void SaveScore()
    {
        HighScore highScore = new HighScore(playerScore.score, initials.text, DateTime.Today);
        repository.SaveScore(highScore);

        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Keyboard").SetActive(false);
    }

    public void OnInitialsValueChanged()
    {
        initials.text = initials.text.ToUpper();
    }
}