using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) animator.SetBool("Open",true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) animator.SetBool("Open", false);
    }
}
