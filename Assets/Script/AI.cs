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
        _agent.destination = Waypoints[1].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
