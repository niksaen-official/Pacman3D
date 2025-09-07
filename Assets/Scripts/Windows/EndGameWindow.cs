using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class EndGameWindow : Window
{
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI scoreText;

    [Inject] GameData gameData;
    [Inject] PlayerData playerData;
    [Inject] IMaxScoreService maxScoreService;

    private readonly CompositeDisposable disposables = new();

    private void Start()
    {
        gameData.State.Subscribe(v =>
        {
            switch (v)
            {
                case GameState.Lose: ShowLose(playerData.Score.Value); break;
                case GameState.Win: ShowWin(); break;
                default: Close(); break;
            }
        }).AddTo(disposables);

    }

    public void ShowWin()
    {
        background.color = Color.green;
        text.text = "You win";
        scoreText.text = "";
        Show();
    }

    public void ShowLose(int score)
    {
        background.color = Color.red;
        text.text = "You lose";
        int maxScore = maxScoreService.Score;
        if (score > maxScore) 
        {
            scoreText.text = $"New record!\nYou score: {score}\nPrevious record: {maxScore}";
            maxScoreService.Score = score;
        }
        else
        {
            scoreText.text = $"You score: {score}\nYour record: {maxScore}";
        }
        Show();
    }

    public void RestartBtnOnClick()
    {
        gameData.State.Value = GameState.Play;
        playerData.Restart();
    }
}
