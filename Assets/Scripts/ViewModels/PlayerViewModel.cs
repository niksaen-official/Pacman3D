using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerViewModel : MonoBehaviour
{
    [SerializeField]
    FixedJoystick joystick;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [Inject] private PlayerMovement playerMovement;
    [Inject] private PlayerData playerData;
    [Inject] private GameData gameData;
    [Inject] private PlayerCollector playerCollector;

    private readonly CompositeDisposable disposables = new CompositeDisposable();

    private void Start()
    {
        playerCollector.OnCollectedCoin
            .Subscribe(_ => playerData.Score.Value++)
            .AddTo(disposables);

        playerData.Score
            .Subscribe(score => scoreText.text = $"Score: {score}")
            .AddTo(disposables);

        gameData.State.Subscribe(state =>
        {
            if (state != GameState.Play) playerMovement.Stop();
        }).AddTo(disposables);
    }

    private void FixedUpdate()
    {
        playerMovement.HorizontalInput = joystick.Horizontal;
        playerMovement.VerticalInput = joystick.Vertical;
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
