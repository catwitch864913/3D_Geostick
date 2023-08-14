using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool isRotating = false;
    private Vector3 initialDirection;
    private Quaternion initialRotation;

    private void OnMouseDown()
    {
        if (transform.gameObject.CompareTag("LockStick") & IsJointLock())
        {
            isRotating = true;
            initialDirection = FindLockPoint().transform.position - GetMouseWorldPosition();
            initialRotation = transform.rotation;
            StartCoroutine(RotateObject());
        }
    }

    private void OnMouseUp()
    {
        isRotating = false;
        StopCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {
        while (isRotating)
        {
            Vector3 currentDirection = FindLockPoint().transform.position - GetMouseWorldPosition();
            transform.rotation = initialRotation * Quaternion.FromToRotation(initialDirection.normalized, currentDirection.normalized);
            yield return new WaitForFixedUpdate();
        }
    }


    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private bool IsJointLock()
    {
        if(GameObject.FindWithTag("LockPoint") != null)
        {
            MagneticJoint joint = GameObject.FindWithTag("LockPoint").GetComponent<MagneticJoint>();
            return joint.isLock;
        }
        return false;
    }

    private GameObject FindLockPoint()
    {
        GameObject[] lockPointObjects = transform.GetComponentsInChildren<Transform>(true).Where(child => child.CompareTag("LockPoint")).Select(child => child.gameObject).ToArray();
        return lockPointObjects[0];
    }
}
