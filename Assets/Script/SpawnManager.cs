using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnTime;
    private bool spawn = true;
    private Vector3 _spawnLocation;

    private void Update()
    {
        if (spawn) 
        {
            spawn = false;
            StartCoroutine(SpawnRoutine());
        }
    }

   private IEnumerator SpawnRoutine() 
    {  
        Instantiate(_enemy, _spawnPoint);
        yield return new WaitForSeconds(_spawnTime);
        spawn = true;
    }
}
