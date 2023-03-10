using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    private Ray _ray;
    private ActionMaps _inputs;
    private AudioSource _gunFire;
    [SerializeField] private AudioClip _barrier;
    [SerializeField] private GameObject _muzzleFlash;
    private bool _canFire = true;
    [SerializeField] private Camera _camera;
    private PlayerManager _playerManager;
    [SerializeField] private LayerMask _mask;
    private float _timer = 0.5f;
    private IExplode _damagable;

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
        _muzzleFlash.SetActive(false);
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
            _muzzleFlash.SetActive(true);
            _gunFire.Play();
            _canFire = false;
            _timer += Time.time;
            Firing();
        }
    }
    private void Firing()
    {
        StartCoroutine(FiringCoolDownRoutine());
        if (Physics.Raycast(_ray, out RaycastHit _hit, Mathf.Infinity,_mask))
        {
            if (_hit.transform.CompareTag("Enemy"))
            {
                _hit.transform.gameObject.TryGetComponent<AI>(out AI ai);
                SpawnManager._instance._spawnCount--;
                _playerManager.PlayerScore(60);
                ai.Death();
            }
            else if (_hit.transform.CompareTag("Barriers"))
            {
                _damagable = _hit.transform.gameObject.GetComponent<Barrier>();
                _damagable.Damage(5);
                AudioSource.PlayClipAtPoint(_barrier, new Vector3(0, 11, 32), 500);
            }
            else 
            {
                Explosive_Barrel barrel = _hit.transform.gameObject.GetComponentInParent<Explosive_Barrel>();
                barrel._isIgnited = true;
                Debug.Log("BOOM");
            }
        }
        
    }

    IEnumerator FiringCoolDownRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        _muzzleFlash.SetActive(false);
    }

    private void OnDisable()
    {
        _inputs.Player.Fire.performed -= Fire_performed;
        _inputs.Player.Fire.canceled -= Fire_canceled;
        _inputs.Player.Disable();
    }
}
