using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mainCam != null)
        {
            mainCam.GetComponent<CameraMain>().TargetPoint = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }
}
