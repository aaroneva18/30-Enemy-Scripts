using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        throw new System.NotImplementedException();
    }


    public void DetectPlayerByCollisionRadious() {
        
    }


    public override void SetDefaultState() {
        try {
            navMeshAgent = GetComponent<NavMeshAgent>();
        } catch (System.Exception e) {
            Debug.LogError(e);
        }
    }
}
