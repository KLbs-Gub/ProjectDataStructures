// Written by Kieran Pounds

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public GameObject self;
    public List<string> validSpawnPoints = new List<string>
    {
        "EntranceUp", "EntranceDown", "EntranceLeft", "EntranceRight"
    };
    public float timerStartValue = 65f;
    public int spawnAmount = 1;
    public bool endsGame = false;

    private List<EnemyBase> possibleEnemies = new List<EnemyBase>();
    private float spawnTimer = 35f;

    // Events
    public static event Action OnWaveComplete;

    // Start is called before the first frame update
    void Start()
    {
        RoomManager roomManager = FindAnyObjectByType<RoomManager>();

        possibleEnemies = roomManager.levels[roomManager.currentLevel].availableEnemiesList;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer--;
        if (spawnAmount > 0 && spawnTimer <= 0f)
        {
            SpawnEnemy();
        }

        if (transform.childCount - 4 == 0 && spawnAmount <= 0)
        {
            if (endsGame)
            {
                SceneManager.LoadSceneAsync("EndScreen");
            }

            OnWaveComplete?.Invoke();
            Destroy(gameObject);
        }
        // EnemyBase enemy = Instantiate(possibleEnemies[0], transform.position, transform.rotation);
        // enemy.transform.parent = transform;
    }

    public void SpawnEnemy()
    {
        // spawn a random enemy from the level's enemy list
        int random = UnityEngine.Random.Range(0, validSpawnPoints.Count);
        Vector2 spawnLocation = transform.position;

        // If else chain to spawn the enemy at one of the room entrances.
        if (validSpawnPoints[random] == "EntranceUp")
        {
            spawnLocation = new Vector2(transform.position.x, transform.position.y + 6f);
        }
        else if (validSpawnPoints[random] == "EntranceDown")
        {
            spawnLocation = new Vector2(transform.position.x, transform.position.y - 6f);
        }
        else if (validSpawnPoints[random] == "EntranceLeft")
        {
            spawnLocation = new Vector2(transform.position.x - 9.9f, transform.position.y);
        }
        else if (validSpawnPoints[random] == "EntranceRight")
        {
            spawnLocation = new Vector2(transform.position.x + 9.9f, transform.position.y);
        }

        EnemyBase enemy = Instantiate(possibleEnemies[UnityEngine.Random.Range(0, possibleEnemies.Count)], 
            spawnLocation, transform.rotation);
        enemy.EnterRoom(validSpawnPoints[random]);
        enemy.transform.parent = transform;

        spawnAmount--;
        spawnTimer = timerStartValue;
    }

    public void SelfDisable()
    {
        self.SetActive(false);
    }

    public void SelfActivate()
    {
        self.SetActive(true);
    }
}
