using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField]
    private Transform firstControlPoint;
    [SerializeField]
    private Transform secondControlPoint;

    internal Transform GetFirstControlPoint()
    {
        return firstControlPoint;
    }

    internal Transform GetSecondControlPoint()
    {
        return secondControlPoint;
    }
}