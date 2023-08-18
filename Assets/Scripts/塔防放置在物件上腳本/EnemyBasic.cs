using TMPro;
using UnityEngine;

public class EnemyBasic : MonsterFather
{


    public void Awake()
    {
        levelBasic = GameObject.Find("GM").GetComponent<LevelBasic>();
        Hp = data.HP;
        Speed = data.Speed;
        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        //小怪是否要抓取??
        //MonsterName = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        //MonsterHp = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        target = Waypoints.points[0]; // 目標 = 路徑點

    }
    public void Update()
    {
        #region 外掛
        if (Input.GetKeyDown(KeyCode.X))
        {
            Hp -= 5;
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
        if (isPoisoned) // 敵人中毒
        {
            PoisonTime -= Time.deltaTime;
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.09f, 0.8f, 0.09f, 1);
            // Debug.Log("time" + PoisonTime);
            if (PoisonTime <= 0)
            {
                Hp -= 5;
                Debug.Log(Hp);
                PoisonCount -= 1;
                PoisonTime = 1;
            }
            if (PoisonCount <= 0)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                isPoisoned = false;
                PoisonTime = 1;
            }
        }
        if (colorchanged) // 顏色改變（敵人受傷）
        {
            changetime -= Time.deltaTime;
            if (changetime <= 0)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                changetime = 0.2f;
                colorchanged = false;
            }
        }
        //UpdateMonsterHp();
    }
    public void OnCollisionEnter(Collision other) // 碰撞
    {
        Debug.Log("觸發碰撞事件");
    }
    public void OnCollisionStay(Collision other) // 碰撞
    {

        AttackingTime -= Time.deltaTime;
        if (AttackingTime <= 0)
        {
            switch (other.gameObject.tag)
            {
                case "Wall_Burn":
                    other.gameObject.GetComponent<Wall>().HP -= 1;
                    Hp -= 10;
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.3f, 0.3f, 1);
                    colorchanged = true;
                    AttackingTime = 1;
                    if (other.gameObject == null)
                    {
                        AttackingTime = 1;
                    }
                    break;
                case "Wall_Poison":
                    isPoisoned = true;
                    PoisonCount = 5;
                    other.gameObject.GetComponent<Wall>().HP -= 1;
                    AttackingTime = 1;
                    break;
                case "Wall_Normal":
                    other.gameObject.GetComponent<Wall>().HP -= 1;
                    AttackingTime = 1;
                    break;
            }
            AttackingTime = 1;
            myAnimator.SetBool("Attack", false);

        }
        if (other.gameObject.GetComponent<Wall>().HP <= 0)
        {
            myAnimator.SetBool("Attack", false);
        }
    }
    public void OnCollisionExit(Collision other)
    {
        AttackingTime = 1;
        myAnimator.SetBool("Attack", false);
    }
}
