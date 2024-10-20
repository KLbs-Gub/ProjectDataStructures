// Written By Kieran Pounds
// 10/15/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    private float camSpeed = 9f;

    private Camera camPos;
    private Vector3 targetPoint = new Vector3(0, 0, -10);

    // Gets and sets
    public Vector3 TargetPoint
    {
        get { return this.targetPoint; }
        set { 
            this.targetPoint.x = value.x;
            this.targetPoint.y = value.y;
            this.targetPoint.z = -10;
        }
    }

    // Methods

    private void Awake()
    {
        camPos = gameObject.GetComponent<Camera>();
    }


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        camPos.transform.position = Vector3.Lerp(camPos.transform.position, targetPoint, camSpeed * Time.deltaTime);
    }

}
