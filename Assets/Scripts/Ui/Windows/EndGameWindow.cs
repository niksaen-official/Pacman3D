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

    [Inject] EndGameWindowViewModel viewModel;

    private void Start()
    {
        viewModel.OnPlayerLose = ShowLose;
        viewModel.OnPlayerWin = ShowWin;
        viewModel.DefaultAction = Close;
        viewModel.Initialize();
    }

    public void ShowWin()
    {
        background.color = Color.green;
        text.text = "You win";
        scoreText.text = "";
        Show();
    }

    public void ShowLose(int score, int maxScore)
    {
        background.color = Color.red;
        text.text = "You lose";
        if (score > maxScore) 
        {
            scoreText.text = $"New record!\nYou score: {score}\nPrevious record: {maxScore}";
        }
        else
        {
            scoreText.text = $"You score: {score}\nYour record: {maxScore}";
        }
        Show();
    }

    public void RestartBtnOnClick()
    {
        viewModel.RestartGame();
    }
}
