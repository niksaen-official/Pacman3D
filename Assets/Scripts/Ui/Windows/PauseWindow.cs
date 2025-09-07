using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PauseWindow : Window
{
    [Inject] PauseWindowViewModel viewModel;

    private void Start()
    {
        viewModel.OnGameStateChanged = state => { if (state == GameState.Pause) Show(); else Close(); };
        viewModel.Initialize();
    }

    public void ContinueBtnOnClick()
    {
        viewModel.ContinueGame();
    }

    public void RestartBtnOnClick()
    {
        viewModel.RestartGame();
    }
}
