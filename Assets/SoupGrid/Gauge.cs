using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public float maxY;
    [Tooltip("Pozition at which flui dissapears behind red line")]
    public float minY;

    [Header("References")]
    public Transform fluidParent;

    public void adjustFluid(float percent)
    {
        fluidParent.localPosition = Vector3.up * (minY + (percent * (maxY - minY)));
    }
}
