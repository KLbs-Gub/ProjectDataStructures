// Written by Kieran Pounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Scriptable Objects/Levels/Level")]
public class Level1ScriptObject : ScriptableObject
{
    #region Header LEVEL DETAILS

    [Space(10)]
    [Header("LEVEL DETAILS")]

    #endregion

    #region Tooltip
    [Tooltip("The level's name")]
    #endregion Tooltip

    public string levelName;

    #region Header LEVEL ROOMS
    [Space(10)]
    [Header("LEVEL ROOMS")]
    #endregion Header LEVEL ROOMS
    #region Tooltip
    [Tooltip("The room layouts that can be generated for normal rooms in this level")]
    #endregion Tooltip

    public List<GameObject> normalRoomLayoutsList;

    #region Tooltip
    [Tooltip("The room layouts that can be generated for shop rooms in this level")]
    #endregion Tooltip
    public List<GameObject> shopRoomLayoutsList;

    #region Tooltip
    [Tooltip("The room layouts that can be generated for boss rooms in this level")]
    #endregion Tooltip
    public List<GameObject> bossRoomLayoutsList;

    #region Header LEVEL ENEMIES
    [Space(10)]
    [Header("LEVEL ENEMIES")]
    #endregion Header LEVEL ENEMIES

    #region Tooltip
    [Tooltip("The enemies that can appear in this level")]
    #endregion Tooltip
    public List<EnemyBase> availableEnemiesList;

    #region Validation

#if UNITY_EDITOR
    // Validate details entered
    private void OnValidate()
    {
        string message = "";
        if (levelName == "")
        {
            message += this + " has unspecified level name!\n";
        }
        if (normalRoomLayoutsList.Count == 0)
        {
            message += this + " has no specified normal room layouts\n";
        }
        if (shopRoomLayoutsList.Count == 0)
        {
            message += this + " has no specified shop room layouts\n";
        }
        if (bossRoomLayoutsList.Count == 0)
        {
            message += this + " has no specified boss room layouts\n";
        }

        if (message != "")
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.ClearDeveloperConsole();
        }
    }

#endif

    #endregion Validation
}
