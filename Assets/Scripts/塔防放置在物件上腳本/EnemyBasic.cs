using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBasic : MonoBehaviour
{
    [Header("被繼承的關卡程式")]
    public LevelBasic levelBasic;

    [Header("怪物資料")]
    public EnemyData data;

    [Header("要被資料繼承的血量")]
    public float Hp;
    [Header("要被繼承的移動速度")]
    public float Speed;
    [Header("行走路線")]//測試完畢可以改成privite
    public Transform target;
    [Header("當前走到了哪一格")]
    public int wavepointIndex = 0;
    [Header("該怪物的型態")]
    public EnemyType enemyType;
    [Header("該怪物種類")]
    public MonsterType monsterType;
    private void Awake()
    {
        levelBasic = GameObject.Find("GM").GetComponent<LevelBasic>();
        Hp = data.HP;
        Speed = data.Speed;
    }
    private void Start()
    {
        target = Waypoints.points[0]; // 目標 = 路徑點

    }
    private void Update()
    {
        /*if (enemyType == EnemyType.NormalEnemy)
        {
            levelBasic.NormalEnemy--;
        }*/
        #region 外掛
        if (Input.GetKeyDown(KeyCode.X))
        {
            Hp = 0;
        }
        #endregion

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World); // 移動 要改速度改speed即可

        if (Vector3.Distance(transform.position, target.position) <= 0.002f) // 換下一個路徑點
        {
            GetNextWaypoint();
        }
        if (Hp <= 0)
        {
            Dead();
        }
    }
    void GetNextWaypoint()
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







    private void Dead()
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
