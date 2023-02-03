using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IExplode
{
    [SerializeField] private int _barrierHealth;
    void IExplode.Damage(int damage)
    {
        _barrierHealth -= damage;
    }

    private void Update()
    {
        if (_barrierHealth <= 0)
            Destroy(this.gameObject);
    }
}
