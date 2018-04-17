using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Score : JSONNode
{
    public override string ToJSON(int prefix)
    {
        throw new NotImplementedException();
    }

    public int GetScore()
    {
        return this["score"].AsInt;
    }

    public string GetInitials()
    {
        return this["initials"].Value;
    }

    public 
}
