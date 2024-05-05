using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollowScript : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _springConstant;

    [SerializeField]
    private float _dampingConstant;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _radius;

    private Vector3 _targetPosition;
    
    private void Update()
    {
        UpdateTargetPosition();
        
        var currentPosition = transform.position;
        
        var displacement = _targetPosition - currentPosition;
        
        var springForce = _springConstant * displacement;

        var dampingForce = _dampingConstant * _rb.velocity;
        
        var force = springForce - dampingForce;
        
        _rb.velocity += force * Time.deltaTime;
    }

    private void UpdateTargetPosition()
    {
        var currentPosition = transform.position;
        _targetPosition = _target.position + (currentPosition - _target.position).normalized * _radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_targetPosition, 0.5f);
    }
}
