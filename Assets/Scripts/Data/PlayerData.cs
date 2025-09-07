using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public ReactiveProperty<int> Score { get; } = new(0);

    public void Restart()
    {
        Score.Value = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
