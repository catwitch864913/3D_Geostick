using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float countdownTime = 120f; // 倒數計時的秒數，這裡設定為 2 分鐘

    private int minutes;
    private int seconds;

    public bool 時間到=false;

    private void Start()
    {
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (時間到) return;
        countdownTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (countdownTime <= 0f)
        {
            時間到 = true;
            print("時間到，你輸了");
        }
    }

    private void UpdateTimerDisplay()
    {
        minutes = Mathf.FloorToInt(countdownTime / 60f);
        seconds = Mathf.FloorToInt(countdownTime % 60f);

        // 將分鐘和秒數轉換為字串形式，並顯示在 UI 上
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
