using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Room manager variables
    [SerializeField] private Room room;

    public List<Level1ScriptObject> levels;
    public int currentLevel = 0;

    private List<GameObject> normalRooms;
    private List<GameObject> shopRooms;
    private List<GameObject> bossRooms;

    [Range(1, 15)][SerializeField] private int columns;
    [Range(1, 15)][SerializeField] private int rows;
    private Vector3 transformOffset;

    // Start is called before the first frame update
    void Start()
    {
        PopulateRoomLists();

        // Run this last
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

                AddLayout("normal");

                // Make sure rooms on the edges have holes to the void blocked off.
                if (j == rows - 1) newRoom.EnableBlocker("EntranceUp");
                if (i == 0) newRoom.EnableBlocker("EntranceLeft");
                if (j == 0) newRoom.EnableBlocker("EntranceDown");
                if (i == columns - 1) newRoom.EnableBlocker("EntranceRight");
            }
        }
    }

    // Get the rooms from the level scriptable objects
    private void PopulateRoomLists()
    {
        normalRooms = levels[currentLevel].normalRoomLayoutsList;
        shopRooms = levels[currentLevel].shopRoomLayoutsList;
        bossRooms = levels[currentLevel].bossRoomLayoutsList;
    }

    // Add a room layout to the room at the transform offset.
    private void AddLayout(string layoutType)
    {
        int roomSelect;
        if (layoutType == "normal")
        {
            roomSelect = Random.Range(0, normalRooms.Count);

            GameObject layout = Instantiate(normalRooms[roomSelect], transformOffset, transform.rotation);
        }
    }
}
