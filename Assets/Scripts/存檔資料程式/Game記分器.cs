using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game�O����
{

    #region ���
    static Game�O���� _instance;
    public static Game�O���� instance
    {
        //Ū��
        get
        {
            //���H�ݭn�ڪ��ɭ� �p�G�ڤ��s�b
            if (_instance == null)
            {
                //�ڴN��ۤv�̪ųгy�X��
                _instance = new Game�O����();
            }
            return _instance;
        }
    }
    #endregion
    public Achievement ���׸��;
    //public List<Achievement> achievements;

    // ��L���C���޿�M�\��

    //public void UnlockAchievement(string achievementName)
    //{
    //    Achievement achievement = achievements.Find(a => a.name == achievementName);
    //    if (achievement != null)
    //    {
    //        achievement.isUnlocked = true;
    //        // ��s���P�����������ާ@
    //    }
    //}
    public void �U���ɮ�()
    {
        //���ձq�w��Ū��json���
        string json = PlayerPrefs.GetString("Score","N");
        //��json��r�ഫ�^���a���
        ���׸�� = JsonUtility.FromJson<Achievement>(json);
        /*if (json != "N")
        {
            //��json��r�ഫ�^���a���
            ���׸�� = JsonUtility.FromJson<Achievement>(json);
        }
        else
        {
            //���t�O���鵹�s�ɸ��
            ���׸�� = new Achievement();
            //���t�O���鵹�}�C �קK����
        }*/
    }
    public void �W���ɮ�()
    {
        //�ഫ�ɮצ�Json�榡
        string json = JsonUtility.ToJson(���׸��, true);
        Debug.Log(json);
        //������w�Ф�
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
