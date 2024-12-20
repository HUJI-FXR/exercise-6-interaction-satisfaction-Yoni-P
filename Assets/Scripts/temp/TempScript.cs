using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    public enum MovementDirection
    {
        SideToSide = 0,
        UpAndDown = 1
    }
    [SerializeField] private Vector3 movementDirection;

    private float duration = 5f;
    private float t = 0;
    private float x = 0;
    private void Update()
    {
        x = Mathf.Lerp(0, 10, t / duration);
        t += Time.deltaTime;
    }
}