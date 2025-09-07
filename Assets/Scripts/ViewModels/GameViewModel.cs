using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameViewModel : MonoBehaviour
{
    [SerializeField] GameObject Controls;
    public int NeedScoreForWin = 63;

    [Inject] GameData gameData;
    [Inject] PlayerData playerData;

    private readonly CompositeDisposable disposables = new ();

    private void Start()
    {
        gameData.State.Subscribe(v => 
        {
            if(v == GameState.Play) Controls.SetActive(true); else Controls.SetActive(false);
        }).AddTo(disposables);

        playerData.Score.Subscribe(v =>
        {
            if(v >= NeedScoreForWin)
            {
                gameData.State.Value = GameState.Win;
            }
        }).AddTo(disposables);
    }

    public void PauseBtnOnClick()
    {
        gameData.State.Value = GameState.Pause;
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
