using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine;

public class NewHighScoreUI : WorldSpaceUI {
    public static UnityEngine.Object prefab;
    public string scoreFieldName = "ScoreField";
    public string initialsFieldName = "InitialsField";
    public VRKeyboard vrKeyboardPrefab;
    private HighScore highScore;
    private Text score;
    private InputField initials;
    private VRKeyboard vrKeyboard;
    private Repository repository;

    void Start()
    {
        ConvertToVR();
        gameObject.AddComponent<OVRRaycaster>().sortOrder = 20;

        score = GetComponentsInChildren<Text>().Where(textField => textField.name == scoreFieldName).FirstOrDefault();
        initials = GetComponentsInChildren<InputField>().Where(inputField => inputField.name == initialsFieldName).FirstOrDefault();

        if (GameObject.FindObjectOfType<Player>() is OculusRiftPlayer)
            vrKeyboard = Instantiate(vrKeyboardPrefab);

        repository = new Repository();
    }
    
    public static NewHighScoreUI Create(HighScore _highScore)
    {
        prefab = Resources.Load("Prefabs/NewHighScoreUI");
        GameObject newObject = Instantiate(prefab) as GameObject;
        NewHighScoreUI newHighScoreUI = newObject.GetComponent<NewHighScoreUI>();
        newHighScoreUI.LoadHighScore(_highScore);
        return newHighScoreUI;
    }

    private void LoadHighScore(HighScore _highScore)
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
