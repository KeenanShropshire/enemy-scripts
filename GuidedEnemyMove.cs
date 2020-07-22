using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuidedEnemyMove : MonoBehaviour
{
    [SerializeField]
    Transform _destination;
    NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(_navMeshAgent == null){
            Debug.LogError("the nav mesh agent component is not attatched to " + gameObject.name);
        }
        else{
            setDestination();
        }
    }

    private void setDestination()
    {
        if(_destination != null){
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
            
        }
    }

}
