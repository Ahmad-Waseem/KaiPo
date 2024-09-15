using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static float leftSide = -6f;
    public static float rightSide = 6.5f;
    public static float up = 10f;
    public static float down = 0.45f;

    public float internalLeftSide;
    public float internalRightSide;
    public float internalUp;
    public float internalDown;

    private void Start()
    {
        internalLeftSide = leftSide;
        internalDown = down;
        internalRightSide = rightSide;
        internalUp = up;
    }
}
