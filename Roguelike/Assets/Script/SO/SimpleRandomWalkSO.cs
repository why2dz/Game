using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SimplRandomWalkParameter_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10, walklength = 10;
    
    public bool startRandomlyEachIteration = true;
}
