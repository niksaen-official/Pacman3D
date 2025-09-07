using UnityEngine;

public class PrefsMaxScoreService : IMaxScoreService
{
    private int maxScore;
    private bool isLoaded = false;

    public int Score
    {
        get 
        {
            if(!isLoaded) LoadScore();
            return maxScore;
        }
        set
        {
            maxScore = value;
            SaveScore();
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("maxScore", maxScore);
    }

    private void LoadScore()
    {
        maxScore = PlayerPrefs.GetInt("maxScore", 0);
        isLoaded = true;
    }

}
