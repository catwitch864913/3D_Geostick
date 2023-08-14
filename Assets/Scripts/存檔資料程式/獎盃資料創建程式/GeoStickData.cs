using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GeoStick/Data Stick")]
public class GeoStickData : ScriptableObject
{
    [Header("���צW��")]
    public string nameStick;
    [Header("���׹Ϥ�")]
    public Sprite stickPicture;
    [Header("���״y�z")]
    public string stickDescription;
    [Header("������")]
    public string stickGetData;

    private void OnEnable()
    {
        string �Ĥ@����� = PlayerPrefs.GetString(key: "a");
        string �ĤG����� = PlayerPrefs.GetString(key: "b");
        if (�Ĥ@����� != null&& nameStick== "�ڬO����P�O�`�N��")
        {
            Debug.Log("�ڦ��������");
            stickGetData = �Ĥ@�����;
        }
        if (�ĤG����� != null&&nameStick=="�쭵����")
        {
            Debug.Log("�ڧ��ĤG����ƤF");
            stickGetData = �ĤG�����;
        }
    }
}
