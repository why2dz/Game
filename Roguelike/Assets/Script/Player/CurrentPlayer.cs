using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayer", menuName = "Scriptable Objects/Player/CurrentPlayer")]
public class CurrentPlayer : ScriptableObject
{
    public PlayerSO playerSO;

    public string PlayerName;
}
