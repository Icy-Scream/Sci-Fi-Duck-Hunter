using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Barrel : MonoBehaviour
{
    public bool _isIgnited { get; set; }
    [SerializeField] private GameObject _explosion;

    private void Update()
    {
        if (_isIgnited) 
        {
            _explosion.SetActive(true);
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
