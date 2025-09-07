using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AmbushEnemy : Enemy
{
    [Header("Ambush enemy settings")]
    [SerializeField] private List<Transform> ambushPoints = new List<Transform>();
    [SerializeField] private float ambushRange = 5f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float ambushCooldown = 5f;

    private bool isInAmbush = false;
    private bool canAmbush = true;
    private int currentPointIndex = 0;

    protected override void Start()
    {
        base.Start();
        MoveToAmbushPoint();
    }

    private void FixedUpdate()
    {
        if (isInAmbush)
        {
            CheckForPlayer();
        }
        else
        {
            PatrolBetweenSpots();
        }
    }

    private void MoveToAmbushPoint()
    {
        if (ambushPoints.Count == 0) return;

        currentPointIndex = Random.Range(0, ambushPoints.Count);
        agent.SetDestination(ambushPoints[currentPointIndex].position);
        isInAmbush = false;
    }

    private void PatrolBetweenSpots()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isInAmbush = true;
            agent.isStopped = true;
        }
    }

    private void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPositionService.CurrentPosition);

        if (distanceToPlayer <= detectionRange && canAmbush)
        {
            StartAmbush();
        }
    }

    private void StartAmbush()
    {
        canAmbush = false;
        agent.isStopped = false;
        agent.SetDestination(playerPositionService.CurrentPosition);

        Observable.Timer(System.TimeSpan.FromSeconds(ambushCooldown))
            .Subscribe(_ =>
            {
                canAmbush = true;
                ReturnToAmbush();
            })
            .AddTo(disposables);
    }

    private void ReturnToAmbush()
    {
        MoveToAmbushPoint();
    }
}
