using UniRx;
using UnityEngine;

public class GameData
{
    public ReactiveProperty<GameState> State = new(GameState.Play);
}
public enum GameState
{
    Play,
    Pause,
    Lose,
    Win
}
