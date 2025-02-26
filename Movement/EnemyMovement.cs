using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyPorpuses {
    Patrol,
    Chase,
    Hide
}


public class EnemyMovement : Movement {

    [SerializeField] private float distanceToPlayer = 0;
    [SerializeField] private Transform destination;
    [SerializeField] private EnemyPorpuses currentPorpuse;
    

    private Dictionary<Func<bool>, EnemyPorpuses> PorpusesRules;
    private NavMeshAgent navMeshAgent;
    private EnemyManager enemyManager;

    private void Awake() {
        SetDefaultState();
    }

    void Start()
    {
        SetEnemyPorpusesRules();
    }

    void Update()
    {
        SetEnemyPorpuse();
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
    public void SetDesination() {

        //hacer un diccionario de propositos y de destinos.
        
    }

    private Transform SearchForPlayerPosition() {
        destination = GameObject.FindGameObjectWithTag("Player").transform;
        string destinationisSet = destination ? "Destination is set" : "Destination is not set";
        Debug.Log(destinationisSet);
        return destination;

    }

    private void SetEnemyPorpusesRules() {
        PorpusesRules = new Dictionary<Func<bool>, EnemyPorpuses>() 
        {
            {() => enemyManager.GetIsEnemyCloseToPlayer, EnemyPorpuses.Chase },
            {() => distanceToPlayer > 10, EnemyPorpuses.Hide },
            {() => true, EnemyPorpuses.Patrol} //default state

        };
    }

    private void SetEnemyPorpuse() {
        foreach (var rule in PorpusesRules) {
            if (rule.Key()) {
                currentPorpuse = rule.Value;
                return;
            }
        }
    }

    public override void SetDefaultState() {
        try {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyManager = GetComponent<EnemyManager>();
        } catch (System.Exception e) {
            Debug.LogError(e);
        }
    }

}
