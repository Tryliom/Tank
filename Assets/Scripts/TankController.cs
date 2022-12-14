using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private float _fireRate = 0.1f;
    
    private Rigidbody _rigidbody;
    
    private float _direction;
    private float _rotation;
    
    private bool _canFire = true;
    private bool _isShooting;

    private void Awake()
    {
        StartCoroutine(nameof(ShootDelay));
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction = Input.GetAxis("Vertical");
        _rotation = Input.GetAxis("Horizontal");

        if (Input.GetMouseButtonDown((int) MouseButton.Left))
        {
            _isShooting = true;
        }
        else if (Input.GetMouseButtonUp((int) MouseButton.Left))
        {
            _isShooting = false;
        }
    }
    
    private void FixedUpdate()
    {
        var velocity = _speed * Time.deltaTime * _direction * transform.forward;
        var rotation = Vector3.up * (_rotationSpeed * Time.deltaTime * _rotation);
        
        _rigidbody.velocity = velocity;
        
        if (_rigidbody.velocity.sqrMagnitude <= Mathf.Epsilon)
        {
            _rigidbody.rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles + rotation);
        }
    }
    
    private void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

        if (bullet.TryGetComponent(out Rigidbody bulletRigidbody))
        {
            bulletRigidbody.velocity = _rigidbody.velocity;
            bulletRigidbody.AddRelativeForce(Vector3.forward * 1000f);
        } 
    }
    
    private IEnumerator ShootDelay()
    {
        while (true)
        {
            if (_canFire && _isShooting)
            {
                Shoot();
                _canFire = false;
                
                yield return new WaitForSeconds(_fireRate);
                
                _canFire = true;
            }

            yield return null;
        }
    }
}
