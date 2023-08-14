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

        // �ˬd�O�_��Ĳ���I��A�ð���s���޿�
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        // �i�H�ϥθI���˴���Ĳ�o���i��P�_
        // �p�G��Ĳ���I��A�h�ھڻݭn����s���޿�
        // �Ҧp�]�m�s���ΩM�I�쪺�s�����A�Φ�m
    }
}
