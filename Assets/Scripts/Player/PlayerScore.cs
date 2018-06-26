using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerScore : ScriptableObject
{
    public int score;

    public void ResetScore()
    {
        score = 0;
    }
}
