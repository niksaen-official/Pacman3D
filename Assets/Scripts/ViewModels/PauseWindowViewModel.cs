using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PauseWindowViewModel:ViewModel
{
    [Inject] GameData gameData;
    [Inject] PlayerData playerData;

    public Action<GameState> OnGameStateChanged = (gameState) => {  };

    public override void Initialize()
    {
        gameData.State.Subscribe(OnGameStateChanged).AddTo(disposables);
    }

    public void ContinueGame()
    {
        gameData.State.Value = GameState.Play;
    }

    public void RestartGame()
    {
        gameData.State.Value = GameState.Play;
        playerData.Score.Value = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
