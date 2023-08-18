using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterFather : MonoBehaviour
{
    [Header("�Q�~�Ӫ����d�{��")]
    [HideInInspector]
    public LevelBasic levelBasic;

    [Header("�Ǫ����")]
    public EnemyData data;
    [Header("�n�Q����~�Ӫ���q")]
    [HideInInspector]
    public float Hp;
    [Header("�n�Q�~�Ӫ����ʳt��")]
    [HideInInspector]
    public float Speed;
    [Header("UI�Ǫ��W��")]
    [HideInInspector]
    public TextMeshProUGUI MonsterName;
    [Header("UI�Ǫ���q")]
    [HideInInspector]
    public TextMeshProUGUI MonsterHp;
    [Header("�Ǫ���Rigidbody")]
    [HideInInspector]
    public Rigidbody rb;

    [Header("�樫���u")]//���է����i�H�令privite
    [HideInInspector]
    public Transform target;
    [Header("��e����F���@��")]
    [HideInInspector]
    public int wavepointIndex = 0;
    [Header("�өǪ�����")]
    public MonsterType monsterType;

    public float AttackingTime = 1; // �Ǫ��������ɶ�
    public bool isPoisoned; // �O�_���r
    public float PoisonTime = 1; // ���r����ɶ��]�C1��1���A�@��5���^
    public int PoisonCount = 5; // ���r���妸��
    public Animator myAnimator;
    public float changetime = 0.2f;
    public bool colorchanged = false;
    public void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) // ��F�֤ߴN�����]�ثe�^
        {
            if (monsterType == MonsterType.Boss)
            {
                levelBasic.CoreHP -= 10;
                levelBasic.UpdateCoreHP();
                Destroy(gameObject);
            }
            else
            {
                levelBasic.CoreHP--;
                levelBasic.UpdateCoreHP();
                Destroy(gameObject);
            }
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    public void UpdateMonsterHp()
    {
        MonsterHp.text = "��q�G" + Hp;
    }
    public void UpdateMonsterName()
    {
        MonsterName.text = data.EnemyName;
    }

    public void Dead()
    {
        if (monsterType == MonsterType.CommonEnemy)
        {
            levelBasic.Coin += 10;
            levelBasic.UpdateCoinWallet();
            Destroy(gameObject);
        }
        else if (monsterType == MonsterType.EliteEnemy)
        {
            levelBasic.Coin += 20;
            levelBasic.UpdateCoinWallet();
            Destroy(gameObject);
        }
        else if (monsterType == MonsterType.LittleBoss)
        {
            levelBasic.Coin += 100;
            levelBasic.UpdateCoinWallet();
            Destroy(gameObject);
        }
        else if (monsterType == MonsterType.Boss)
        {
            print("�ڥ���Boss�F�A�ڥi�H�q���F");
            levelBasic.WinPanel.SetActive(true);
            Time.timeScale = 0f;
            Destroy(gameObject);
        }

    }


    

}
