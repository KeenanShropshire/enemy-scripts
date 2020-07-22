using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaypointMove : MonoBehaviour
{
    //whether object waits or not
    [SerializeField]
    bool _patrolWaiting;

    //time to wait at waypoint
    float _totalWaitTime = 3f;

    //waypoint switch probability
    float _switchProbability = 0.2f;

    //list all waupoints to visit
    [SerializeField]
    List<Waypoint> _points;

    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _traveling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(_navMeshAgent == null){
            Debug.LogError("the nav mesh agent component is not attatched to " + gameObject.name);
        }
        else{
            if(_points != null && _points.Count >= 2){
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else{
                Debug.LogError("not enough waypoints to move between");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_traveling && _navMeshAgent.remainingDistance <= 1.0f){
            _traveling = false;

            //if going to wait
            if(_patrolWaiting){
                _waiting = true;
                _waitTimer = 0f;
            }
            else{
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //if waiting
        if(_waiting){
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime) {
                _waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }

    }

    private void SetDestination(){
        if(_points != null){
            Vector3 target = _points[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(target);
            _traveling = true;
        }
    }


    public void ResetDestination()
    {
        if (_points != null)
        {
            Vector3 target = _points[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(target);
            _traveling = true;
        }
    }


    //select new waypoint from one of the available positions in the list
    //possibility of direction changes
    private void ChangePatrolPoint(){
        if (UnityEngine.Random.Range(0f,1f) <= _switchProbability){
            _patrolForward = !_patrolForward;
        }

        if(_patrolForward){
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _points.Count;
        }
        else{
            if(--_currentPatrolIndex < 0){
                _currentPatrolIndex = _points.Count - 1;
            }
        }
    }
}
