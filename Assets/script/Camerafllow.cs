﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafllow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    // Start is called before the first frame update
    void Start()
    {

    }
    void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPosition = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
