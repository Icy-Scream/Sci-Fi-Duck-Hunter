using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Barrel : MonoBehaviour
{
    public bool _isIgnited { get; set; }
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioSource _audioSource;
    private void Update()
    {
        if (_isIgnited) 
        {
            _isIgnited = false;
            _explosion.SetActive(true);
            _audioSource.Play();
            Destroy(this.gameObject, 2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && _isIgnited) 
        {
            other.TryGetComponent<AI>(out AI aI);
            aI.Death();
        }
    }
}
