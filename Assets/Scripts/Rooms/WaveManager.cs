// Written by Kieran Pounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject self;
    public List<string> validSpawnPoints = new List<string>
    {
        "EntranceUp", "EntranceDown", "EntranceLeft", "EntranceRight"
    };
    public float timerStartValue = 65f;
    public int spawnAmount = 1;

    private List<EnemyBase> possibleEnemies = new List<EnemyBase>();
    private float spawnTimer = 65f;

    // Start is called before the first frame update
    void Start()
    {
        RoomManager roomManager = FindAnyObjectByType<RoomManager>();

        possibleEnemies = roomManager.levels[roomManager.currentLevel].availableEnemiesList;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer--;
        if (spawnAmount > 0 && spawnTimer <= 0f)
        {
            int random = Random.Range(0, possibleEnemies.Count);
            EnemyBase enemy = Instantiate(possibleEnemies[random], transform.position, transform.rotation);
            enemy.transform.parent = transform;

            spawnAmount--;
            spawnTimer = timerStartValue;
        }

        if (transform.childCount - 4 == 0 && spawnAmount <= 0)
        {
            SelfDisable();
        }
        // EnemyBase enemy = Instantiate(possibleEnemies[0], transform.position, transform.rotation);
        // enemy.transform.parent = transform;
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
