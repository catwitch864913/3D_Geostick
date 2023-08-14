using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public void Save()
    {
        TheMedal theMedal=GetComponent<TheMedal>();
        //string 獎盃描述 = theMedal.獎盃描述;
        //string 獎盃名字 = theMedal.獎盃名字;
        float 獎盃用多少時間獲取 = theMedal.timer;

        //PlayerPrefs.SetString("獎盃描述", 獎盃描述);
        //PlayerPrefs.SetString("獎盃名字", 獎盃名字);
        PlayerPrefs.SetFloat("獎盃用多少時間獲取", 獎盃用多少時間獲取);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        string 獎盃描述 = PlayerPrefs.GetString(key: "獎盃描述");
        string 獎盃名字 = PlayerPrefs.GetString(key: "獎盃名字");
        float 獎盃用多少時間獲取 = PlayerPrefs.GetFloat(key: "獎盃用多少時間獲取");
        AchievementUI achievementUI=GetComponent<AchievementUI>();
        achievementUI.nameLabel.text = 獎盃名字;
        achievementUI.descriptionLabel.text = 獎盃描述;
        achievementUI.timeBonusLabel.text = 獎盃用多少時間獲取.ToString();
    }
}
