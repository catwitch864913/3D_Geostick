using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
public class TheMedal : MonoBehaviour
{
    [Header("要記錄的時間")]
    public float timer;

    [Header("顯示時間的text")]
    public Text timeclock;

    [Header("獎盃UI")]
    public GameObject goStickUI;

    [Header("獎盃資料陣列")]
    public GeoStickData dataSticks;
    [Header("獎盃名稱")]
    public string MedalName;

    public List<GeoStickData> randomStick = new List<GeoStickData>();

    static TheMedal _instance;
    public static TheMedal instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TheMedal();
            }
            return _instance;
        }
    }

    public bool 存檔第一關獎盃;

    //public bool 記錄了;
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        timeclock.text = "時間:" + timer.ToString();
        //獲取獎杯時();
        print("我還沒進入喔");
        if (PlayerPrefs.GetString(key: "a") == null)
        {
            print("我沒有檔案");
            print(PlayerPrefs.GetString(key: "a"));
        }
    }
    /*public void 紀錄按鈕()
    {
        //記錄了 = true;
        Game記分器.instance.獎盃資料.name = "aaaaaaa";
        //Game記分器.instance.獎盃資料.description = 獎盃描述;
        //Game記分器.instance.獎盃資料.timeBonus = timer;
        //Game記分器.instance.上傳檔案();
        timer = 0;
        //Invoke("關閉紀錄", 0.5f);
    }
    */
    public void 獲取獎杯時()
    {
        goStickUI.transform.Find("Text_獎盃名字").GetComponent<Text>().text = dataSticks.nameStick;
        goStickUI.transform.Find("Text_獎盃描述").GetComponent<Text>().text = dataSticks.stickDescription;
        goStickUI.transform.Find("Image_獎盃圖示").GetComponent<Image>().sprite = dataSticks.stickPicture;
        goStickUI.transform.Find("Text_獎盃獲取時間").GetComponent<Text>().text = dataSticks.stickGetData;
    }
    public void 當獲取到的獎盃要更新資料()
    {
        if (dataSticks.nameStick == "我是爛獎牌別注意我")
        {
            UpdateGetData();
            print("我進到更新資料了");
        }
    }

    /* private void UpdateStickTime()
     {
         if(timer< dataSticks.stickTime)
         {
             dataSticks.stickTime = timer;
             UpdateGetData();
         }
     }*/
    private void UpdateGetData()
    {
        dataSticks.stickGetData = GetUniqueIdentifier();
        PlayerPrefs.SetString("a", dataSticks.stickGetData);
        print(dataSticks.stickGetData);
        print(PlayerPrefs.GetString(key: "a"));
        存檔第一關獎盃 = true;
    }
    private string GetUniqueIdentifier()
    {
        // 使用时间戳生成唯一标识符
        string timeStamp = System.DateTime.Now.ToString("yyyy" + "_" + "MM" + "_" + "dd" + "_" + "HHmmss");
        return timeStamp;
    }
    //    string fileName = "image_" + i + "_" + GetUniqueIdentifier() + ".png";















}
