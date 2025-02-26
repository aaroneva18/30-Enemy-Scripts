using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyPorpuses {
    Patrol,
    Chase,
    Attack,
    Hide
}

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

    public Transform GetDestination() { return destination; }

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
            navMeshAgent.SetDestination(SearchForPlayerPosition().position);
            return; 
        }
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination.position);
    }

    private Transform SearchForPlayerPosition() {
        destination = GameObject.FindGameObjectWithTag("Player").transform;
        string destinationisSet = destination ? "Destination is set" : "Destination is not set";
        Debug.Log(destinationisSet);
        return destination;

    }

    public void SetDesination() {
        destination = null;
        
    }

    public override void SetDefaultState() {
        try {
            navMeshAgent = GetComponent<NavMeshAgent>();
        } catch (System.Exception e) {
            Debug.LogError(e);
        }
    }

}
