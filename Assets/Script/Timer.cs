using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float _time = 120;

    private void Update()
    {
        if (_time > 0) 
        { 
           _time -= Time.deltaTime;
            UIManager._instance.Timer(_time);
            if (_time == 0) _time = 0;
        }

    }
}
