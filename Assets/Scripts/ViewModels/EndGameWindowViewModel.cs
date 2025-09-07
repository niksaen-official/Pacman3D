using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EndGameWindowViewModel:ViewModel
{
    [Inject] GameData gameData;
    [Inject] PlayerData playerData;
    [Inject] IMaxScoreService maxScoreService;

    public Action OnPlayerWin = () => {  };
    public Action<int,int> OnPlayerLose = (score,maxScore) => { };
    public Action DefaultAction = () => { };

    public override void Initialize()
    {
        gameData.State.Subscribe(state =>
        {
            if (state == GameState.Win) 
            {
                OnPlayerWin(); 
            }
            else if (state == GameState.Lose)
            {
                int score = playerData.Score.Value;
                int maxScore = maxScoreService.Score;
                OnPlayerLose(score, maxScore);
                if(score > maxScore)
                {
                    maxScoreService.Score = score;
                }
            }
            else
            {
                DefaultAction();
            }
        }).AddTo(disposables);
    }

    public void RestartGame()
    {
        gameData.State.Value = GameState.Play;
        playerData.Score.Value = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
