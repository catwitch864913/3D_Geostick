using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionStick : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 startPosition;

    private void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // 檢查是否接觸到點位，並執行連接邏輯
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        // 可以使用碰撞檢測或觸發器進行判斷
        // 如果接觸到點位，則根據需要執行連接邏輯
        // 例如設置連接棒和點位的連接狀態或位置
    }
}
