using NUnit.Framework;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PatrollingEnemy : Enemy
{
    [Header("Patrolling enemy settings")]
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float waitTimeAtPoint = 2f;
    [SerializeField] private float playerDetectionRange = 10f;

    private int currentPointIndex = 0;
    private bool isWaiting = false;

    protected override void Start()
    {
        base.Start();
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (patrolPoints.Count == 0) return;

        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    private void FixedUpdate()
    {
        if (IsPlayerInDetectionRange())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private bool IsPlayerInDetectionRange()
    {
        return Vector3.Distance(transform.position, playerPositionService.CurrentPosition) <= playerDetectionRange;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerPositionService.CurrentPosition);
    }

    private void Patrol()
    {
        if (!isWaiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                StartWaitTimer();
            }
        }
    }

    private void StartWaitTimer()
    {
        isWaiting = true;

        Observable.Timer(System.TimeSpan.FromSeconds(waitTimeAtPoint))
            .Subscribe(_ =>
            {
                isWaiting = false;
                MoveToNextPoint();
            })
            .AddTo(disposables);
    }
}
