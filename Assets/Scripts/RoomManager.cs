using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject room;

    [Range(1, 15)] [SerializeField] private int columns;
    [Range(1, 15)] [SerializeField] private int rows;
    private Vector3 transformOffset;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                transformOffset = new Vector3(17.8f * i, 10 * j, 0);
                Instantiate(room, transformOffset, transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
