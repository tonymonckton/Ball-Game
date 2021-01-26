﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion faceCamera = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        transform.rotation = faceCamera;
    }
}
