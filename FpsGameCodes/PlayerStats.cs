using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float runningMovementSpeed { get; private set; } = 6f;
    public float walkingMovementSpeed { get; private set; } = 3f;
    public float crouchMovementSpeed { get; private set; } = 2f;
    public float gravity { get; private set; } = -21f;
    public float jumpHeight { get; private set; } = 1.5f;

    public float standingHeightY { get; private set; } = 2f;
    public float crouchHeightY { get; private set; } = 0.5f;
    public float crouchSpeed { get; private set; } = 10f;

}
