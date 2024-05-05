using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollowScript : MonoBehaviour
{

    [SerializeField]
    private float _springConstant;

    [SerializeField]
    private float _dampingConstant;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _radius;
    
    [SerializeField]
    private float _baseHeight;
    
    [SerializeField]
    private float _heightOffset;

    [SerializeField]
    private float _heightWaveSpeed;

    private Vector3 _targetPosition;
    
    private Transform _target;

    private void Start()
    {
        _target = ProtagController.Instance.MeshBody.transform;
    }

    private void Update()
    {
        UpdateTargetPosition();
        
        var currentPosition = transform.position;
        
        var displacement = _targetPosition - currentPosition;
        
        var springForce = _springConstant * displacement;

        var dampingForce = _dampingConstant * _rb.velocity;
        
        var force = springForce - dampingForce;
        
        _rb.velocity += force * Time.deltaTime;
        
        float t = 1 - Mathf.Pow(1 - 0.999f, Time.deltaTime * 10);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(ProtagController.Instance.Facing, Vector3.up),
            t);
    }

    private void UpdateTargetPosition()
    {
        var currentPosition = transform.position;
        var forward = ProtagController.Instance.Facing.normalized;
        var right = (Vector3.Cross(Vector3.up, forward) + forward * 0.4f).normalized;
        var left = (-Vector3.Cross(Vector3.up, forward) + forward * 0.4f).normalized;
        
        var leftDist = Vector3.Distance(currentPosition, _target.position + left * _radius);
        var rightDist = Vector3.Distance(currentPosition, _target.position + right * _radius);
        
        if (leftDist < rightDist)
        {
            right = left;
        }
        
        float height = Mathf.Sin(Time.time * _heightWaveSpeed) * _heightOffset + _baseHeight;
        
        _targetPosition = _target.position + right * _radius + Vector3.up * height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_targetPosition, 0.5f);
    }
}
