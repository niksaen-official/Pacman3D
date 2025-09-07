using System;
using UniRx;
using UnityEngine;
using Zenject;

public class ControlsViewModel:ViewModel
{
    public int NeedScoreForWin = 63;

    [Inject] private PlayerMovement playerMovement;
    [Inject] private PlayerData playerData;
    [Inject] private GameData gameData;
    [Inject] private PlayerCollector playerCollector;

    public Action<GameState> OnGameStateChanged = (state) => { }; 
    public Action<int> OnScoreChanged = (score) => { };

    public override void Initialize()
    {
        playerCollector.OnCollectedCoin
            .Subscribe(_ => playerData.Score.Value++)
            .AddTo(disposables);

        gameData.State.Subscribe(OnGameStateChanged).AddTo(disposables);
        
        playerData.Score
            .Subscribe(score =>
            {
                if (score >= NeedScoreForWin)
                {
                    gameData.State.Value = GameState.Win;
                }
                OnScoreChanged(score);
            })
            .AddTo(disposables);
    }

    public void MovePlayer(float horizontal, float vertical)
    {
        playerMovement.HorizontalInput = horizontal;
        playerMovement.VerticalInput = vertical;
    }

    public void PauseGame()
    {
        gameData.State.Value = GameState.Pause;
    }
}
