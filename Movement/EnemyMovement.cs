using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement {

    [SerializeField] private float distanceToPlayer = 0;
    [SerializeField] private Transform destination;

    private NavMeshAgent navMeshAgent;

    private void Awake() {
        SetDefaultState();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override float CalculateCurrentSpeed() {
        throw new System.NotImplementedException();
    }

    public override bool CheckIsMoving() {
        throw new System.NotImplementedException();
    }

    public override void Jump() {
        throw new System.NotImplementedException();
    }

    public override void Move() {
        if (destination == null) { 
            navMeshAgent.isStopped = true;
            return; 
        }
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination.position);
    }

    public void SetDestination(Transform desiredDestination) {
        destination = desiredDestination;
        Move();
    }


    public override void SetDefaultState() {
        try {
            navMeshAgent = GetComponent<NavMeshAgent>();
        } catch (System.Exception e) {
            Debug.LogError(e);
        }
    }

}
