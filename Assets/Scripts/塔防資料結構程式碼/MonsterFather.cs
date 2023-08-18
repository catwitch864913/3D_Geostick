using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterFather : MonoBehaviour
{
    [Header("被繼承的關卡程式")]
    [HideInInspector]
    public LevelBasic levelBasic;

    [Header("怪物資料")]
    public EnemyData data;
    [Header("要被資料繼承的血量")]
    [HideInInspector]
    public float Hp;
    [Header("要被繼承的移動速度")]
    [HideInInspector]
    public float Speed;
    [Header("UI怪物名稱")]
    [HideInInspector]
    public TextMeshProUGUI MonsterName;
    [Header("UI怪物血量")]
    [HideInInspector]
    public TextMeshProUGUI MonsterHp;
    [Header("怪物的Rigidbody")]
    [HideInInspector]
    public Rigidbody rb;

    [Header("行走路線")]//測試完畢可以改成privite
    [HideInInspector]
    public Transform target;
    [Header("當前走到了哪一格")]
    [HideInInspector]
    public int wavepointIndex = 0;
    [Header("該怪物種類")]
    public MonsterType monsterType;

    public float AttackingTime = 1; // 怪物攻擊的時間
    public bool isPoisoned; // 是否中毒
    public float PoisonTime = 1; // 中毒扣血時間（每1秒扣1次，共扣5次）
    public int PoisonCount = 5; // 中毒扣血次數
    public Animator myAnimator;
    public float changetime = 0.2f;
    public bool colorchanged = false;
    public void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) // 到達核心就消失（目前）
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
        MonsterHp.text = "血量：" + Hp;
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
            print("我打敗Boss了，我可以通關了");
            levelBasic.WinPanel.SetActive(true);
            Time.timeScale = 0f;
            Destroy(gameObject);
        }

    }


    

}
