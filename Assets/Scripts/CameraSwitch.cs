using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    private Button switchButton;
    private Camera[] cameras;
    private int currentCameraIndex = 0;

    private void Start()
    {
        switchButton = GameObject.Find("SwitchCameraButton").GetComponent<Button>();
        switchButton.onClick.AddListener(SwitchCameraAndLight);
        cameras = Camera.allCameras;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    private void SwitchCameraAndLight()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
