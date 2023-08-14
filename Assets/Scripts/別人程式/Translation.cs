using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    //static public bool isDragging = false;
    public bool isDragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        if (transform.gameObject.CompareTag("Stick"))
        {
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
            StartCoroutine(DragObject());
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        StopCoroutine(DragObject());
    }

    private IEnumerator DragObject()
    {
        while (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
            yield return new WaitForFixedUpdate();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}

