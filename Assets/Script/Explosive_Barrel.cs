using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Barrel : MonoBehaviour
{
    public bool _isIgnited { get; set; }
    private bool _isDestroyed = false;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioSource _audioSource;
    private void Update()
    {
        if (_isIgnited) 
        {
            _isIgnited = false;
            _isDestroyed = true;
            _explosion.SetActive(true);
            _audioSource.Play();
            Destroy(this.gameObject, 2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && _isDestroyed) 
        {
            other.TryGetComponent<AI>(out AI aI);
            SpawnManager._instance._spawnCount--;
            aI.Death();
        }
    }
}
