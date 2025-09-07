using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public ReactiveProperty<int> Score { get; } = new(0);
}
