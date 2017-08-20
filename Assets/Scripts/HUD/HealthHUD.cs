using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHUD : MonoBehaviour
{
    private Transform CameraTransform;
    void Start()
    {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
	void Update () {
        //transform.rotation = Quaternion.LookRotation(CameraTransform.position.normalized);
	    transform.rotation = Quaternion.LookRotation(Vector3.forward)*CameraTransform.rotation;
    }
}
