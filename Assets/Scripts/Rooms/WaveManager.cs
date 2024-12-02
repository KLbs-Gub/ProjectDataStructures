using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject self;

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
