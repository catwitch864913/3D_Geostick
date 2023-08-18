using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private Transform basicMap;
    private string mapName = "Basic Map";
    private float rotationSpeed = 5.0f;

    private void Awake()
    {
        basicMap = GameObject.Find(mapName)?.transform;
        if (basicMap == null)
        {
            Debug.LogWarning("Map not found!");
            return;
        }
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0)) transform.RotateAround(basicMap.position, Vector3.up, mouseX * rotationSpeed);
    }
}
