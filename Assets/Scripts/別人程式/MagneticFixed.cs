using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFixed : MonoBehaviour
{
    private GameObject otherJoint;
    static public bool isContact = false;
    public bool isLock = false;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("JointPoint") & other.CompareTag("JointPoint") & !isLock)
        {
            isContact = true;
            otherJoint = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("JointPoint") & other.CompareTag("JointPoint") & !isLock)
        {
            isContact = false;
            otherJoint = null;
        }
    }
    private void Update()
    {
        if (isContact & otherJoint != null)
        {
            transform.parent.position = otherJoint.transform.position - transform.TransformDirection(transform.localPosition);
            //transform.position = otherJoint.transform.position;
            transform.parent.gameObject.tag = "LockStick";
            transform.gameObject.tag = "LockPoint";
            isLock = true;
        }
    }
}
