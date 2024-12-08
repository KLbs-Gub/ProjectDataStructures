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
    public float spawnTimer = 10f;

    private int spawnAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
