using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerScore : ScriptableObject
{
    public int levelScore;
    public int totalScore;

    public void ResetScore()
    {
        levelScore = 0;
        totalScore = 0;
    }
}
