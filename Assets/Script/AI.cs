using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints;
    NavMeshAgent _agent;
    

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Waypoints[0] = GameObject.Find("AIStartPoint").transform;
        Waypoints[1] = GameObject.Find("AIEndPoint").transform;
        _agent.destination = Waypoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            _agent.destination = Waypoints[1].transform.position;
    }
}
