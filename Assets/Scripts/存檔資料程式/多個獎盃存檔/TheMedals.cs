using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TheMedals : MonoBehaviour
{
    [Header("要記錄的時間")]
    public float timer;

    [Header("顯示時間的text")]
    public Text timeclock;

    [Header("獎盃UI")]
    public GameObject[] goStickUI;

    [Header("獎盃資料陣列")]
    public GeoStickData[] dataSticks;

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        timeclock.text = "時間:" + timer.ToString();
    }
    public void 獲取多個獎杯時()
    {
        for (int i = 0; i < 4; i++)
        {
            goStickUI[i].transform.Find("Text_獎盃名字").GetComponent<Text>().text = dataSticks[i].nameStick;
            goStickUI[i].transform.Find("Text_獎盃描述").GetComponent<Text>().text = dataSticks[i].stickDescription;
            //goStickUI[i].transform.Find("Text_獎盃破關時間").GetComponent<Text>().text = "Time：" + dataSticks[i].stickTime;
            goStickUI[i].transform.Find("Image_獎盃圖示").GetComponent<Image>().sprite = dataSticks[i].stickPicture;
            goStickUI[i].transform.Find("Text_獎盃獲取時間").GetComponent<Text>().text = dataSticks[i].stickGetData;
            print(goStickUI[i]);
        }
    }
    /*public void 當獲取到的獎盃要更新資料()
    {
        if (dataSticks[0].nameStick == "我是爛獎牌別注意我") UpdateStickTime();
        if (dataSticks[1].nameStick == "傻傻的獎盃") UpdateStickTime();
        if (dataSticks[2].nameStick == "初音獎盃") UpdateStickTime();
        if (dataSticks[3].nameStick== "雪初音獎盃")UpdateStickTime();
    }*/

   /* private void UpdateStickTime()
    {
        if (timer < dataSticks[0].stickTime)
        {
            dataSticks[0].stickTime = timer;
            UpdateGetData();
        }
        if (timer < dataSticks[1].stickTime)
        {
            dataSticks[1].stickTime = timer;
            UpdateGetData();
        }
        if (timer < dataSticks[2].stickTime)
        {
            dataSticks[2].stickTime = timer;
            UpdateGetData();
        }
        if (timer < dataSticks[3].stickTime)
        {
            dataSticks[3].stickTime = timer;
            UpdateGetData();
        }
    }*/
    private void UpdateGetData()
    {
        dataSticks[0].stickGetData = GetUniqueIdentifier();
        dataSticks[1].stickGetData = GetUniqueIdentifier();
        dataSticks[2].stickGetData = GetUniqueIdentifier();
        dataSticks[3].stickGetData = GetUniqueIdentifier();
    }

    #region 簡化方式
    /*public void 獲取多個獎杯時()
    {
        for (int i = 0; i < 4; i++)
        {
            goStickUI[i].transform.Find("Text_獎盃名字").GetComponent<Text>().text = dataSticks[i].nameStick;
            goStickUI[i].transform.Find("Text_獎盃描述").GetComponent<Text>().text = dataSticks[i].stickDescription;
            goStickUI[i].transform.Find("Text_獎盃破關時間").GetComponent<Text>().text = "Time：" + dataSticks[i].stickTime;
            goStickUI[i].transform.Find("Image_獎盃圖示").GetComponent<Image>().sprite = dataSticks[i].stickPicture;
            goStickUI[i].transform.Find("Text_獎盃獲取時間").GetComponent<Text>().text = dataSticks[i].stickGetData;
            print(goStickUI[i]);
        }
    }

    public void 當獲取到的獎盃要更新資料()
    {
        for (int i = 0; i < 4; i++)
        {
            if (dataSticks[i].nameStick == "我是爛獎牌別注意我" || dataSticks[i].nameStick == "傻傻的獎盃" || dataSticks[i].nameStick == "初音獎盃" || dataSticks[i].nameStick == "雪初音獎盃")
            {
                if (timer < dataSticks[i].stickTime)
                {
                    dataSticks[i].stickTime = timer;
                    dataSticks[i].stickGetData = GetUniqueIdentifier();
                    UpdateStickUI(i);
                }
            }
        }
    }

    private void UpdateStickUI(int index)
    {
        goStickUI[index].transform.Find("Text_獎盃破關時間").GetComponent<Text>().text = "Time：" + dataSticks[index].stickTime;
        goStickUI[index].transform.Find("Text_獎盃獲取時間").GetComponent<Text>().text = dataSticks[index].stickGetData;
    }*/
    #endregion
    private string GetUniqueIdentifier()
    {
        // 使用时间戳生成唯一标识符
        string timeStamp = System.DateTime.Now.ToString("yyyy" + "_" + "MM" + "_" + "dd" + "_" + "HHmmss");
        return timeStamp;
    }
}
