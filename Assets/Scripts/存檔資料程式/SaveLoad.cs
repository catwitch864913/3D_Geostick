using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public void Save()
    {
        TheMedal theMedal=GetComponent<TheMedal>();
        //string ���״y�z = theMedal.���״y�z;
        //string ���צW�r = theMedal.���צW�r;
        float ���ץΦh�֮ɶ���� = theMedal.timer;

        //PlayerPrefs.SetString("���״y�z", ���״y�z);
        //PlayerPrefs.SetString("���צW�r", ���צW�r);
        PlayerPrefs.SetFloat("���ץΦh�֮ɶ����", ���ץΦh�֮ɶ����);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        string ���״y�z = PlayerPrefs.GetString(key: "���״y�z");
        string ���צW�r = PlayerPrefs.GetString(key: "���צW�r");
        float ���ץΦh�֮ɶ���� = PlayerPrefs.GetFloat(key: "���ץΦh�֮ɶ����");
        AchievementUI achievementUI=GetComponent<AchievementUI>();
        achievementUI.nameLabel.text = ���צW�r;
        achievementUI.descriptionLabel.text = ���״y�z;
        achievementUI.timeBonusLabel.text = ���ץΦh�֮ɶ����.ToString();
    }
}
