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
        AddTransition(iddleState, walkState, () => enemyMovement.GetDestination());
        AddTransition(walkState, iddleState, () => enemyMovement.GetDestination() && !enemyMovement.CheckIsMoving());
        SetState(iddleState);
    }

}
