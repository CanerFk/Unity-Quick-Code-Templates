using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float runningMovementSpeed { get; private set; } = 6f;
    public float walkingMovementSpeed { get; private set; } = 3f;
    public float gravity { get; private set; } = -21f;
    public float jumpHeight { get; private set; } = 1.5f;

}
