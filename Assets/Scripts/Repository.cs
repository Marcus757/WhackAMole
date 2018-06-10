using SQLiter;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Repository : MonoBehaviour {
    private SQLite sqLite;

    public Repository()
    {
        sqLite = GameObject.FindObjectOfType<SQLite>();
    }

    public List<HighScore> GetAllHighScores()
    {
        return sqLite.GetAllHighScores();
    }

    public void SaveScore(HighScore highScore)
    {
        List<HighScore> highScores = GetAllHighScores();
        highScores.Add(highScore);
        highScores = highScores.OrderByDescending(score => score.Score).Take(10).ToList();

        foreach (HighScore _highScore in highScores)
        {
            int rank = highScores.FindIndex(hs => hs.Name == _highScore.Name && hs.Score == _highScore.Score && hs.Date == _highScore.Date);
            _highScore.Rank = ++rank;
        }

        DeleteAllScores();
        sqLite.SaveScores(highScores);
    }

    public void DeleteAllScores()
    {
        sqLite.DeleteAllScores();
    }
}
