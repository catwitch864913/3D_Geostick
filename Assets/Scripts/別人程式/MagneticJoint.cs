using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticJoint : MonoBehaviour
{
    public GameObject otherJoint;
    public bool isContact = false;
    public bool isLock = false;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Point") & other.CompareTag("Point") & !isLock)
        {
            isContact = true;
            otherJoint = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Point") & other.CompareTag("Point") & !isLock)
        {
            isContact = false;
            otherJoint = null;
        }
    }
     //& gameObject.transform.parent.CompareTag("FreeStick")
    private void Update()
    {
        if (isContact & otherJoint != null & !IsDragging())
        {
            //make sure the joint is always locked on the other one
            transform.parent.position = otherJoint.transform.position - transform.TransformDirection(transform.localPosition);          
            transform.parent.gameObject.tag = "LockStick";
            transform.gameObject.tag = "LockPoint";
            isLock = true;

            //transform.parent.position = otherJoint.transform.position - transform.position;
            //transform.position = otherJoint.transform.position;
            //if (gameObject.CompareTag("LockPoint") & transform.position != otherJoint.transform.position)
            //{
            //    transform.parent.position = otherJoint.transform.position - transform.TransformDirection(transform.localPosition);
            //}
        }
    }

    private bool IsDragging()
    {
        Translation dragLock = transform.parent.GetComponent<Translation>();
        return dragLock.isDragging;
    }

    private bool IsRotating()
    {
        Rotation rotateLock = transform.parent.GetComponent<Rotation>();
        return rotateLock.isRotating;
    }
}
