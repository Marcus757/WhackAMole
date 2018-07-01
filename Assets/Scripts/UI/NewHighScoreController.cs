using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScoreController : MonoBehaviour
{
    public NewHighScoreUI newHighScoreUI;
    public GameObject keyboard;

    public void ShowNewHighScoreUI()
    {
        //NewHighScoreUI newHighScoreUI = Instantiate(newHighScoreUIPrefab);
        newHighScoreUI.gameObject.SetActive(true);
        keyboard.SetActive(true);
    }
}
