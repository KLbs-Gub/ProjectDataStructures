using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Levels/Room")]
public class RoomLayoutScriptObject : ScriptableObject
{
    [HideInInspector] public string globalUniqueIdentifierField;
}
