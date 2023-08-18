using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints2 : MonoBehaviour
{
    public static Transform[] points2;
    void Awake() 
    {
        points2 = new Transform[transform.childCount]; // 取得路徑點個數
        for (int i = 0; i < points2.Length; i++)
        {
            points2[i] = transform.GetChild(i); // 取得路徑點
        }
    }
}
