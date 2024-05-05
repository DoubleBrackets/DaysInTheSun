using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scatter : MonoBehaviour
{
    [SerializeField]
    private int _scatterAmount;

    [SerializeField]
    private GameObject _scatterPrefab;

    [SerializeField]
    private float _minDist;
    
    [SerializeField]
    private float _maxDist;
    
    private void Awake()
    {
        for (int i = 0; i < _scatterAmount; i++)
        {
            var scatter = Instantiate(_scatterPrefab, transform);
            var random = Random.insideUnitCircle.normalized * Random.Range(_minDist, _maxDist);
            
            Vector3 pos = new Vector3(random.x, Random.Range(-30, 35f), random.y);
            
            scatter.transform.position = pos;
            
            scatter.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), transform.position - pos);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawLine(position, position + Vector3.right * _minDist);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(position, position + Vector3.right * _maxDist + Vector3.up * 10f);
    }
}
