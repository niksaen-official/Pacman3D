using UnityEngine;
using UnityEngine.AI;
using Zenject;
using UniRx;
using System;

public class ChasePlayerEnemy : Enemy
{
    private void FixedUpdate()
    {
        agent.SetDestination(playerPositionService.CurrentPosition);
    }
}
