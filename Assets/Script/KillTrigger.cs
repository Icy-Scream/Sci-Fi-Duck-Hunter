using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _lose;
    [SerializeField] private GameManager _manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { 
            Destroy(other.gameObject);
            _manager._enemiesGoal++;
            SpawnManager._instance._spawnCount--;
            AudioSource.PlayClipAtPoint(_lose, new Vector3(0, 11, 30), 150);
        }
    }
}
