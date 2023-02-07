using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IExplode
{
    [SerializeField] private int _barrierHealth;
    private bool reset;
    void IExplode.Damage(int damage)
    {
        if(_barrierHealth > 0 )
        _barrierHealth -= damage;
    }

    private void Update()
    {
        if (_barrierHealth <= 0) 
        {
            reset = true;
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (reset) 
        {
            StartCoroutine(ResetRoutine());
        }
    }

    IEnumerator ResetRoutine() 
    {
        reset = false;
        yield return new WaitForSeconds(8f);
        _barrierHealth = 50;
        GetComponent<MeshRenderer>().enabled = true; 
    }
}
