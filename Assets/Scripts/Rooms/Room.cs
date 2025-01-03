using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject mainCam;

    [SerializeField] private GameObject[] entranceBlockers = new GameObject[4];
    [SerializeField] private WaveManager waveManager;

    // types: "safe", "hostile", "boss"
    [HideInInspector] public string roomType = "hostile";

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

        if (roomType != "boss")
        {
            waveManager.spawnAmount = Random.Range(7, 12);
        }
        else if (roomType == "boss")
        {
            waveManager.timerStartValue = 85f;
            waveManager.endsGame = true;
            waveManager.spawnAmount = Random.Range(20, 30);
        }

        waveManager.SelfDisable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mainCam != null && collision.gameObject.tag == "Player")
        {
            mainCam.GetComponent<CameraMain>().TargetPoint = new Vector3(transform.position.x, transform.position.y, 1);

            collision.GetComponent<Player>().currentRoomPosition = transform.position;

            // Only activate the wave manager if the room is hostile or boss.
            if (roomType != "safe" && waveManager != null)
            {
                waveManager.SelfActivate();
            }
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

                // Make it so enemies don't spawn out of bounds.
                waveManager.validSpawnPoints.Remove(aBlocker);
            }
        }
    }
}
