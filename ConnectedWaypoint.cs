using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedWaypoint : Waypoint
{
    [SerializeField]
    //protected 
    public float connectivityRadius = 50f; //change to best suit each map

    List<ConnectedWaypoint> connections;
    public bool idle = false;

    // Start is called before the first frame update
    void Start()
    {
        //find everywaypoint in the scene
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        connections = new List<ConnectedWaypoint>();//list all the points

        //check if waypoints are connected
        for (int i = 0; i < allWaypoints.Length;i++){
            ConnectedWaypoint nextWaypoint = allWaypoints[i].GetComponent<ConnectedWaypoint>();

            if(nextWaypoint != null){
                if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectivityRadius && nextWaypoint != this){
                    connections.Add(nextWaypoint);
                }
            }
        }
    }


    public override void OnDrawGizmos()
    {
        //base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, connectivityRadius);
    }

    public ConnectedWaypoint nextWaypoint(ConnectedWaypoint previousWaypoint){

        if(connections.Count == 0){
            //returns null when no waypoints are in range
            idle = true;
            Debug.LogError("no waypoint found");
            return null;
        }
        //only 1 waypoint
        else if (connections.Count == 1 && connections.Contains(previousWaypoint)){
            idle = false;
            return previousWaypoint;
        }
        //random waypoint that is not the previos one
        else{
            idle = false;
            ConnectedWaypoint nextWaypoint;
            int nextIndex = 0;

            do
            {
                nextIndex = UnityEngine.Random.Range(0, connections.Count);
                nextWaypoint = connections[nextIndex];

            } while (nextWaypoint == previousWaypoint);

            return nextWaypoint;
        }


    }
}
