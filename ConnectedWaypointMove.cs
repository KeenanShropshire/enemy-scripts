using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class ConnectedWaypointMove : MonoBehaviour
{

    //whether object waits or not
    [SerializeField]
    bool _patrolWaiting;

    //time to wait at waypoint
    float _totalWaitTime = 3f;

    //waypoint switch probability
    float _switchProbability = 0.2f;

    NavMeshAgent _navMeshAgent;
    ConnectedWaypoint currentWaypoint;
    ConnectedWaypoint previousWaypoint;
    static Animator anim;

    bool _traveling;
    public bool _waiting;
    float _waitTimer;
    int waypointsVisited;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        //_navMeshAgent.updateRotation = false;

        if (_navMeshAgent == null)
        {
            Debug.LogError("the nav mesh agent component is not attatched to " + gameObject.name);
        }
        else
        {
            if (currentWaypoint == null)
            {
                //set to random and get other waypoints in scene
                GameObject[] allwaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allwaypoints.Length > 0)
                {
                    while (currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allwaypoints.Length);
                        ConnectedWaypoint startingPoint = allwaypoints[random].GetComponent<ConnectedWaypoint>();

                        //found waypoint
                        if (startingPoint != null)
                        {
                            currentWaypoint = startingPoint;
                        }
                    }
                }
                else
                {
                    Debug.LogError("cannot find waypoints to use in this scene");
                }
            }

            SetDestination();
        }
    }
       
    // Update is called once per frame
        void Update()
        {

        if (_traveling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _traveling = false;
            waypointsVisited++;

            //if going to wait
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                SetDestination();
            }
        }

        //if waiting
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;
                SetDestination();
            }
        }



    }

    private void SetDestination(){
            if(waypointsVisited > 0){
                ConnectedWaypoint nextWaypoint = currentWaypoint.nextWaypoint(previousWaypoint);
                previousWaypoint = currentWaypoint;
                currentWaypoint = nextWaypoint;
        }

        Vector3 target = currentWaypoint.transform.position;
        _navMeshAgent.SetDestination(target);
        _traveling = true;

        if(currentWaypoint.name == "PlayerPoint"){
            _navMeshAgent.isStopped = true;
            gameObject.GetComponent<EnemyMove>();
            Debug.Log("hello");
        }
    }

    public void ResetDestination()
    {
        if (waypointsVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = currentWaypoint.nextWaypoint(previousWaypoint);
            previousWaypoint = currentWaypoint;
            currentWaypoint = nextWaypoint;
        }

        Vector3 target = currentWaypoint.transform.position;
        _navMeshAgent.SetDestination(target);
        _traveling = true;

        if (currentWaypoint.name == "PlayerPoint")
        {
            _navMeshAgent.isStopped = true;
            gameObject.GetComponent<EnemyMove>();
            Debug.Log("hello");
        }
    }

}
