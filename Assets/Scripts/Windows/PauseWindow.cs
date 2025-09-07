using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PauseWindow : Window
{
    [Inject] GameData gameData;
    [Inject] PlayerData playerData;

    private readonly CompositeDisposable disposables = new ();

    private void Start()
    {
        gameData.State.Subscribe(v =>
        {
            if (v == GameState.Pause) Show(); else Close();
        }).AddTo(disposables);

    }

    public void ContinueBtnOnClick()
    {
        gameData.State.Value = GameState.Play;
    }

    public void RestartBtnOnClick()
    {
        gameData.State.Value = GameState.Play;
        playerData.Restart();
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
