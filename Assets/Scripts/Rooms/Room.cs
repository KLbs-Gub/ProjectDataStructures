using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject mainCam;

    [SerializeField] private GameObject[] entranceBlockers = new GameObject[4];
    [SerializeField] private WaveManager waveManager;

    // types: "safe", "hostile", "boss"
    [HideInInspector] public string roomType = "safe";

    // Start is called before the first frame update
    private void Awake()
    {
        foreach (GameObject gameObject in entranceBlockers)
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        mainCam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mainCam != null)
        {
            mainCam.GetComponent<CameraMain>().TargetPoint = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }

    public void EnableBlocker(string aBlocker)
    {
        foreach (GameObject gameObject in entranceBlockers)
        {
            if (gameObject.name == aBlocker)
            {
                //Debug.Log("Blocked an: " +  gameObject.name);
                gameObject.SetActive(true);
            }
        }
    }
}
