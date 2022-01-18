using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighscoreController  
{

    public static void SetHighscore(int score)
    {
        if (PlayerPrefs.GetInt("Highscore", 0) < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    public static int GetHightscore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }
}
