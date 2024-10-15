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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetPoint.Set(0, camPos.transform.position.y + 10, -10);
        }
    }
}
