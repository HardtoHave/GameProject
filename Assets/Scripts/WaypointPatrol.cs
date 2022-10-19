using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    //set up navmeshagent component object 
    NavMeshAgent m_NavMeshAgent;
    //waypoint array
    public Transform[] waypoints;
    //current waypoint index
    int m_CurrentWaypointIndex;
    // Start is called before the first frame update
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        //set up nav start point
        m_NavMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        //cycle algorithm
        //distance to the particular point
        if (m_NavMeshAgent.remainingDistance < m_NavMeshAgent.stoppingDistance)
        {
            //get new waypoint index
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            //set new nav position
            m_NavMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        }
    }
}
