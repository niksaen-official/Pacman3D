using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class Enemy: MonoBehaviour
{
    [Inject] private GameData gameData;
    [Inject] protected IPositionService playerPositionService;

    protected readonly CompositeDisposable disposables = new();
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        gameData.State.Subscribe(state =>
        {
            if (state != GameState.Play) agent.isStopped = true;
            else agent.isStopped = false;
        }).AddTo(disposables);
    }

    protected void AttackPlayer()
    {
        gameData.State.Value = GameState.Lose;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
