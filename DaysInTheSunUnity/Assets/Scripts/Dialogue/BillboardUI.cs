using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private void LateUpdate()
    {
        // look away from the camera
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
