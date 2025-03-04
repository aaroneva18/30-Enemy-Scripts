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
    [SerializeField] private Transform currentEnemyDestination;
    [SerializeField] private EnemyPorpuses currentPorpuse;
    [SerializeField] private bool enemyHasDestination = false;
    [SerializeField] List<Transform> PatrolPosition;
    [SerializeField] List<Transform> HidePosition;


    private Dictionary<Func<bool>, EnemyPorpuses> PorpusesRules;
    private Dictionary<EnemyPorpuses, Transform> PosibleDestinations;
    private NavMeshAgent navMeshAgent;
    private EnemyManager enemyManager;

    private void Awake() {
        SetDefaultState();
    }

    void Start()
    {
        SetEnemyPorpusesRules();
        SetPosibleDesinations();
    }

    void Update()
    {
        SetEnemyPorpuse();
        SetDestination();
        Debug.Log(currentPorpuse);
    }

    public Transform GetDestination() { return currentEnemyDestination; }
    public bool EnemyHasDestination { get { return enemyHasDestination; } }

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
        if (currentEnemyDestination == null) {
            enemyHasDestination = false;
            navMeshAgent.isStopped = true;
            Debug.Log("Destination is null");
            return; 
        }
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(currentEnemyDestination.position);
    }
    private void SetPosibleDesinations() {

       PosibleDestinations = new Dictionary<EnemyPorpuses, Transform>()
        {
            {EnemyPorpuses.Patrol, FindCurrentPatrolPosition() },
            {EnemyPorpuses.Chase, FindPlayerPosition() },
            {EnemyPorpuses.Hide, FindCurrentHidePosition() }
        };

    }

    private Transform FindCurrentPatrolPosition() {
        foreach (var temp_position in PatrolPosition) {
            set
        }
        return null;
    }

    private Transform FindCurrentHidePosition() {
        return null;
    }

    private Transform FindPlayerPosition() {
        currentEnemyDestination = GameObject.FindGameObjectWithTag("Player").transform;
        string destinationisSet = currentEnemyDestination ? "Destination is set" : "Destination is not set";
        Debug.Log(destinationisSet);
        return currentEnemyDestination;

    }

    private void SetEnemyPorpusesRules() {
        PorpusesRules = new Dictionary<Func<bool>, EnemyPorpuses>() 
        {
            {() => !enemyManager.IsEnemyCloseToPlayer && enemyManager.IsCharacterHealthy(), EnemyPorpuses.Patrol},
            {() => enemyManager.IsEnemyCloseToPlayer, EnemyPorpuses.Chase },
            {() => enemyManager.IsCharacterHurt() && distanceToPlayer > 50, EnemyPorpuses.Hide },

        };
    }

    private void SetEnemyPorpuse() {
        foreach (var temp_rule in PorpusesRules) {
            if (temp_rule.Key()) {
                currentPorpuse = temp_rule.Value;
                return;
            }
        }
    }

    private void SetDestination() {
        foreach (var temp_porpuse in PosibleDestinations) {
            if (temp_porpuse.Key == currentPorpuse) {
                currentEnemyDestination = temp_porpuse.Value;
                enemyHasDestination = true; 
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
