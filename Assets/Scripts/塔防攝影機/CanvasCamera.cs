using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        // 計算怪物的位置與攝影機之間的方向向量
        Vector3 directionToCamera = transform.position - mainCameraTransform.position;

        // 使Canvas的物件面向攝影機
        transform.rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);
    }
}
