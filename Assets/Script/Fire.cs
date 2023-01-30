using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    Ray _ray;
    bool _canFire = true;
    [SerializeField]Camera _camera;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] private LayerMask _mask;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
    }
    private void FixedUpdate()
    {
        _ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Mouse.current.leftButton.isPressed && _canFire)
        {
            _canFire = false;
            Firing();
            StartCoroutine(FiringCoolDownRoutine());
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_ray);
    }

    private void Firing()
    {
        if (Physics.Raycast(_ray, out RaycastHit _hit, Mathf.Infinity,_mask))
        {
            if (_hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("HIT ENEMY");
                _hit.transform.gameObject.TryGetComponent<AI>(out AI ai);
                SpawnManager._instance._spawnCount--;
                _playerManager.PlayerScore(60);
                ai.Death();
            }
            else
                Debug.Log(_hit.ToString());
        }

    }

    IEnumerator FiringCoolDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _canFire = true;
    }
}
