using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private List<Transform> _cover;
    private Animator _animator;
    private Transform _startingPoint;
    private Transform _endPoint;
    private int _destinationPoint = 0;
    private NavMeshAgent _agent;
    private bool _hiding = false;
    private bool _isDead = false;
    [SerializeField] private AIStates _currentState;
    
   

    private void Awake()
    {
        _cover = new List<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _currentState = AIStates.Run;
        _animator = GetComponent<Animator>();

    }

    void Start()
    {

        _cover = GameObject.Find("Cover").GetComponentsInChildren<Transform>().Skip(1).ToList<Transform>();
        _startingPoint = GameObject.Find("AIStartPoint").transform;
        _endPoint = GameObject.Find("AIEndPoint").transform;
        _agent.destination = _startingPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) _currentState = AIStates.Death;
       
        switch(_currentState)
        {
            case AIStates.Run:
                if (!_hiding)
                {
                    _hiding = true;
                    StartCoroutine(FindCoverRoutine());
                }
                _agent.isStopped = false;
                    if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                        _agent.destination = _endPoint.position;
                break;
           
            case AIStates.Hide:
                if (_agent.remainingDistance < 0.5f) 
                { 
                  _animator.SetBool("Hiding",true);
                    _agent.isStopped = true;
                }
                
                break;

            case AIStates.Death:
                _agent.isStopped = true;
                gameObject.transform.GetComponent<Collider>().enabled = false;
              _animator.SetBool("Death", true);
                Destroy(this.gameObject, 2f);
                break;

        
        };
    }
    private void GetCoverPoint() 
    {
        var currentPosition = transform.position;
        float distance = float.MaxValue;
        Vector3 destination = Vector3.zero;
        foreach (var cover in _cover)
        {
            float coverDistance = Vector3.Distance(currentPosition, cover.position);
            if (coverDistance < distance) 
            { 
                distance = coverDistance;
                destination = cover.position;
            }
        }
        if(distance > 10) 
        {
            _agent.destination = _endPoint.position;
        }
        else
        _agent.destination = destination;
    }

    IEnumerator FindCoverRoutine() 
    {
        yield return new WaitForSeconds(Random.Range(1f, 8f));
        _currentState = AIStates.Hide;
        GetCoverPoint();
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        _hiding = false;
       _animator.SetBool("Hiding", false);
        _currentState = AIStates.Run;
    }

    public void Death()
    {
        _isDead = true;
        StopAllCoroutines();
    }

    public enum AIStates{ Run,Hide,Death}
}
