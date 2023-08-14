using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game記分器
{

    #region 單例
    static Game記分器 _instance;
    public static Game記分器 instance
    {
        //讀取
        get
        {
            //當有人需要我的時候 如果我不存在
            if (_instance == null)
            {
                //我就把自己憑空創造出來
                _instance = new Game記分器();
            }
            return _instance;
        }
    }
    #endregion
    public Achievement 獎盃資料;
    //public List<Achievement> achievements;

    // 其他的遊戲邏輯和功能

    //public void UnlockAchievement(string achievementName)
    //{
    //    Achievement achievement = achievements.Find(a => a.name == achievementName);
    //    if (achievement != null)
    //    {
    //        achievement.isUnlocked = true;
    //        // 更新獎牌介面等相關操作
    //    }
    //}
    public void 下載檔案()
    {
        //嘗試從硬碟讀取json文件
        string json = PlayerPrefs.GetString("Score","N");
        //把json文字轉換回玩家資料
        獎盃資料 = JsonUtility.FromJson<Achievement>(json);
        /*if (json != "N")
        {
            //把json文字轉換回玩家資料
            獎盃資料 = JsonUtility.FromJson<Achievement>(json);
        }
        else
        {
            //分配記憶體給存檔資料
            獎盃資料 = new Achievement();
            //分配記憶體給陣列 避免報錯
        }*/
    }
    public void 上傳檔案()
    {
        //轉換檔案成Json格式
        string json = JsonUtility.ToJson(獎盃資料, true);
        Debug.Log(json);
        //把文件丟到硬碟中
        PlayerPrefs.SetString("Score", json);
    }



    [System.Serializable]
    public struct Achievement
    {
        public string name;
        public string description;
        //public Sprite icon;
        public float timeBonus;
        //public bool isUnlocked;
    }
}
