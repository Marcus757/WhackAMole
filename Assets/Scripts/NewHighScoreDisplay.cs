using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class NewHighScoreDisplay : MonoBehaviour {
    public static bool areInitialsEntered = false;
    public Text score;
    public Text initials;

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
                case "InitialsField":
                    initials = textField;
                    break;
                default:
                    break;
            }
        }
    }

    // Use this for initialization
    void Start () {
        GameController gameController = GameObject.FindObjectOfType<GameController>();
        Player player = gameController.player;

        if (player is NonVRPlayer)
            GetComponent<Canvas>().worldCamera = player.GetComponentInChildren<Camera>();
    }

    public void LoadScore(int _score)
    {
        if (score != null)
            score.text = _score.ToString();
    }
	
	// Update is called once per frame

    public void SetAreInitialsEntered()
    {
        if (!string.IsNullOrEmpty(GetComponentInChildren<InputField>().text))
            areInitialsEntered = true;
    }

    //public string GetInitials()
    //{
    //    return GetComponentInChildren<InputField>().text;
    //}

}
