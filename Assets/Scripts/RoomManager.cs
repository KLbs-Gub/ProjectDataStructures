using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Room manager variables
    [SerializeField] private Room room;

    [Range(1, 15)][SerializeField] private int columns;
    [Range(1, 15)][SerializeField] private int rows;
    private Vector3 transformOffset;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRooms();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Self explanatory
    private void GenerateRooms()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                transformOffset = new Vector3(17.8f * i, 10 * j, 0);
                Room newRoom = Instantiate(room, transformOffset, transform.rotation);

                // Make sure rooms on the edges have walls to the void blocked off.
                if (j == rows - 1) newRoom.EnableBlocker("EntranceUp");
                if (i == 0) newRoom.EnableBlocker("EntranceLeft");
                if (j == 0) newRoom.EnableBlocker("EntranceDown");
                if (i == columns - 1) newRoom.EnableBlocker("EntranceRight");
            }
        }
    }
}
