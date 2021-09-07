using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfig", menuName = "Game Config", order = 51)]

public class GameConfig : ScriptableObject
{
    public Vector3 initialRotationSabreLeft;
    public Vector3 initialRotationSabreRight;
    public float simulationSpeed;
    public float swordMinRotation;
    public float swordMaxRotation;
}
