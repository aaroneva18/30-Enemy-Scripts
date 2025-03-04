using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    private EnemyMovement enemyMovement;

    private void Awake() {
        enemyMovement = GetComponent<EnemyMovement>();
    }
    private void Start() {
        WalkState walkState = new WalkState(enemyMovement);
        IddleState iddleState = new IddleState(enemyMovement);
        AddTransition(iddleState, walkState, () => enemyMovement.EnemyHasDestination);
        AddTransition(walkState, iddleState, () => !enemyMovement.EnemyHasDestination && !enemyMovement.CheckIsMoving());
        SetState(iddleState);
    }

}
