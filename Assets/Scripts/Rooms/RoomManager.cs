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

    [Range(1, 15)][SerializeField] private int columns;
    [Range(1, 15)][SerializeField] private int rows;
    private Vector3 transformOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Run this last
        GenerateRooms();
    }

    // Self explanatory
    private void GenerateRooms()
    {
        // Variables used to determine when to generate a special room.
        int playerRoomCountdown = Random.Range(0, rows * columns);
        int shopRoomCountdown = Random.Range(0, rows * columns);
        int bossRoomCountdown = Random.Range(0, rows * columns);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Count down special room types
                playerRoomCountdown--;
                shopRoomCountdown--;
                bossRoomCountdown--;

                transformOffset = new Vector3(17.8f * i, 10 * j, 0);
                Room newRoom = Instantiate(room, transformOffset, transform.rotation);

                #region RoomGen else/if chain
                // If a special room count is below zero, generate that special room type.
                // We also set the count to an absurdly high number so it doesn't show up again.
                if (playerRoomCountdown < 0)
                {
                    // Don't generate a layout for player spawn room
                    // but DO move the player to this room.
                    GameObject player = GameObject.Find("Player");
                    player.transform.position = transformOffset;

                    // Move the camera too
                    GameObject camera = GameObject.Find("Main Camera");
                    camera.GetComponent<CameraMain>().TargetPoint = new Vector3(transformOffset.x, transformOffset.y, 1);
                    camera.transform.position = transformOffset;

                    // Make sure the player spawn room doesn't have any enemies.
                    newRoom.roomType = "safe";

                    AddLayout("start");

                    playerRoomCountdown = 99999;
                }
                else if (shopRoomCountdown < 0)
                {
                    AddLayout("shop");

                    // Make sure the shop room doesn't have any enemies.
                    newRoom.roomType = "safe";

                    shopRoomCountdown = 99999;
                }
                else if (bossRoomCountdown < 0)
                {
                    AddLayout("boss");

                    // This room type has the logic for bosses.
                    newRoom.roomType = "boss";

                    bossRoomCountdown = 99999;
                }
                else
                {
                    AddLayout("normal");

                    // This is a normal hostile room
                    newRoom.roomType = "hostile";
                }
                #endregion

                // Make sure rooms on the edges have holes to the void blocked off.
                if (j == rows - 1) newRoom.EnableBlocker("EntranceUp");
                if (i == 0) newRoom.EnableBlocker("EntranceLeft");
                if (j == 0) newRoom.EnableBlocker("EntranceDown");
                if (i == columns - 1) newRoom.EnableBlocker("EntranceRight");
            }
        }
    }

    // Add a room layout to the room at the transform offset.
    private void AddLayout(string layoutType)
    {
        int roomSelect;
        if (layoutType == "normal")
        {
            roomSelect = Random.Range(1, levels[currentLevel].normalRoomLayoutsList.Count);

            GameObject layout = Instantiate(levels[currentLevel].normalRoomLayoutsList[roomSelect], transformOffset, transform.rotation);
        }
        else if (layoutType == "shop")
        {
            roomSelect = Random.Range(0, levels[currentLevel].shopRoomLayoutsList.Count);

            GameObject layout = Instantiate(levels[currentLevel].shopRoomLayoutsList[roomSelect], transformOffset, transform.rotation);
        }
        else if (layoutType == "boss")
        {
            roomSelect = Random.Range(0, levels[currentLevel].bossRoomLayoutsList.Count);

            GameObject layout = Instantiate(levels[currentLevel].bossRoomLayoutsList[roomSelect], transformOffset, transform.rotation);
        }
        else if (layoutType == "start")
        {
            GameObject layout = Instantiate(levels[currentLevel].normalRoomLayoutsList[0], transformOffset, transform.rotation);
        }
    }
}
