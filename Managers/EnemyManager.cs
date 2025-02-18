using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    [SerializeField] private bool isEnemyCloseToPlayer;
    [SerializeField] private bool isChasingPlayer;  
    [SerializeField] private float radiusDetection;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private Transform initialEnemyPosition;

    private EnemyMovement enemyMovement;

    private void Awake() {
        SetDefaultState();
    }

    void Start()
    {
        KnowWherePlayerIs();
    }

    void Update()
    {
        DetectPlayerByCollisionRadious();
        DistanceToPlayer();
    }

    void FixedUpdate() {
        //implement states
        if (isEnemyCloseToPlayer) {
            if (isChasingPlayer) { 
                enemyMovement.Move();
                return; 
            }
            enemyMovement.SetDestination(player.transform);
            isChasingPlayer = true;
        }

        if (DistanceToPlayer() > radiusDetection) { 
            enemyMovement.TeleportTo(initialEnemyPosition);
            enemyMovement.SetDestination(null);
            isChasingPlayer = false;
        }
    }

    public bool GetIsEnemyCloseToPlayer { get { return isEnemyCloseToPlayer; } }

    public override void Dead() {
        throw new System.NotImplementedException();
    }

    public override void Respawn() {
        throw new System.NotImplementedException();
    }


    public override void Spawn() {
        throw new System.NotImplementedException();
    }

    public void DetectPlayerByCollisionRadious() {
        isEnemyCloseToPlayer = Physics.CheckSphere(transform.position, radiusDetection, playerLayerMask);
    }

    public void KnowWherePlayerIs() {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.DrawRay(transform.position, player.transform.position, Color.red);
    }

    public float DistanceToPlayer() {
        return distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    public override void SetDefaultState() {
        try {
            enemyMovement = GetComponent<EnemyMovement>();
        } catch (System.Exception e) {
            Debug.LogError(e);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusDetection);
    }
}
