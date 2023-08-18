using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int HP = 10;

    private Transform target;
    private int wavepointIndex = 0;
    private float AttackingTime = 1;
    private float PoisonTime = 1; // 中毒時間
    private bool isPoisoned; // 是否中毒


    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0]; // 目標 = 路徑點
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); // 移動 要改速度改speed即可

        if (Vector3.Distance(transform.position, target.position) <= 0.002f) // 換下一個路徑點
        {
            GetNextWaypoint();
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        if (isPoisoned)
        {
            int count = 5;
            PoisonTime -= Time.deltaTime;
            if (PoisonTime <= 0)
            {
                HP -= 5;
                Debug.Log(HP);
                count -= 1;
            }
            if (count <= 0)
            {
                isPoisoned = false;
            }
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) // 到達核心就消失（目前）
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void WallAttack()
    {
        // if ()
    }

    void OnCollisionEnter(Collision other) // 碰撞
    {
        Debug.Log("觸發碰撞事件");
    //     // Destroy(gameObject);
    //     Debug.Log(other.gameObject);
    //     if (other.gameObject == wall2)
    //     {
    //         Destroy(gameObject);
    //     }
    }
    void OnCollisionStay(Collision other) // 碰撞
    {
        Debug.Log("持續碰撞");
        // Destroy(gameObject);
        Debug.Log(other.gameObject);
        Debug.Log(other.gameObject.tag);
        AttackingTime -= Time.deltaTime;
        if (AttackingTime <= 0)
        {
            switch (other.gameObject.tag)
            {
                case "Wall_Burn":
                    other.gameObject.GetComponent<WallBurn>().HP -= 1;
                    HP -= 10;
                    Debug.Log(HP);
                    AttackingTime = 1;
                    if (other.gameObject == null)
                    {
                        AttackingTime = 1;
                        Debug.Log("ResetTimer");
                    }
                    break;
                case "Wall_Poison":
                    isPoisoned = true;
                    other.gameObject.GetComponent<WallPoison>().HP -= 1;
                    AttackingTime = 1;
                    break;
                case "Wall_Normal":
                    other.gameObject.GetComponent<WallNormal>().HP -= 1;
                    AttackingTime = 1;
                    break;

            }
        }
        // if (other.gameObject.tag)
        // {
        //     if (AttackingTime <= 0)
        //     {
        //         other.gameObject.SetActive(false);
        //         HP -= 10;
        //         AttackingTime = 1;
        //     }
        //     // Destroy(other.gameObject);

        // }
    }
    void OnCollisionExit(Collision other)
    {
        AttackingTime = 1;
        Debug.Log("Reset Time.");
    }
}
