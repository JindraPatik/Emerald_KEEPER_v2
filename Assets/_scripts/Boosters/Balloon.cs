using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private float _randomYTorqueRadius;
    [SerializeField] private float _minTorqueRadius, _maxTorqueRadius;
    [SerializeField] private GameObject _supplyCrate;
    private ConstantForce _constantForce;
    private Transform _myCurrentTransform;
    [SerializeField] float _hangingSpeed;
    [SerializeField] float _hangingAmplitude;
    [SerializeField] float _crateRotationSpeed;
    [SerializeField] float _crateRotationAmplitude;



    private void Start()
    {
        GetRandomLocalTorque();
    }

    private void Update()
    {
        float tZ = Time.time * _hangingSpeed;
        float tY = Time.time * _crateRotationSpeed;
        float rotationZ = Mathf.Sin(tZ) * _hangingAmplitude;
        float rotationY = Mathf.Sin(tY) * _crateRotationAmplitude;

        Quaternion rotation = Quaternion.Euler(transform.localRotation.x, rotationY , rotationZ);
        transform.localRotation = rotation;
    }

    private void GetRandomLocalTorque()
    {
        _constantForce = GetComponent<ConstantForce>();
        _randomYTorqueRadius = Random.Range(_minTorqueRadius, _maxTorqueRadius);
        Vector3 torque = _constantForce.relativeTorque;
        torque.y = _randomYTorqueRadius;
        _constantForce.relativeTorque = torque;
    }

    private TerrainCollider terrain;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<TerrainCollider>(out terrain)) 
        {
            _myCurrentTransform = transform;
            Instantiate(_supplyCrate, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
