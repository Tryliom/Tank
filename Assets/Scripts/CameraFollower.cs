using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _locationOffset = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 _rotationOffset = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {
        var rotation = _player.transform.rotation;
        var desiredPosition = _player.transform.position + rotation * _locationOffset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;

        var desiredRotation = rotation * Quaternion.Euler(_rotationOffset);
        var smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, _smoothSpeed);
        transform.rotation = smoothedRotation;
    }
}
