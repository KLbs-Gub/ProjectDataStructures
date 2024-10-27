using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject mainCam;

    [SerializeField] private GameObject[] entranceBlockers = new GameObject[4];

    // 2 = waiting, 1 = active, 0 = complete
    [HideInInspector] public int roomState = 2;
    [HideInInspector] public int wavePopulation;
    private float spawnTimer = 50f;
    public GameObject enemy;

    // Start is called before the first frame update
    private void Awake()
    {
        wavePopulation = 12;

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
        if (roomState == 1 && wavePopulation > 0 && spawnTimer < 0)
        {
            Vector2 transformOffset = new Vector2(transform.position.x, transform.position.y + 10);
            Instantiate(enemy, transformOffset, transform.rotation);
            
            transformOffset = new Vector2(transform.position.x, transform.position.y - 10);
            Instantiate(enemy, transformOffset, transform.rotation);

            transformOffset = new Vector2(transform.position.x + 10, transform.position.y);
            Instantiate(enemy, transformOffset, transform.rotation);

            transformOffset = new Vector2(transform.position.x - 10, transform.position.y);
            Instantiate(enemy, transformOffset, transform.rotation);

            wavePopulation -= 4;
            spawnTimer = 50f;
        }

        spawnTimer--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mainCam != null)
        {
            mainCam.GetComponent<CameraMain>().TargetPoint = new Vector3(transform.position.x, transform.position.y, 1);
        }

        if (roomState == 2)
        {
            roomState = 1;
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
