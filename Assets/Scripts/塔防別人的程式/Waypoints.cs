using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;
    void Awake() 
    {
        points = new Transform[transform.childCount]; // 取得路徑點個數
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i); // 取得路徑點
        }
    }
}
