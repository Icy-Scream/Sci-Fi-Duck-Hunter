using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager _instance { get; private set; }

    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnTime;
    [SerializeField] private GameObject _enemyPool;
    [SerializeField] public int _spawnLimit { get; private set; }
    [SerializeField] private int _spawnStartingValue;
    public int _spawnCount { get; set; }
    private int _stopSpawning = 0;
    private bool spawn = true;
    private Vector3 _spawnLocation;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this);
        else _instance = this;
        _spawnLimit = _spawnStartingValue;
        _spawnCount = _spawnLimit;
    }

    private void Update()
    {
        UIManager._instance.SpawnCount(_spawnCount, _spawnLimit);
        if (spawn && _stopSpawning < _spawnLimit) 
        {
            spawn = false;
            StartCoroutine(SpawnRoutine());
        }
    }

   private IEnumerator SpawnRoutine() 
    {  
        Instantiate(_enemy, _spawnPoint.position,Quaternion.identity,_enemyPool.transform);
        _stopSpawning++;
        yield return new WaitForSeconds(_spawnTime);
        spawn = true;
    }
}
