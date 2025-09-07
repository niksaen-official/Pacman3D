using TMPro;
using UnityEngine;
using Zenject;

public class ControlView : MonoBehaviour
{
    [SerializeField] FixedJoystick joystick;
    [SerializeField] TextMeshProUGUI scoreText;

    [Inject] ControlsViewModel viewModel;

    private void Start()
    {
        viewModel.OnScoreChanged = score => { scoreText.text = "Score: "+score; };
        viewModel.OnGameStateChanged = state => gameObject.SetActive(state == GameState.Play);
        viewModel.Initialize();
    }

    private void FixedUpdate()
    {
        viewModel.MovePlayer(joystick.Horizontal, joystick.Vertical);
    }

    public void PauseButtonOnClick()
    {
        viewModel.PauseGame();
    }
}
