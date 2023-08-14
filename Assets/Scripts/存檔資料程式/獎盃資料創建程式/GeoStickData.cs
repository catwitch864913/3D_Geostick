using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GeoStick/Data Stick")]
public class GeoStickData : ScriptableObject
{
    [Header("獎盃名稱")]
    public string nameStick;
    [Header("獎盃圖片")]
    public Sprite stickPicture;
    [Header("獎盃描述")]
    public string stickDescription;
    [Header("獲取日期")]
    public string stickGetData;

    private void OnEnable()
    {
        string 第一關資料 = PlayerPrefs.GetString(key: "a");
        string 第二關資料 = PlayerPrefs.GetString(key: "b");
        if (第一關資料 != null&& nameStick== "我是爛獎牌別注意我")
        {
            Debug.Log("我有抓取到資料");
            stickGetData = 第一關資料;
        }
        if (第二關資料 != null&&nameStick=="初音獎盃")
        {
            Debug.Log("我抓到第二關資料了");
            stickGetData = 第二關資料;
        }
    }
}
