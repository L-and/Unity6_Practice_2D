using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _smoothSPeed = 5f;
    [SerializeField] private Vector2 minBound;
    [SerializeField] private Vector2 maxBound;

    private Vector3 _offset;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        var destination = target.transform.position + _offset;
        Vector3 smoothPos =  Vector3.Lerp(transform.position, destination, _smoothSPeed * Time.deltaTime);
        
        smoothPos.x = Mathf.Clamp(smoothPos.x, minBound.x, maxBound.x);
        smoothPos.y = Mathf.Clamp(smoothPos.y, minBound.y, maxBound.y);
        
        transform.position = smoothPos;
    }
}
