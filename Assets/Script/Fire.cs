using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    private Ray _ray;
    private ActionMaps _inputs;
    private AudioSource _gunFire;
   [SerializeField] private AudioClip _barrier;
    private bool _canFire = true;
    [SerializeField] private Camera _camera;
    private PlayerManager _playerManager;
    [SerializeField] private LayerMask _mask;
    private float _timer = 0.5f;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _gunFire = GetComponent<AudioSource>();
        _inputs = new ActionMaps();
    }

    private void OnEnable()
    {
        _inputs.Player.Enable();
        _inputs.Player.Fire.performed += Fire_performed;
        _inputs.Player.Fire.canceled += Fire_canceled;
    }

    private void Fire_canceled(InputAction.CallbackContext obj)
    {
            _canFire = true;
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        if (_canFire) 
        {
            _ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            _gunFire.Play();
            _canFire = false;
            _timer += Time.time;
            Firing();
        }
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
            else if(_hit.transform.CompareTag("Barriers")) 
            {
                Debug.Log("BARRIER");
                AudioSource.PlayClipAtPoint(_barrier, new Vector3(0, 11, 32), 500);
            }
        }

    }

    IEnumerator FiringCoolDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _canFire = true;
    }
}
