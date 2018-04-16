using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Score : JSONClass  {
    public Score(int _score, string _initials, DateTime _date)
    {
        Add("score", new JSONData(_score));
        Add("initials", new JSONData(_initials));
        Add("date", new JSONData(_date.ToString()));
    }
}
