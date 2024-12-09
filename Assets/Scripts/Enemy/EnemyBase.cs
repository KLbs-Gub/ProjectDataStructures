// Written by Kieran Pounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public abstract void EnterRoom(string direction);

    public abstract void EnemyKilled();
}
