using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximShader : MonoBehaviour
{
    [SerializeField]
    private Material _material;

    // Update is called once per frame
    void Update()
    {
        var cPos = _material.GetVector("_Position");
        var newPos = Vector3.Lerp(cPos, transform.position, 55f * Time.deltaTime);
        _material.SetVector("_Position", newPos);
    }
}
