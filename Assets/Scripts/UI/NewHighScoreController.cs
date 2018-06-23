using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScoreController : MonoBehaviour
{
    public NewHighScoreUI newHighScoreUIPrefab;

    public void ShowNewHighScoreUI()
    {
        NewHighScoreUI newHighScoreUI = Instantiate(newHighScoreUIPrefab);
    }
}
