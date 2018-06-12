using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine;

public class NewHighScoreDisplay : UIDisplay {
    public string scoreFieldName = "ScoreField";
    public string initialsFieldName = "InitialsField";
    public VRKeyboard vrKeyboardPrefab;
    private HighScore highScore;
    private Text score;
    private InputField initials;
    private VRKeyboard vrKeyboard;
    private Repository repository;

    void Awake()
    {
        ConvertToVR();
        gameObject.AddComponent<OVRRaycaster>().sortOrder = 20;

        score = GetComponentsInChildren<Text>().Where(textField => textField.name == scoreFieldName).FirstOrDefault();
        initials = GetComponentsInChildren<InputField>().Where(inputField => inputField.name == initialsFieldName).FirstOrDefault();

        if (GameObject.FindObjectOfType<Player>() is OculusRiftPlayer)
            vrKeyboard = Instantiate(vrKeyboardPrefab);

        repository = new Repository();
    }

    public void LoadHighScore(HighScore _highScore)
    {
        highScore = _highScore;

        if (score != null)
            score.text = highScore.Score.ToString();
    }

    public void SaveScore()
    {
        highScore.Name = initials.text;
        highScore.Date = DateTime.Now;

        repository.SaveScore(highScore);

        Destroy(vrKeyboard);
        Destroy(gameObject);
    }

    public void OnInitialsValueChanged()
    {
        initials.text = initials.text.ToUpper();
    }
}
